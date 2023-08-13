using Microsoft.EntityFrameworkCore;
using Remita.Data.Interfaces;

namespace Remita.Data.Implementation;
public class RepositoryReadOnly<T> : BaseRepository<T>, IRepositoryReadOnly<T> where T : class
{
    public RepositoryReadOnly(DbContext context) : base(context)
    {
    }
}
