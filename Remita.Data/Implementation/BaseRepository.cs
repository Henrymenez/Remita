using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Remita.Data.Interfaces;
using System.Linq.Expressions;

namespace Remita.Data.Implementation;
public abstract class BaseRepository<T> : IReadRepository<T> where T : class
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(DbContext context)
    {
        _dbContext = context ?? throw new ArgumentException(nameof(context));
        _dbSet = _dbContext.Set<T>();
    }



    public T? Search(params object[] keyValues) => _dbSet.Find(keyValues);

    public T? Single(Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool disableTracking = true)
    {
        IQueryable<T> query = _dbSet;
        if (disableTracking) query = query.AsNoTracking();

        if (include != null) query = include(query);

        var result = query.SingleOrDefault(predicate);
        return result;
    }

    public async Task<T?> SingleAsync(Expression<Func<T, bool>> predicate,
       Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
       bool disableTracking = true)
    {
        IQueryable<T> query = _dbSet;
        if (disableTracking) query = query.AsNoTracking();

        if (include != null) query = include(query);

        var result = await query.SingleOrDefaultAsync(predicate);
        return result;
    }

    public async Task<T?> FirstAsync(Expression<Func<T, bool>>? predicate = null,
      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
      Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
      bool disableTracking = true)
    {
        IQueryable<T> query = _dbSet;
        if (disableTracking) query = query.AsNoTracking();

        if (include != null) query = include(query);

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null)
            return await orderBy(query).FirstOrDefaultAsync();
        return await query.FirstOrDefaultAsync();
    }

    public T? First(Expression<Func<T, bool>>? predicate = null,
       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
       Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
       bool disableTracking = true)
    {
        IQueryable<T> query = _dbSet;
        if (disableTracking) query = query.AsNoTracking();

        if (include != null) query = include(query);

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null)
            return orderBy(query).FirstOrDefault();
        return query.FirstOrDefault();
    }

    public async Task<T?> LastAsync(Expression<Func<T, bool>>? predicate = null,
    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
    Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
    bool disableTracking = true)
    {
        IQueryable<T> query = _dbSet;
        if (disableTracking) query = query.AsNoTracking();

        if (include != null) query = include(query);

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null)
            return await orderBy(query).LastOrDefaultAsync();
        return await query.LastOrDefaultAsync();
    }

    public T? Last(Expression<Func<T, bool>>? predicate = null,
      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
      Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
      bool disableTracking = true)
    {
        IQueryable<T> query = _dbSet;
        if (disableTracking) query = query.AsNoTracking();

        if (include != null) query = include(query);

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null)
            return orderBy(query).LastOrDefault();
        return query.LastOrDefault();
    }

    public IEnumerable<T> GetList(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include, bool disableTracking)
    {
        IQueryable<T> query = _dbSet;
        if (disableTracking) query = query.AsNoTracking();

        if (include != null) query = include(query);

        if (predicate != null) query = query.Where(predicate);

        return orderBy != null
            ? orderBy(query).ToList()
            : query.ToList();
    }

    public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, int? skip, int? take, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include, bool disableTracking)
    {
        IQueryable<T> query = constructQuery(predicate, orderBy, skip, take, include, disableTracking);
        return await query.ToListAsync();
    }

    public IQueryable<T> GetQueryableList(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include, bool disableTracking)
    {
        IQueryable<T> query = _dbSet;
        if (disableTracking) query = query.AsNoTracking();

        if (include != null) query = include(query);

        if (predicate != null) query = query.Where(predicate);

        return orderBy != null
            ? orderBy(query)
            : query;
    }

    private IQueryable<T> constructQuery(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, int? skip, int? take, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include, bool disableTracking)
    {
        IQueryable<T> query = _dbSet;

        if (disableTracking) query = query.AsNoTracking();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (include != null) query = include(query);

        if (skip != null)
        {
            query = query.Skip(skip.Value);
        }

        if (take != null)
        {
            query = query.Take(take.Value);
        }

        return query;
    }
}
