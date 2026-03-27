using Microsoft.Data.SqlClient;

namespace Sistema_de_Getion_de_Piscicultura.Infraestructura;

public sealed class SqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Configure ConnectionStrings:DefaultConnection en appsettings.json.");
    }

    public SqlConnection CreateConnection() => new(_connectionString);
}
