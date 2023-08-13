using Remita.SharedDatabaseRepository.Interfaces;

namespace Remita.SharedDatabaseRepository.Implementation;
public class RemitaDbRepository : IRemitaDbRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly string _connectonString;

    public RemitaDbRepository(string connectionString, ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _connectonString = connectionString;
    }
}
