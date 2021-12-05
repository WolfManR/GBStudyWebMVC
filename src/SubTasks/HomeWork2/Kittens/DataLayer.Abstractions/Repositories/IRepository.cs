using System.Threading.Tasks;
using DataLayer.Abstractions.Entities;

namespace DataLayer.Abstractions.Repositories
{
    public interface IRepository<TEntity, in TId> : IReadOnlyRepository<TEntity> where TEntity : IEntity<TId>
    {
        Task Add(TEntity kitten);
        Task Delete(TId id);
        Task Update(TEntity kitten);
    }
}