using Microsoft.EntityFrameworkCore;
using Remita.Data.Interfaces;

namespace Remita.Data.Implementation;
public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>
    where TContext : DbContext
{
    private Dictionary<Type, object>? _repositories;

    public UnitOfWork(TContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        if (_repositories == null) _repositories = new Dictionary<Type, object>();

        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(Context);
        return (IRepository<TEntity>)_repositories[type];
    }

    public IRepositoryReadOnly<TEntity> GetReadOnlyRepository<TEntity>() where TEntity : class
    {
        if (_repositories == null) _repositories = new Dictionary<Type, object>();

        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type)) _repositories[type] = new RepositoryReadOnly<TEntity>(Context);
        return (IRepositoryReadOnly<TEntity>)_repositories[type];
    }

    public TContext Context { get; }

    public int SaveChanges()
    {
        return Context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await Context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Context?.Dispose();
    }


}
