using Dapper;
using Sistema_de_Getion_de_Piscicultura.Infraestructura;

namespace Sistema_de_Getion_de_Piscicultura.Servicios;

public class Catalogos_Service
{
    private readonly IDbConnectionFactory _db;

    public Catalogos_Service(IDbConnectionFactory db)
    {
        _db = db;
    }

    public List<CatalogoDto> ObtenerEspeciesActivas()
    {
        const string sql = """
            SELECT e.IdEspecie AS Id, e.Nombre
            FROM dbo.Especie e
            WHERE e.Activo = 1
            ORDER BY e.Nombre;
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.Query<CatalogoDto>(sql).ToList();
    }

    public List<CatalogoDto> ObtenerEstanquesActivos()
    {
        const string sql = """
            SELECT
                e.IdEstanque AS Id,
                CASE
                    WHEN e.Tipo IS NULL OR LTRIM(RTRIM(e.Tipo)) = N'' THEN e.Codigo
                    ELSE e.Codigo + N' (' + e.Tipo + N')'
                END AS Nombre
            FROM dbo.Estanque e
            WHERE e.Activo = 1
            ORDER BY e.Codigo;
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.Query<CatalogoDto>(sql).ToList();
    }

    public List<CatalogoDto> ObtenerProveedoresActivos()
    {
        const string sql = """
            SELECT p.IdProveedor AS Id, p.Nombre
            FROM dbo.Proveedor p
            WHERE p.Activo = 1
            ORDER BY p.Nombre;
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.Query<CatalogoDto>(sql).ToList();
    }

    public List<EstanqueResumenDto> ObtenerEstanques()
    {
        const string sql = """
            SELECT
                e.IdEstanque,
                e.Codigo,
                e.Tipo,
                e.VolumenM3,
                e.CapacidadMaxima,
                e.Estado,
                e.Activo
            FROM dbo.Estanque e
            ORDER BY e.Codigo;
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.Query<EstanqueResumenDto>(sql).ToList();
    }

    public (bool exito, string mensaje) CrearEstanque(string codigo, string? tipo, decimal? volumenM3, int capacidadMaxima, string? estado)
    {
        if (string.IsNullOrWhiteSpace(codigo))
        {
            return (false, "El código del estanque es obligatorio.");
        }

        if (capacidadMaxima <= 0)
        {
            return (false, "La capacidad máxima debe ser mayor a cero.");
        }

        const string sql = """
            INSERT INTO dbo.Estanque (Codigo, Tipo, VolumenM3, CapacidadMaxima, Estado, Activo)
            VALUES (@Codigo, @Tipo, @VolumenM3, @CapacidadMaxima, @Estado, 1);
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        conn.Execute(sql, new
        {
            Codigo = codigo.Trim(),
            Tipo = string.IsNullOrWhiteSpace(tipo) ? null : tipo.Trim(),
            VolumenM3 = volumenM3,
            CapacidadMaxima = capacidadMaxima,
            Estado = string.IsNullOrWhiteSpace(estado) ? "Activo" : estado.Trim()
        });

        return (true, "Estanque creado correctamente.");
    }

    public (bool exito, string mensaje) ActualizarEstanque(int idEstanque, string codigo, string? tipo, decimal? volumenM3, int capacidadMaxima, string? estado)
    {
        if (idEstanque <= 0)
        {
            return (false, "Seleccione un estanque válido.");
        }

        if (string.IsNullOrWhiteSpace(codigo))
        {
            return (false, "El código del estanque es obligatorio.");
        }

        if (capacidadMaxima <= 0)
        {
            return (false, "La capacidad máxima debe ser mayor a cero.");
        }

        const string sql = """
            UPDATE dbo.Estanque
            SET Codigo = @Codigo,
                Tipo = @Tipo,
                VolumenM3 = @VolumenM3,
                CapacidadMaxima = @CapacidadMaxima,
                Estado = @Estado
            WHERE IdEstanque = @IdEstanque;
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        var filas = conn.Execute(sql, new
        {
            IdEstanque = idEstanque,
            Codigo = codigo.Trim(),
            Tipo = string.IsNullOrWhiteSpace(tipo) ? null : tipo.Trim(),
            VolumenM3 = volumenM3,
            CapacidadMaxima = capacidadMaxima,
            Estado = string.IsNullOrWhiteSpace(estado) ? null : estado.Trim()
        });

        return filas > 0
            ? (true, "Estanque actualizado correctamente.")
            : (false, "No se encontró el estanque a actualizar.");
    }

    public (bool exito, string mensaje) DesactivarEstanque(int idEstanque)
    {
        if (idEstanque <= 0)
        {
            return (false, "Seleccione un estanque válido.");
        }

        const string sql = """
            UPDATE dbo.Estanque
            SET Activo = 0,
                Estado = N'Inactivo'
            WHERE IdEstanque = @IdEstanque;
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        var filas = conn.Execute(sql, new { IdEstanque = idEstanque });
        return filas > 0
            ? (true, "Estanque desactivado correctamente.")
            : (false, "No se encontró el estanque a desactivar.");
    }

    public List<EspecieResumenDto> ObtenerEspecies()
    {
        const string sql = """
            SELECT
                e.IdEspecie,
                e.Nombre,
                e.TempMin,
                e.TempMax,
                e.PhMin,
                e.PhMax,
                e.OxigenoMin,
                e.PesoComercialMin,
                e.PesoComercialMax,
                e.Activo
            FROM dbo.Especie e
            ORDER BY e.Nombre;
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.Query<EspecieResumenDto>(sql).ToList();
    }

    public (bool exito, string mensaje) CrearEspecie(
        string nombre,
        decimal tempMin,
        decimal tempMax,
        decimal phMin,
        decimal phMax,
        decimal oxigenoMin,
        decimal pesoMin,
        decimal pesoMax)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            return (false, "El nombre de la especie es obligatorio.");
        }

        if (tempMin > tempMax || phMin > phMax || pesoMin > pesoMax)
        {
            return (false, "Revise los rangos: mínimo no puede ser mayor que máximo.");
        }

        const string sql = """
            INSERT INTO dbo.Especie
            (Nombre, TempMin, TempMax, PhMin, PhMax, OxigenoMin, PesoComercialMin, PesoComercialMax, Activo)
            VALUES
            (@Nombre, @TempMin, @TempMax, @PhMin, @PhMax, @OxigenoMin, @PesoComercialMin, @PesoComercialMax, 1);
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        conn.Execute(sql, new
        {
            Nombre = nombre.Trim(),
            TempMin = tempMin,
            TempMax = tempMax,
            PhMin = phMin,
            PhMax = phMax,
            OxigenoMin = oxigenoMin,
            PesoComercialMin = pesoMin,
            PesoComercialMax = pesoMax
        });

        return (true, "Especie creada correctamente.");
    }

    public (bool exito, string mensaje) ActualizarEspecie(
        int idEspecie,
        string nombre,
        decimal tempMin,
        decimal tempMax,
        decimal phMin,
        decimal phMax,
        decimal oxigenoMin,
        decimal pesoMin,
        decimal pesoMax)
    {
        if (idEspecie <= 0)
        {
            return (false, "Seleccione una especie válida.");
        }

        if (string.IsNullOrWhiteSpace(nombre))
        {
            return (false, "El nombre de la especie es obligatorio.");
        }

        if (tempMin > tempMax || phMin > phMax || pesoMin > pesoMax)
        {
            return (false, "Revise los rangos: mínimo no puede ser mayor que máximo.");
        }

        const string sql = """
            UPDATE dbo.Especie
            SET Nombre = @Nombre,
                TempMin = @TempMin,
                TempMax = @TempMax,
                PhMin = @PhMin,
                PhMax = @PhMax,
                OxigenoMin = @OxigenoMin,
                PesoComercialMin = @PesoComercialMin,
                PesoComercialMax = @PesoComercialMax
            WHERE IdEspecie = @IdEspecie;
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        var filas = conn.Execute(sql, new
        {
            IdEspecie = idEspecie,
            Nombre = nombre.Trim(),
            TempMin = tempMin,
            TempMax = tempMax,
            PhMin = phMin,
            PhMax = phMax,
            OxigenoMin = oxigenoMin,
            PesoComercialMin = pesoMin,
            PesoComercialMax = pesoMax
        });

        return filas > 0
            ? (true, "Especie actualizada correctamente.")
            : (false, "No se encontró la especie a actualizar.");
    }

    public (bool exito, string mensaje) DesactivarEspecie(int idEspecie)
    {
        if (idEspecie <= 0)
        {
            return (false, "Seleccione una especie válida.");
        }

        const string sql = "UPDATE dbo.Especie SET Activo = 0 WHERE IdEspecie = @IdEspecie;";
        using var conn = _db.CreateConnection();
        conn.Open();
        var filas = conn.Execute(sql, new { IdEspecie = idEspecie });
        return filas > 0
            ? (true, "Especie desactivada correctamente.")
            : (false, "No se encontró la especie a desactivar.");
    }

    public List<ProveedorResumenDto> ObtenerProveedores()
    {
        const string sql = """
            SELECT
                p.IdProveedor,
                p.Nombre,
                p.Contacto,
                p.CertificacionSanitaria,
                p.Activo
            FROM dbo.Proveedor p
            ORDER BY p.Nombre;
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.Query<ProveedorResumenDto>(sql).ToList();
    }

    public (bool exito, string mensaje) CrearProveedor(string nombre, string? contacto, string? certificacion)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            return (false, "El nombre del proveedor es obligatorio.");
        }

        const string sql = """
            INSERT INTO dbo.Proveedor (Nombre, CertificacionSanitaria, Contacto, Activo)
            VALUES (@Nombre, @CertificacionSanitaria, @Contacto, 1);
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        conn.Execute(sql, new
        {
            Nombre = nombre.Trim(),
            CertificacionSanitaria = string.IsNullOrWhiteSpace(certificacion) ? null : certificacion.Trim(),
            Contacto = string.IsNullOrWhiteSpace(contacto) ? null : contacto.Trim()
        });

        return (true, "Proveedor creado correctamente.");
    }

    public (bool exito, string mensaje) ActualizarProveedor(int idProveedor, string nombre, string? contacto, string? certificacion)
    {
        if (idProveedor <= 0)
        {
            return (false, "Seleccione un proveedor válido.");
        }

        if (string.IsNullOrWhiteSpace(nombre))
        {
            return (false, "El nombre del proveedor es obligatorio.");
        }

        const string sql = """
            UPDATE dbo.Proveedor
            SET Nombre = @Nombre,
                CertificacionSanitaria = @CertificacionSanitaria,
                Contacto = @Contacto
            WHERE IdProveedor = @IdProveedor;
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        var filas = conn.Execute(sql, new
        {
            IdProveedor = idProveedor,
            Nombre = nombre.Trim(),
            CertificacionSanitaria = string.IsNullOrWhiteSpace(certificacion) ? null : certificacion.Trim(),
            Contacto = string.IsNullOrWhiteSpace(contacto) ? null : contacto.Trim()
        });

        return filas > 0
            ? (true, "Proveedor actualizado correctamente.")
            : (false, "No se encontró el proveedor a actualizar.");
    }

    public (bool exito, string mensaje) DesactivarProveedor(int idProveedor)
    {
        if (idProveedor <= 0)
        {
            return (false, "Seleccione un proveedor válido.");
        }

        const string sql = "UPDATE dbo.Proveedor SET Activo = 0 WHERE IdProveedor = @IdProveedor;";
        using var conn = _db.CreateConnection();
        conn.Open();
        var filas = conn.Execute(sql, new { IdProveedor = idProveedor });
        return filas > 0
            ? (true, "Proveedor desactivado correctamente.")
            : (false, "No se encontró el proveedor a desactivar.");
    }

    public List<RolDto> ObtenerRolesActivos()
    {
        const string sql = """
            SELECT r.IdRol, r.Nombre
            FROM dbo.Rol r
            WHERE r.Activo = 1
            ORDER BY r.Nombre;
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.Query<RolDto>(sql).ToList();
    }

    public List<UsuarioResumenDto> ObtenerUsuariosConRol()
    {
        const string sql = """
            SELECT
                u.IdUsuario,
                u.Username,
                u.Correo,
                u.Activo,
                u.CreatedAt,
                ISNULL(r.Nombre, N'Sin rol') AS RolNombre
            FROM dbo.Usuario u
            LEFT JOIN dbo.Rol r ON r.IdRol = u.IdRol
            ORDER BY u.Username;
            """;

        using var conn = _db.CreateConnection();
        conn.Open();
        return conn.Query<UsuarioResumenDto>(sql).ToList();
    }
}

public sealed record CatalogoDto(int Id, string Nombre);
public sealed record RolDto(int IdRol, string Nombre);

public sealed class EstanqueResumenDto
{
    public int IdEstanque { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string? Tipo { get; set; }
    public decimal? VolumenM3 { get; set; }
    public int CapacidadMaxima { get; set; }
    public string? Estado { get; set; }
    public bool Activo { get; set; }
}

public sealed class EspecieResumenDto
{
    public int IdEspecie { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal TempMin { get; set; }
    public decimal TempMax { get; set; }
    public decimal PhMin { get; set; }
    public decimal PhMax { get; set; }
    public decimal OxigenoMin { get; set; }
    public decimal PesoComercialMin { get; set; }
    public decimal PesoComercialMax { get; set; }
    public bool Activo { get; set; }
}

public sealed class ProveedorResumenDto
{
    public int IdProveedor { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Contacto { get; set; }
    public string? CertificacionSanitaria { get; set; }
    public bool Activo { get; set; }
}

public sealed class UsuarioResumenDto
{
    public int IdUsuario { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public bool Activo { get; set; }
    public DateTime CreatedAt { get; set; }
    public string RolNombre { get; set; } = "Sin rol";
}
