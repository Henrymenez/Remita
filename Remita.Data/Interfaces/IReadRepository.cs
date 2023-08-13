using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Remita.Data.Interfaces;
public interface IReadRepository<T> where T : class
{

    T? Search(params object[] keyValues);

    T? Single(Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool disableTracking = true);

    Task<T?> SingleAsync(Expression<Func<T, bool>> predicate,
       Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
       bool disableTracking = true);

    T? Last(Expression<Func<T, bool>>? predicate = null,
     Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
     Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
     bool disableTracking = true);

    Task<T?> LastAsync(Expression<Func<T, bool>>? predicate = null,
    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
    Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
    bool disableTracking = true);

    T? First(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool disableTracking = true);

    Task<T?> FirstAsync(Expression<Func<T, bool>>? predicate = null,
      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
      Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
      bool disableTracking = true);

    IEnumerable<T> GetList(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,

        bool disableTracking = true);

    Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate,
                                      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                      int? skip = null,
                                      int? take = null,
                                      Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                      bool disableTracking = true);

    IQueryable<T> GetQueryableList(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool disableTracking = true);
}
