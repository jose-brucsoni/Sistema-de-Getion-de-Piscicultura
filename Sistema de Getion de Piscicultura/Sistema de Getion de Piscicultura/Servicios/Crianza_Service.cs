using Dapper;
using Microsoft.Data.SqlClient;
using Sistema_de_Getion_de_Piscicultura.Infraestructura;

namespace Sistema_de_Getion_de_Piscicultura.Servicios;

public class Crianza_Service
{
    private readonly IDbConnectionFactory _db;

    public Crianza_Service(IDbConnectionFactory db)
    {
        _db = db;
    }

    public List<LoteCrianzaDto> ObtenerLotesActivos()
    {
        const string sql = """
            SELECT
                l.Id AS IdLote,
                l.Codigo,
                l.EspecieId,
                l.EstanqueId,
                l.FaseActual,
                l.CantidadActual,
                l.FechaSiembra
            FROM dbo.Lotes l
            WHERE l.Estado = 1
            ORDER BY l.Codigo;
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.Query<LoteCrianzaDto>(sql).ToList();
    }

    public List<MortalidadDto> ObtenerMortalidadReciente(int top = 20)
    {
        const string sql = """
            SELECT TOP (@Top)
                m.IdMortalidad,
                m.IdLote,
                m.FechaRegistro,
                m.Cantidad,
                m.Causa,
                m.Observacion
            FROM dbo.Mortalidad m
            ORDER BY m.FechaRegistro DESC;
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.Query<MortalidadDto>(sql, new { Top = top }).ToList();
    }

    public List<MuestreoDto> ObtenerMuestreosRecientes(int top = 20)
    {
        const string sql = """
            SELECT TOP (@Top)
                m.IdMuestreo,
                m.IdLote,
                m.FechaRegistro,
                m.CantidadMuestra,
                m.PesoPromedio,
                m.Dispersion
            FROM dbo.Muestreo m
            ORDER BY m.FechaRegistro DESC;
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.Query<MuestreoDto>(sql, new { Top = top }).ToList();
    }

    public List<TrasladoDto> ObtenerTrasladosRecientes(int top = 20)
    {
        const string sql = """
            SELECT TOP (@Top)
                t.IdTraslado,
                t.IdLote,
                t.IdEstanqueOrigen,
                t.IdEstanqueDestino,
                t.IdFaseOrigen,
                t.IdFaseDestino,
                t.FechaTraslado,
                t.CantidadTrasladada,
                t.MortalidadEnTraslado,
                t.PesoPromedioGr
            FROM dbo.TrasladoLote t
            ORDER BY t.FechaTraslado DESC;
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.Query<TrasladoDto>(sql, new { Top = top }).ToList();
    }

    public List<BiomasaDto> ObtenerBiomasaReciente(int top = 20)
    {
        const string sql = """
            SELECT TOP (@Top)
                b.IdRegistro,
                b.IdLote,
                b.FechaRegistro,
                b.CantidadPeces,
                b.BiomasaEstimadaKg
            FROM dbo.RegistroBiomasa b
            ORDER BY b.FechaRegistro DESC;
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.Query<BiomasaDto>(sql, new { Top = top }).ToList();
    }

    public List<TratamientoDto> ObtenerTratamientosRecientes(int top = 20)
    {
        const string sql = """
            SELECT TOP (@Top)
                t.IdTratamiento,
                t.IdLote,
                t.FechaAplicacion,
                t.Tipo,
                t.Dosis,
                t.Observacion
            FROM dbo.Tratamiento t
            ORDER BY t.FechaAplicacion DESC;
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.Query<TratamientoDto>(sql, new { Top = top }).ToList();
    }

    public (bool exito, string mensaje) RegistrarMortalidad(int idLote, DateTime fechaRegistro, int cantidad, string? causa, string? observacion)
    {
        if (idLote <= 0) return (false, "Seleccione un lote válido.");
        if (cantidad <= 0) return (false, "La cantidad debe ser mayor a cero.");

        using var conn = _db.CreateConnection();
        conn.Open();
        using var tx = conn.BeginTransaction();
        try
        {
            var actual = conn.ExecuteScalar<int?>("SELECT CantidadActual FROM dbo.Lotes WHERE Id = @Id", new { Id = idLote }, tx);
            if (actual is null) return (false, "Lote no encontrado.");
            if (actual.Value < cantidad) return (false, "La mortalidad no puede ser mayor que la cantidad actual del lote.");

            conn.Execute(
                """
                INSERT INTO dbo.Mortalidad (IdLote, IdUsuario, FechaRegistro, Cantidad, Causa, Observacion)
                VALUES (@IdLote, NULL, @FechaRegistro, @Cantidad, @Causa, @Observacion);
                """,
                new { IdLote = idLote, FechaRegistro = fechaRegistro, Cantidad = cantidad, Causa = causa, Observacion = observacion }, tx);

            conn.Execute(
                "UPDATE dbo.Lotes SET CantidadActual = CantidadActual - @Cantidad WHERE Id = @IdLote;",
                new { Cantidad = cantidad, IdLote = idLote }, tx);

            RegistrarAlertaMortalidadSiAplica(conn, tx, idLote, fechaRegistro, actual.Value, cantidad, causa);
            tx.Commit();
            return (true, "Mortalidad registrada y lote actualizado.");
        }
        catch (Exception ex)
        {
            tx.Rollback();
            return (false, $"Error al registrar mortalidad: {ex.Message}");
        }
    }

    public (bool exito, string mensaje) RegistrarMuestreo(int idLote, DateTime fechaRegistro, int cantidadMuestra, decimal pesoPromedio, string? dispersion)
    {
        if (idLote <= 0) return (false, "Seleccione un lote válido.");
        if (cantidadMuestra <= 0) return (false, "La muestra debe ser mayor a cero.");
        if (pesoPromedio <= 0) return (false, "El peso promedio debe ser mayor a cero.");

        using var conn = _db.CreateConnection();
        conn.Open();
        using var tx = conn.BeginTransaction();
        try
        {
            var cantidadActual = conn.ExecuteScalar<int?>("SELECT CantidadActual FROM dbo.Lotes WHERE Id = @Id", new { Id = idLote }, tx);
            if (cantidadActual is null) return (false, "Lote no encontrado.");

            var idMuestreo = conn.ExecuteScalar<int>(
                """
                INSERT INTO dbo.Muestreo (IdLote, IdUsuario, FechaRegistro, CantidadMuestra, PesoPromedio, Dispersion)
                OUTPUT INSERTED.IdMuestreo
                VALUES (@IdLote, NULL, @FechaRegistro, @CantidadMuestra, @PesoPromedio, @Dispersion);
                """,
                new
                {
                    IdLote = idLote,
                    FechaRegistro = fechaRegistro,
                    CantidadMuestra = cantidadMuestra,
                    PesoPromedio = pesoPromedio,
                    Dispersion = dispersion
                }, tx);

            var biomasaKg = Math.Round((cantidadActual.Value * pesoPromedio) / 1000m, 3);
            conn.Execute(
                """
                INSERT INTO dbo.RegistroBiomasa (IdLote, FechaRegistro, CantidadPeces, BiomasaEstimadaKg, IdUsuario, IdMuestreoOrigen, Observaciones)
                VALUES (@IdLote, @FechaRegistro, @CantidadPeces, @BiomasaEstimadaKg, NULL, @IdMuestreoOrigen, @Observaciones);
                """,
                new
                {
                    IdLote = idLote,
                    FechaRegistro = fechaRegistro,
                    CantidadPeces = cantidadActual.Value,
                    BiomasaEstimadaKg = biomasaKg,
                    IdMuestreoOrigen = idMuestreo,
                    Observaciones = "Biomasa estimada automática desde muestreo."
                }, tx);

            tx.Commit();
            return (true, "Muestreo y biomasa registrados correctamente.");
        }
        catch (Exception ex)
        {
            tx.Rollback();
            return (false, $"Error al registrar muestreo: {ex.Message}");
        }
    }

    public (bool exito, string mensaje) RegistrarTraslado(
        int idLote,
        int idEstanqueDestino,
        int idFaseDestino,
        DateTime fechaTraslado,
        int cantidadTrasladada,
        int mortalidadTraslado,
        decimal? pesoPromedioGr)
    {
        if (idLote <= 0) return (false, "Seleccione un lote válido.");
        if (idEstanqueDestino <= 0) return (false, "Seleccione estanque destino.");
        if (idFaseDestino <= 0) return (false, "Seleccione fase destino.");
        if (cantidadTrasladada <= 0) return (false, "La cantidad a trasladar debe ser mayor a cero.");
        if (mortalidadTraslado < 0) return (false, "La mortalidad no puede ser negativa.");
        if (mortalidadTraslado > cantidadTrasladada) return (false, "La mortalidad no puede superar la cantidad trasladada.");

        using var conn = _db.CreateConnection();
        conn.Open();
        using var tx = conn.BeginTransaction();
        try
        {
            var lote = conn.QueryFirstOrDefault<LoteTrasladoRow>(
                "SELECT Id, EstanqueId, FaseActual, CantidadActual FROM dbo.Lotes WHERE Id = @Id",
                new { Id = idLote }, tx);

            if (lote is null) return (false, "Lote no encontrado.");
            if (lote.CantidadActual < cantidadTrasladada) return (false, "Cantidad trasladada supera la población actual.");

            var capacidadDestino = conn.ExecuteScalar<int?>(
                "SELECT CapacidadMaxima FROM dbo.Estanque WHERE IdEstanque = @IdEstanque",
                new { IdEstanque = idEstanqueDestino }, tx);
            if (capacidadDestino is null) return (false, "Estanque destino no encontrado.");

            var ocupacionDestino = conn.ExecuteScalar<int>(
                "SELECT ISNULL(SUM(CantidadActual), 0) FROM dbo.Lotes WHERE EstanqueId = @IdEstanque AND Estado = 1",
                new { IdEstanque = idEstanqueDestino }, tx);

            var pecesNetos = cantidadTrasladada - mortalidadTraslado;
            if (ocupacionDestino + pecesNetos > capacidadDestino.Value)
            {
                return (false, "El estanque destino no tiene capacidad suficiente.");
            }

            conn.Execute(
                """
                INSERT INTO dbo.TrasladoLote
                (IdLote, IdEstanqueOrigen, IdEstanqueDestino, IdFaseOrigen, IdFaseDestino, FechaTraslado, CantidadTrasladada, MortalidadEnTraslado, PesoPromedioGr, IdUsuario, Observaciones)
                VALUES
                (@IdLote, @IdEstanqueOrigen, @IdEstanqueDestino, @IdFaseOrigen, @IdFaseDestino, @FechaTraslado, @CantidadTrasladada, @MortalidadEnTraslado, @PesoPromedioGr, NULL, NULL);
                """,
                new
                {
                    IdLote = idLote,
                    IdEstanqueOrigen = lote.EstanqueId,
                    IdEstanqueDestino = idEstanqueDestino,
                    IdFaseOrigen = lote.FaseActual,
                    IdFaseDestino = idFaseDestino,
                    FechaTraslado = fechaTraslado,
                    CantidadTrasladada = cantidadTrasladada,
                    MortalidadEnTraslado = mortalidadTraslado,
                    PesoPromedioGr = pesoPromedioGr
                }, tx);

            conn.Execute(
                """
                UPDATE dbo.Lotes
                SET EstanqueId = @IdEstanqueDestino,
                    FaseActual = @IdFaseDestino,
                    CantidadActual = @CantidadActualFinal
                WHERE Id = @IdLote;
                """,
                new
                {
                    IdEstanqueDestino = idEstanqueDestino,
                    IdFaseDestino = idFaseDestino,
                    CantidadActualFinal = lote.CantidadActual - mortalidadTraslado,
                    IdLote = idLote
                }, tx);

            tx.Commit();
            return (true, "Traslado registrado y lote actualizado.");
        }
        catch (Exception ex)
        {
            tx.Rollback();
            return (false, $"Error al registrar traslado: {ex.Message}");
        }
    }

    public (bool exito, string mensaje) RegistrarTratamiento(
        int idLote,
        DateTime fechaAplicacion,
        string tipo,
        decimal dosis,
        string? observacion)
    {
        if (idLote <= 0) return (false, "Seleccione un lote válido.");
        if (string.IsNullOrWhiteSpace(tipo)) return (false, "El tipo de tratamiento es obligatorio.");
        if (dosis <= 0) return (false, "La dosis debe ser mayor a cero.");

        using var conn = _db.CreateConnection();
        conn.Open();

        var existe = conn.ExecuteScalar<int>("SELECT COUNT(1) FROM dbo.Lotes WHERE Id = @Id", new { Id = idLote });
        if (existe == 0) return (false, "Lote no encontrado.");

        conn.Execute(
            """
            INSERT INTO dbo.Tratamiento (IdLote, IdUsuario, FechaAplicacion, Tipo, Dosis, Observacion)
            VALUES (@IdLote, NULL, @FechaAplicacion, @Tipo, @Dosis, @Observacion);
            """,
            new
            {
                IdLote = idLote,
                FechaAplicacion = fechaAplicacion,
                Tipo = tipo.Trim(),
                Dosis = dosis,
                Observacion = observacion
            });

        return (true, "Tratamiento registrado correctamente.");
    }

    private sealed class LoteTrasladoRow
    {
        public int Id { get; set; }
        public int EstanqueId { get; set; }
        public int FaseActual { get; set; }
        public int CantidadActual { get; set; }
    }

    private static void RegistrarAlertaMortalidadSiAplica(
        SqlConnection conn,
        SqlTransaction tx,
        int idLote,
        DateTime fechaRegistro,
        int cantidadAntes,
        int cantidadMuerta,
        string? causa)
    {
        if (cantidadAntes <= 0)
        {
            return;
        }

        if (conn.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM sys.tables WHERE name = 'Alerta' AND schema_id = SCHEMA_ID('dbo')",
                transaction: tx) == 0)
        {
            return;
        }

        var porcentaje = (decimal)cantidadMuerta * 100m / cantidadAntes;
        if (porcentaje < 5m)
        {
            return;
        }

        var causaTexto = string.IsNullOrWhiteSpace(causa) ? "sin causa especificada" : causa.Trim();
        conn.Execute(
            """
            INSERT INTO dbo.Alerta (IdLote, FechaHora, Tipo, Nivel, Mensaje, Atendida)
            VALUES (@IdLote, @FechaHora, @Tipo, @Nivel, @Mensaje, 0);
            """,
            new
            {
                IdLote = idLote,
                FechaHora = fechaRegistro,
                Tipo = "Mortalidad",
                Nivel = porcentaje >= 10m ? "Alta" : "Media",
                Mensaje = $"Mortalidad elevada ({porcentaje:0.##}%): {cantidadMuerta} peces, causa {causaTexto}."
            }, tx);
    }
}

public sealed class LoteCrianzaDto
{
    public int IdLote { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public int EspecieId { get; set; }
    public int EstanqueId { get; set; }
    public int FaseActual { get; set; }
    public int CantidadActual { get; set; }
    public DateTime FechaSiembra { get; set; }
}

public sealed class MortalidadDto
{
    public int IdMortalidad { get; set; }
    public int IdLote { get; set; }
    public DateTime FechaRegistro { get; set; }
    public int Cantidad { get; set; }
    public string? Causa { get; set; }
    public string? Observacion { get; set; }
}

public sealed class MuestreoDto
{
    public int IdMuestreo { get; set; }
    public int IdLote { get; set; }
    public DateTime FechaRegistro { get; set; }
    public int CantidadMuestra { get; set; }
    public decimal PesoPromedio { get; set; }
    public string? Dispersion { get; set; }
}

public sealed class TrasladoDto
{
    public int IdTraslado { get; set; }
    public int IdLote { get; set; }
    public int IdEstanqueOrigen { get; set; }
    public int IdEstanqueDestino { get; set; }
    public int IdFaseOrigen { get; set; }
    public int IdFaseDestino { get; set; }
    public DateTime FechaTraslado { get; set; }
    public int CantidadTrasladada { get; set; }
    public int MortalidadEnTraslado { get; set; }
    public decimal? PesoPromedioGr { get; set; }
}

public sealed class BiomasaDto
{
    public int IdRegistro { get; set; }
    public int IdLote { get; set; }
    public DateTime FechaRegistro { get; set; }
    public int CantidadPeces { get; set; }
    public decimal BiomasaEstimadaKg { get; set; }
}

public sealed class TratamientoDto
{
    public int IdTratamiento { get; set; }
    public int IdLote { get; set; }
    public DateTime FechaAplicacion { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public decimal Dosis { get; set; }
    public string? Observacion { get; set; }
}
