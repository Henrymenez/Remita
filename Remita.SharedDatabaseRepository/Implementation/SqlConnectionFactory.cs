using Remita.SharedDatabaseRepository.Interfaces;
using System.Data.SqlClient;

namespace Remita.SharedDatabaseRepository.Implementation;
public class SqlConnectionFactory : ISqlConnectionFactory
{
    public SqlConnection Create(string connectionString)
    {
        return new SqlConnection(connectionString);
    }
}
