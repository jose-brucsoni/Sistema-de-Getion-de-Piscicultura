using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Sistema_de_Getion_de_Piscicultura.Infraestructura;
using Sistema_de_Getion_de_Piscicultura.Modelos;

namespace Sistema_de_Getion_de_Piscicultura.Servicios;

public class ParametrosAgua_Service
{
    private readonly IDbConnectionFactory _db;
    private readonly Lotes_Service _lotes;

    public ParametrosAgua_Service(IDbConnectionFactory db, Lotes_Service lotes)
    {
        _db = db;
        _lotes = lotes;
    }

    public IReadOnlyList<ParametroAgua> ObtenerTodos()
    {
        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.Query<ParametroAgua>(
                "dbo.sp_ParametroAgua_ListarFiltrado",
                new { LoteId = (int?)null, Desde = (DateTime?)null, Hasta = (DateTime?)null },
                commandType: CommandType.StoredProcedure)
            .OrderByDescending(r => r.FechaRegistro)
            .ToList();
    }

    public ParametroAgua? ObtenerPorId(int id)
    {
        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.QueryFirstOrDefault<ParametroAgua>(
            "dbo.sp_ParametroAgua_ObtenerPorId",
            new { Id = id },
            commandType: CommandType.StoredProcedure);
    }

    public IReadOnlyList<ParametroAgua> ObtenerFiltrados(
        int? estanqueId,
        int? loteId,
        DateTime? desde,
        DateTime? hasta)
    {
        using var conn = _db.CreateConnection();
        conn.Open();

        if (loteId.HasValue && loteId.Value > 0)
        {
            var list = conn.Query<ParametroAgua>(
                    "dbo.sp_ParametroAgua_ListarFiltrado",
                    new { LoteId = loteId, Desde = desde, Hasta = hasta },
                    commandType: CommandType.StoredProcedure)
                .OrderByDescending(r => r.FechaRegistro)
                .ToList();

            if (estanqueId.HasValue && estanqueId.Value > 0)
            {
                var lote = _lotes.ObtenerPorId(loteId.Value);
                if (lote is null || lote.EstanqueId != estanqueId.Value)
                {
                    return Array.Empty<ParametroAgua>();
                }
            }

            return list;
        }

        if (estanqueId.HasValue && estanqueId.Value > 0)
        {
            return conn.Query<ParametroAgua>(
                    "dbo.sp_ParametroAgua_ListarPorEstanque",
                    new { EstanqueId = estanqueId.Value, Desde = desde, Hasta = hasta },
                    commandType: CommandType.StoredProcedure)
                .OrderByDescending(r => r.FechaRegistro)
                .ToList();
        }

        return conn.Query<ParametroAgua>(
                "dbo.sp_ParametroAgua_ListarFiltrado",
                new { LoteId = (int?)null, Desde = desde, Hasta = hasta },
                commandType: CommandType.StoredProcedure)
            .OrderByDescending(r => r.FechaRegistro)
            .ToList();
    }

    public (bool exito, string mensaje) Crear(ParametroAgua entidad)
    {
        if (_lotes.ObtenerPorId(entidad.LoteId) is null)
        {
            return (false, "El lote indicado no existe.");
        }

        var v = entidad.Validar();
        if (!v.exito)
        {
            return (false, v.mensaje);
        }

        try
        {
            using var conn = _db.CreateConnection();
            conn.Open();
            var p = new DynamicParameters();
            p.Add("@LoteId", entidad.LoteId);
            p.Add("@FechaRegistro", entidad.FechaRegistro);
            p.Add("@Temperatura", entidad.Temperatura);
            p.Add("@Ph", entidad.Ph);
            p.Add("@OxigenoDisuelto", entidad.OxigenoDisuelto);
            p.Add("@Amonio", entidad.Amonio);
            p.Add("@Nitritos", entidad.Nitritos);
            p.Add("@Turbidez", entidad.Turbidez);
            p.Add("@CondicionesClimaticas", entidad.CondicionesClimaticas);
            p.Add("@Observaciones", entidad.Observaciones);
            p.Add("@IdNuevo", dbType: DbType.Int32, direction: ParameterDirection.Output);

            conn.Execute("dbo.sp_ParametroAgua_Insertar", p, commandType: CommandType.StoredProcedure);
            var idNuevo = p.Get<int>("@IdNuevo");
            RegistrarAlertasCalidadAguaSiAplica(conn, entidad, idNuevo);
            return (true, "Parámetros del agua registrados correctamente.");
        }
        catch (SqlException ex)
        {
            return (false, MensajeSql(ex));
        }
    }

    public (bool exito, string mensaje) Actualizar(
        int id,
        int loteId,
        DateTime fechaRegistro,
        decimal temperatura,
        decimal ph,
        decimal oxigenoDisuelto,
        decimal amonio,
        decimal nitritos,
        decimal turbidez,
        string? condicionesClimaticas,
        string? observaciones)
    {
        var entidad = ObtenerPorId(id);
        if (entidad is null)
        {
            return (false, "Registro no encontrado.");
        }

        if (_lotes.ObtenerPorId(loteId) is null)
        {
            return (false, "El lote indicado no existe.");
        }

        entidad.LoteId = loteId;
        entidad.FechaRegistro = fechaRegistro;
        entidad.Temperatura = temperatura;
        entidad.Ph = ph;
        entidad.OxigenoDisuelto = oxigenoDisuelto;
        entidad.Amonio = amonio;
        entidad.Nitritos = nitritos;
        entidad.Turbidez = turbidez;
        entidad.CondicionesClimaticas = condicionesClimaticas;
        entidad.Observaciones = observaciones;

        var v = entidad.Validar();
        if (!v.exito)
        {
            return (false, v.mensaje);
        }

        try
        {
            using var conn = _db.CreateConnection();
            conn.Open();
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@LoteId", loteId);
            p.Add("@FechaRegistro", fechaRegistro);
            p.Add("@Temperatura", temperatura);
            p.Add("@Ph", ph);
            p.Add("@OxigenoDisuelto", oxigenoDisuelto);
            p.Add("@Amonio", amonio);
            p.Add("@Nitritos", nitritos);
            p.Add("@Turbidez", turbidez);
            p.Add("@CondicionesClimaticas", condicionesClimaticas);
            p.Add("@Observaciones", observaciones);
            p.Add("@FilasAfectadas", dbType: DbType.Int32, direction: ParameterDirection.Output);

            conn.Execute("dbo.sp_ParametroAgua_Actualizar", p, commandType: CommandType.StoredProcedure);
            var filas = p.Get<int>("@FilasAfectadas");
            if (filas == 0)
            {
                return (false, "No se pudo actualizar el registro.");
            }

            RegistrarAlertasCalidadAguaSiAplica(conn, entidad, id);
            return (true, "Registro actualizado correctamente.");
        }
        catch (SqlException ex)
        {
            return (false, MensajeSql(ex));
        }
    }

    public (bool exito, string mensaje) Eliminar(int id)
    {
        try
        {
            using var conn = _db.CreateConnection();
            conn.Open();
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@FilasAfectadas", dbType: DbType.Int32, direction: ParameterDirection.Output);

            conn.Execute("dbo.sp_ParametroAgua_Eliminar", p, commandType: CommandType.StoredProcedure);
            var filas = p.Get<int>("@FilasAfectadas");
            if (filas == 0)
            {
                return (false, "Registro no encontrado.");
            }

            return (true, "Registro eliminado correctamente.");
        }
        catch (SqlException ex)
        {
            return (false, MensajeSql(ex));
        }
    }

    private static string MensajeSql(SqlException ex)
        => ex.Number switch
        {
            547 => "No se puede eliminar: hay datos relacionados (por ejemplo alertas).",
            _ => $"Error de base de datos: {ex.Message}"
        };

    private static void RegistrarAlertasCalidadAguaSiAplica(SqlConnection conn, ParametroAgua entidad, int idParametroAgua)
    {
        if (conn.ExecuteScalar<int>("SELECT COUNT(1) FROM sys.tables WHERE name = 'Alerta' AND schema_id = SCHEMA_ID('dbo')") == 0)
        {
            return;
        }

        const string sqlEspecie = """
            SELECT TOP (1)
                e.TempMin,
                e.TempMax,
                e.PhMin,
                e.PhMax,
                e.OxigenoMin
            FROM dbo.Lotes l
            INNER JOIN dbo.Especie e ON e.IdEspecie = l.EspecieId
            WHERE l.Id = @IdLote;
            """;

        var rango = conn.QueryFirstOrDefault<RangoEspecieRow>(sqlEspecie, new { IdLote = entidad.LoteId });
        if (rango is null)
        {
            return;
        }

        var alertas = new List<string>();
        if (entidad.Temperatura < rango.TempMin || entidad.Temperatura > rango.TempMax) alertas.Add("Temperatura");
        if (entidad.Ph < rango.PhMin || entidad.Ph > rango.PhMax) alertas.Add("pH");
        if (entidad.OxigenoDisuelto < rango.OxigenoMin) alertas.Add("Oxígeno");

        if (alertas.Count == 0)
        {
            return;
        }

        var mensaje = $"Parámetros fuera de rango ({string.Join(", ", alertas)}) para lote {entidad.LoteId}.";
        conn.Execute(
            """
            INSERT INTO dbo.Alerta (IdLote, FechaHora, Tipo, Nivel, Mensaje, Atendida, IdParametroAgua)
            VALUES (@IdLote, @FechaHora, @Tipo, @Nivel, @Mensaje, 0, @IdParametroAgua);
            """,
            new
            {
                IdLote = entidad.LoteId,
                FechaHora = entidad.FechaRegistro,
                Tipo = "CalidadAgua",
                Nivel = "Alta",
                Mensaje = mensaje,
                IdParametroAgua = idParametroAgua
            });
    }

    private sealed class RangoEspecieRow
    {
        public decimal TempMin { get; set; }
        public decimal TempMax { get; set; }
        public decimal PhMin { get; set; }
        public decimal PhMax { get; set; }
        public decimal OxigenoMin { get; set; }
    }
}
