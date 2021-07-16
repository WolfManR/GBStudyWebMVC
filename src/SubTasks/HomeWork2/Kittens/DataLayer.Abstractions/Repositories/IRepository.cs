using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Abstractions.Entities;

namespace DataLayer.Abstractions.Repositories
{
    public interface IRepository<TEntity, in TId> where TEntity : IEntity<TId>
    {
        Task<IEnumerable<TEntity>> Get();

        Task Add(TEntity kitten);
        Task Delete(TId id);
        Task Update(TEntity kitten);
    }
}