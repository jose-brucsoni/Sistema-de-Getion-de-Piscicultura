using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Sistema_de_Getion_de_Piscicultura.Infraestructura;
using Sistema_de_Getion_de_Piscicultura.Modelos;

namespace Sistema_de_Getion_de_Piscicultura.Servicios;

public class Lotes_Service
{
    private readonly IDbConnectionFactory _db;

    public Lotes_Service(IDbConnectionFactory db)
    {
        _db = db;
    }

    public List<Lote> ObtenerActivos()
    {
        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.Query<Lote>(
                "dbo.sp_Lote_Listar",
                new { Estado = (int)EstadoLote.Activo, EspecieId = (int?)null, EstanqueId = (int?)null },
                commandType: CommandType.StoredProcedure)
            .OrderBy(l => l.Codigo)
            .ToList();
    }

    public List<Lote> ObtenerTodos()
    {
        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.Query<Lote>(
                "dbo.sp_Lote_Listar",
                new { Estado = (int?)null, EspecieId = (int?)null, EstanqueId = (int?)null },
                commandType: CommandType.StoredProcedure)
            .OrderBy(l => l.Codigo)
            .ToList();
    }

    public Lote? ObtenerPorId(int id)
    {
        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.QueryFirstOrDefault<Lote>(
            "dbo.sp_Lote_ObtenerPorId",
            new { Id = id },
            commandType: CommandType.StoredProcedure);
    }

    public (bool exito, string mensaje, int? loteId) Crear(Lote lote)
    {
        try
        {
            using var conn = _db.CreateConnection();
            conn.Open();

            var codigo = conn.QuerySingle<string>("SELECT dbo.fn_GenerarCodigoLoteSiguiente()");
            lote.Codigo = codigo;

            var resultado = lote.CrearLote();
            if (!resultado.exito)
            {
                return (false, resultado.mensaje, null);
            }

            var p = new DynamicParameters();
            p.Add("@Codigo", lote.Codigo);
            p.Add("@FechaSiembra", lote.FechaSiembra);
            p.Add("@CantidadInicial", lote.CantidadInicial);
            p.Add("@EspecieId", lote.EspecieId);
            p.Add("@EstanqueId", lote.EstanqueId);
            p.Add("@ProveedorId", lote.ProveedorId);
            p.Add("@FaseActual", (int)lote.FaseActual);
            p.Add("@Estado", (int)lote.Estado);
            p.Add("@IdNuevo", dbType: DbType.Int32, direction: ParameterDirection.Output);

            conn.Execute("dbo.sp_Lote_Insertar", p, commandType: CommandType.StoredProcedure);
            var id = p.Get<int>("@IdNuevo");
            return (true, resultado.mensaje, id);
        }
        catch (SqlException ex)
        {
            return (false, MensajeSql(ex), null);
        }
    }

    public string ObtenerCodigoSiguiente()
    {
        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.QuerySingle<string>("SELECT dbo.fn_GenerarCodigoLoteSiguiente()");
    }

    public (bool exito, string mensaje) Editar(
        int id,
        DateTime fechaSiembra,
        int cantidadInicial,
        int especieId,
        int estanqueId,
        int proveedorId,
        FaseCrecimiento faseActual)
    {
        var lote = ObtenerPorId(id);
        if (lote is null)
        {
            return (false, "Lote no encontrado.");
        }

        var edit = lote.EditarLote(lote.Codigo, fechaSiembra, cantidadInicial, especieId, estanqueId, proveedorId, faseActual);
        if (!edit.exito)
        {
            return edit;
        }

        try
        {
            using var conn = _db.CreateConnection();
            conn.Open();
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@FechaSiembra", fechaSiembra);
            p.Add("@CantidadInicial", cantidadInicial);
            p.Add("@EspecieId", especieId);
            p.Add("@EstanqueId", estanqueId);
            p.Add("@ProveedorId", proveedorId);
            p.Add("@FaseActual", (int)faseActual);
            p.Add("@FilasAfectadas", dbType: DbType.Int32, direction: ParameterDirection.Output);

            conn.Execute("dbo.sp_Lote_Actualizar", p, commandType: CommandType.StoredProcedure);
            var filas = p.Get<int>("@FilasAfectadas");
            if (filas == 0)
            {
                return (false, "No se pudo actualizar el lote (puede estar anulado).");
            }

            return (true, edit.mensaje);
        }
        catch (SqlException ex)
        {
            return (false, MensajeSql(ex));
        }
    }

    public (bool exito, string mensaje) Anular(int id)
    {
        var lote = ObtenerPorId(id);
        if (lote is null)
        {
            return (false, "Lote no encontrado.");
        }

        if (lote.Estado == EstadoLote.Inactivo)
        {
            return (false, "El lote ya se encuentra anulado.");
        }

        try
        {
            using var conn = _db.CreateConnection();
            conn.Open();
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@FilasAfectadas", dbType: DbType.Int32, direction: ParameterDirection.Output);

            conn.Execute("dbo.sp_Lote_Anular", p, commandType: CommandType.StoredProcedure);
            var filas = p.Get<int>("@FilasAfectadas");
            if (filas == 0)
            {
                return (false, "No se pudo anular el lote.");
            }

            return (true, "Lote anulado correctamente.");
        }
        catch (SqlException ex)
        {
            return (false, MensajeSql(ex));
        }
    }

    private static string MensajeSql(SqlException ex)
        => ex.Number switch
        {
            547 => "No se puede completar la operación: existe información relacionada en otra tabla.",
            2601 or 2627 => "Ya existe un registro con el mismo código o clave única.",
            _ => $"Error de base de datos: {ex.Message}"
        };
}
