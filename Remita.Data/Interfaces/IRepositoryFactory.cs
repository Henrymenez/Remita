namespace Remita.Data.Interfaces;
public interface IRepositoryFactory
{
    IRepository<T> GetRepository<T>() where T : class;
    IRepositoryReadOnly<T> GetReadOnlyRepository<T>() where T : class;
}
