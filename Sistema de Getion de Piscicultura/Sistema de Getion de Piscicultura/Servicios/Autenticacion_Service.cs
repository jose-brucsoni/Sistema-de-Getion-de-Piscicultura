using Dapper;
using Sistema_de_Getion_de_Piscicultura.Infraestructura;
using Sistema_de_Getion_de_Piscicultura.Seguridad;

namespace Sistema_de_Getion_de_Piscicultura.Servicios;

public class Autenticacion_Service
{
    private readonly IDbConnectionFactory _db;

    public Autenticacion_Service(IDbConnectionFactory db)
    {
        _db = db;
    }

    public async Task<UsuarioSesion?> ValidarCredencialesAsync(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return null;
        }

        using var conn = _db.CreateConnection();
        await conn.OpenAsync();

        const string sql = """
            SELECT TOP (1)
                u.IdUsuario,
                u.Username,
                u.PasswordHash,
                u.Activo,
                ISNULL(r.Nombre, N'Usuario') AS RolNombre
            FROM dbo.Usuario u
            LEFT JOIN dbo.Rol r ON r.IdRol = u.IdRol
            WHERE u.Username = @Username OR u.Correo = @Username
            """;

        var row = await conn.QueryFirstOrDefaultAsync<UsuarioAuthRow>(sql, new { Username = username.Trim() });
        if (row is null || !row.Activo)
        {
            return null;
        }

        if (!PasswordHashing.Verify(password, row.PasswordHash))
        {
            return null;
        }

        // Migra a hash PBKDF2 si estaba en formato legado (texto plano).
        if (!row.PasswordHash.StartsWith("PBKDF2$", StringComparison.Ordinal))
        {
            var nuevoHash = PasswordHashing.Hash(password);
            await conn.ExecuteAsync(
                "UPDATE dbo.Usuario SET PasswordHash = @PasswordHash WHERE IdUsuario = @IdUsuario",
                new { PasswordHash = nuevoHash, row.IdUsuario });
        }

        return new UsuarioSesion(row.IdUsuario, row.Username, row.RolNombre);
    }

    private sealed class UsuarioAuthRow
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public string RolNombre { get; set; } = "Usuario";
    }
}

public sealed record UsuarioSesion(int IdUsuario, string Username, string RolNombre);
