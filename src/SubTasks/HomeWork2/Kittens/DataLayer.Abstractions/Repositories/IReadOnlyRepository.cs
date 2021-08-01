using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Abstractions.Repositories
{
    public interface IReadOnlyRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> Get();
    }
}