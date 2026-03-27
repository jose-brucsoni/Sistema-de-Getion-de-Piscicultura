using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Sistema_de_Getion_de_Piscicultura.Infraestructura;
using Sistema_de_Getion_de_Piscicultura.Modelos;

namespace Sistema_de_Getion_de_Piscicultura.Servicios;

public class Inventario_Service
{
    private readonly IDbConnectionFactory _db;

    public Inventario_Service(IDbConnectionFactory db)
    {
        _db = db;
    }

    public List<InventarioItem> ObtenerTodos()
    {
        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.Query<InventarioItem>(
                "dbo.sp_InventarioItem_Listar",
                commandType: CommandType.StoredProcedure)
            .OrderBy(x => x.Nombre)
            .ToList();
    }

    public InventarioItem? ObtenerPorId(int itemId)
    {
        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.QueryFirstOrDefault<InventarioItem>(
            "dbo.sp_InventarioItem_ObtenerPorId",
            new { Id = itemId },
            commandType: CommandType.StoredProcedure);
    }

    public (bool exito, string mensaje) AgregarNuevoItem(string nombre, string categoria, decimal stockInicialKg, decimal stockMinimoKg)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            return (false, "El nombre del insumo es obligatorio.");
        }

        if (stockInicialKg < 0 || stockMinimoKg < 0)
        {
            return (false, "Los valores de stock no pueden ser negativos.");
        }

        try
        {
            using var conn = _db.CreateConnection();
            conn.Open();

            var existe = conn.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM dbo.InventarioItems WHERE LOWER(Nombre) = LOWER(@n)",
                new { n = nombre.Trim() });
            if (existe > 0)
            {
                return (false, "Ya existe un insumo con ese nombre.");
            }

            var p = new DynamicParameters();
            p.Add("@Nombre", nombre.Trim());
            p.Add("@Categoria", string.IsNullOrWhiteSpace(categoria) ? "General" : categoria.Trim());
            p.Add("@StockKg", stockInicialKg);
            p.Add("@StockMinimoKg", stockMinimoKg);
            p.Add("@IdNuevo", dbType: DbType.Int32, direction: ParameterDirection.Output);

            conn.Execute("dbo.sp_InventarioItem_Insertar", p, commandType: CommandType.StoredProcedure);
            return (true, "Insumo agregado al inventario.");
        }
        catch (SqlException ex)
        {
            return (false, MensajeSql(ex));
        }
    }

    public (bool exito, string mensaje) AgregarStock(int itemId, decimal cantidadKg)
    {
        if (cantidadKg <= 0)
        {
            return (false, "La cantidad debe ser mayor a cero.");
        }

        try
        {
            using var conn = _db.CreateConnection();
            conn.Open();
            var p = new DynamicParameters();
            p.Add("@Id", itemId);
            p.Add("@CantidadKg", cantidadKg);
            p.Add("@FilasAfectadas", dbType: DbType.Int32, direction: ParameterDirection.Output);

            conn.Execute("dbo.sp_InventarioItem_AgregarStock", p, commandType: CommandType.StoredProcedure);
            if (p.Get<int>("@FilasAfectadas") == 0)
            {
                return (false, "Insumo no encontrado.");
            }

            return (true, "Stock actualizado correctamente.");
        }
        catch (SqlException ex)
        {
            return (false, MensajeSql(ex));
        }
    }

    public (bool exito, string mensaje) DescontarStock(int itemId, decimal cantidadKg)
    {
        if (cantidadKg <= 0)
        {
            return (false, "La cantidad debe ser mayor a cero.");
        }

        try
        {
            using var conn = _db.CreateConnection();
            conn.Open();
            var p = new DynamicParameters();
            p.Add("@Id", itemId);
            p.Add("@CantidadKg", cantidadKg);
            p.Add("@FilasAfectadas", dbType: DbType.Int32, direction: ParameterDirection.Output);

            conn.Execute("dbo.sp_InventarioItem_DescontarStock", p, commandType: CommandType.StoredProcedure);
            if (p.Get<int>("@FilasAfectadas") == 0)
            {
                return (false, "Stock insuficiente.");
            }

            return (true, "Stock descontado correctamente.");
        }
        catch (SqlException ex)
        {
            return (false, MensajeSql(ex));
        }
    }

    private static string MensajeSql(SqlException ex)
        => $"Error de base de datos: {ex.Message}";
}
