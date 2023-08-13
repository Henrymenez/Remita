using Microsoft.EntityFrameworkCore;

namespace Remita.Data.Interfaces;
public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
{
    TContext Context { get; }

    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

    int SaveChanges();

    Task<int> SaveChangesAsync();
}
