using System.Data.SqlClient;

namespace Remita.SharedDatabaseRepository.Interfaces;
public interface ISqlConnectionFactory
{
    SqlConnection Create(string connectionString);
}
