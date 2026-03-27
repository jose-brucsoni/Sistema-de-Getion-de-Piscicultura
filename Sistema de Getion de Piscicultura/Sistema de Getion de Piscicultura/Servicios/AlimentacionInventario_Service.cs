using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Sistema_de_Getion_de_Piscicultura.Infraestructura;
using Sistema_de_Getion_de_Piscicultura.Modelos;

namespace Sistema_de_Getion_de_Piscicultura.Servicios;

public class AlimentacionInventario_Service
{
    private readonly IDbConnectionFactory _db;
    private readonly Lotes_Service _lotesService;
    private readonly Inventario_Service _inventarioService;

    public AlimentacionInventario_Service(
        IDbConnectionFactory db,
        Lotes_Service lotesService,
        Inventario_Service inventarioService)
    {
        _db = db;
        _lotesService = lotesService;
        _inventarioService = inventarioService;
    }

    public IReadOnlyList<InventarioItem> ObtenerInventario()
        => _inventarioService.ObtenerTodos();

    public IReadOnlyList<RegistroAlimentacion> ObtenerRegistrosPorFecha(DateTime fecha)
    {
        using var conn = _db.CreateConnection();
        conn.Open();
        const string sql = """
            SELECT Id, LoteId, FechaRegistro, Horario, TipoAlimento, CantidadKg, InventarioItemId
            FROM dbo.RegistrosAlimentacion
            WHERE CAST(FechaRegistro AS date) = @d
            ORDER BY Horario
            """;
        return conn.Query<RegistroAlimentacion>(sql, new { d = fecha.Date }).ToList();
    }

    public IReadOnlyList<ConsumoEstanqueDto> ObtenerConsumoPorEstanque(DateTime fecha)
    {
        using var conn = _db.CreateConnection();
        conn.Open();
        const string sql = """
            SELECT
                r.LoteId,
                l.Codigo AS CodigoLote,
                l.EstanqueId,
                r.TipoAlimento,
                CAST(SUM(r.CantidadKg) AS decimal(18,3)) AS CantidadKg
            FROM dbo.RegistrosAlimentacion r
            INNER JOIN dbo.Lotes l ON l.Id = r.LoteId
            WHERE CAST(r.FechaRegistro AS date) = @fecha
            GROUP BY r.LoteId, l.Codigo, l.EstanqueId, r.TipoAlimento
            ORDER BY r.TipoAlimento, l.EstanqueId, l.Codigo
            """;
        return conn.Query<ConsumoEstanqueDto>(sql, new { fecha = fecha.Date }).ToList();
    }

    public IReadOnlyList<ConsumoEstanqueDto> ObtenerConsumoPorEstanqueRango(DateTime fechaInicio, DateTime fechaFin)
    {
        var inicio = fechaInicio.Date;
        var fin = fechaFin.Date;
        if (fin < inicio)
        {
            (inicio, fin) = (fin, inicio);
        }

        using var conn = _db.CreateConnection();
        conn.Open();
        const string sql = """
            SELECT
                r.LoteId,
                l.Codigo AS CodigoLote,
                l.EstanqueId,
                r.TipoAlimento,
                CAST(SUM(r.CantidadKg) AS decimal(18,3)) AS CantidadKg
            FROM dbo.RegistrosAlimentacion r
            INNER JOIN dbo.Lotes l ON l.Id = r.LoteId
            WHERE CAST(r.FechaRegistro AS date) >= @inicio AND CAST(r.FechaRegistro AS date) <= @fin
            GROUP BY r.LoteId, l.Codigo, l.EstanqueId, r.TipoAlimento
            ORDER BY r.TipoAlimento, l.EstanqueId, l.Codigo
            """;
        return conn.Query<ConsumoEstanqueDto>(sql, new { inicio, fin }).ToList();
    }

    public (bool exito, string mensaje) RegistrarAlimentacion(
        int loteId,
        DateTime fechaRegistro,
        TimeSpan horario,
        int alimentoId,
        decimal cantidadKg)
    {
        var lote = _lotesService.ObtenerPorId(loteId);
        if (lote is null)
        {
            return (false, "Lote no encontrado.");
        }

        var alimento = _inventarioService.ObtenerPorId(alimentoId);
        if (alimento is null)
        {
            return (false, "Alimento no encontrado.");
        }

        if (cantidadKg <= 0)
        {
            return (false, "La cantidad debe ser mayor a cero.");
        }

        var frecuenciaMaxima = ObtenerFrecuenciaMaxima(lote.FaseActual);
        var fechaDia = fechaRegistro.Date;

        try
        {
            using var conn = _db.CreateConnection();
            conn.Open();

            var frecuenciaActual = conn.ExecuteScalar<int>(
                """
                SELECT COUNT(1)
                FROM dbo.RegistrosAlimentacion
                WHERE LoteId = @loteId AND CAST(FechaRegistro AS date) = @fecha
                """,
                new { loteId, fecha = fechaDia });

            if (frecuenciaActual >= frecuenciaMaxima)
            {
                return (false, $"Se alcanzó la frecuencia máxima diaria para la fase {lote.FaseActual} ({frecuenciaMaxima}).");
            }

            using var tx = conn.BeginTransaction();
            try
            {
                var pDesc = new DynamicParameters();
                pDesc.Add("@Id", alimentoId);
                pDesc.Add("@CantidadKg", cantidadKg);
                pDesc.Add("@FilasAfectadas", dbType: DbType.Int32, direction: ParameterDirection.Output);

                conn.Execute(
                    "dbo.sp_InventarioItem_DescontarStock",
                    pDesc,
                    tx,
                    commandType: CommandType.StoredProcedure);

                if (pDesc.Get<int>("@FilasAfectadas") == 0)
                {
                    tx.Rollback();
                    return (false, "Stock insuficiente para registrar la alimentación.");
                }

                var pIns = new DynamicParameters();
                pIns.Add("@LoteId", loteId);
                pIns.Add("@FechaRegistro", fechaDia);
                pIns.Add("@Horario", horario);
                pIns.Add("@TipoAlimento", alimento.Nombre);
                pIns.Add("@CantidadKg", cantidadKg);
                pIns.Add("@InventarioItemId", alimentoId);
                pIns.Add("@IdNuevo", dbType: DbType.Int32, direction: ParameterDirection.Output);

                conn.Execute(
                    "dbo.sp_RegistroAlimentacion_Insertar",
                    pIns,
                    tx,
                    commandType: CommandType.StoredProcedure);

                RegistrarAlertaStockBajoSiAplica(conn, tx, alimentoId, loteId);
                tx.Commit();
                return (true, "Alimentación registrada correctamente.");
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }
        catch (SqlException ex)
        {
            return (false, $"Error de base de datos: {ex.Message}");
        }
    }

    private static int ObtenerFrecuenciaMaxima(FaseCrecimiento fase)
        => fase switch
        {
            FaseCrecimiento.Alevinaje => 6,
            FaseCrecimiento.Juveniles => 4,
            _ => 3
        };

    private static void RegistrarAlertaStockBajoSiAplica(SqlConnection conn, SqlTransaction tx, int inventarioItemId, int loteId)
    {
        if (conn.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM sys.tables WHERE name = 'Alerta' AND schema_id = SCHEMA_ID('dbo')",
                transaction: tx) == 0)
        {
            return;
        }

        var item = conn.QueryFirstOrDefault<InventarioMinimoRow>(
            """
            SELECT Id, Nombre, StockKg, StockMinimoKg
            FROM dbo.InventarioItems
            WHERE Id = @Id;
            """,
            new { Id = inventarioItemId }, tx);

        if (item is null || item.StockKg > item.StockMinimoKg)
        {
            return;
        }

        conn.Execute(
            """
            INSERT INTO dbo.Alerta (IdLote, FechaHora, Tipo, Nivel, Mensaje, Atendida, IdInventario)
            VALUES (@IdLote, @FechaHora, @Tipo, @Nivel, @Mensaje, 0, @IdInventario);
            """,
            new
            {
                IdLote = loteId,
                FechaHora = DateTime.Now,
                Tipo = "StockAlimento",
                Nivel = "Media",
                Mensaje = $"Stock bajo de '{item.Nombre}': {item.StockKg:0.###} kg (mínimo {item.StockMinimoKg:0.###}).",
                IdInventario = item.Id
            }, tx);
    }

    private sealed class InventarioMinimoRow
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal StockKg { get; set; }
        public decimal StockMinimoKg { get; set; }
    }
}

public class ConsumoEstanqueDto
{
    public int LoteId { get; set; }
    public string CodigoLote { get; set; } = string.Empty;
    public int EstanqueId { get; set; }
    public string TipoAlimento { get; set; } = string.Empty;
    public decimal CantidadKg { get; set; }
}
