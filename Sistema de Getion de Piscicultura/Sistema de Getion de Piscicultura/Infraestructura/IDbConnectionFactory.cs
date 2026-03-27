using Microsoft.Data.SqlClient;

namespace Sistema_de_Getion_de_Piscicultura.Infraestructura;

public interface IDbConnectionFactory
{
    SqlConnection CreateConnection();
}
