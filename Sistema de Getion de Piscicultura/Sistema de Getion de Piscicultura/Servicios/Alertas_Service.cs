using Dapper;
using System.Data;
using Sistema_de_Getion_de_Piscicultura.Infraestructura;

namespace Sistema_de_Getion_de_Piscicultura.Servicios;

public class Alertas_Service
{
    private readonly IDbConnectionFactory _db;

    public Alertas_Service(IDbConnectionFactory db)
    {
        _db = db;
    }

    public List<AlertaDto> ObtenerAlertas(bool? atendida = null, int top = 100)
    {
        using var conn = _db.CreateConnection();
        conn.Open();

        if (!ExisteTablaAlerta(conn))
        {
            return new List<AlertaDto>();
        }

        const string sql = """
            SELECT TOP (@Top)
                a.IdAlerta,
                a.IdLote,
                a.FechaHora,
                a.Tipo,
                a.Nivel,
                a.Mensaje,
                a.Atendida
            FROM dbo.Alerta a
            WHERE (@Atendida IS NULL OR a.Atendida = @Atendida)
            ORDER BY a.Atendida ASC, a.FechaHora DESC;
            """;

        return conn.Query<AlertaDto>(sql, new { Top = top, Atendida = atendida }).ToList();
    }

    public (bool exito, string mensaje) MarcarAtendida(int idAlerta)
    {
        if (idAlerta <= 0)
        {
            return (false, "Seleccione una alerta valida.");
        }

        using var conn = _db.CreateConnection();
        conn.Open();

        if (!ExisteTablaAlerta(conn))
        {
            return (false, "La tabla dbo.Alerta no existe. Ejecute el script 03_CrearTablaAlerta_Modelos.sql.");
        }

        const string sql = """
            UPDATE dbo.Alerta
            SET Atendida = 1
            WHERE IdAlerta = @IdAlerta;
            """;

        var filas = conn.Execute(sql, new { IdAlerta = idAlerta });
        if (filas == 0)
        {
            return (false, "No se encontro la alerta.");
        }

        return (true, "Alerta marcada como atendida.");
    }

    private static bool ExisteTablaAlerta(IDbConnection conn)
        => conn.ExecuteScalar<int>("SELECT COUNT(1) FROM sys.tables WHERE name = 'Alerta' AND schema_id = SCHEMA_ID('dbo')") > 0;
}

public sealed class AlertaDto
{
    public int IdAlerta { get; set; }
    public int? IdLote { get; set; }
    public DateTime FechaHora { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string? Nivel { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public bool Atendida { get; set; }
}
