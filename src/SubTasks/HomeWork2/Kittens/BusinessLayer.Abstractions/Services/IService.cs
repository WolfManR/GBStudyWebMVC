using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Abstractions.Models;

namespace BusinessLayer.Abstractions.Services
{
    public interface IService<TEntity, in TId> where TEntity : IEntity<TId>
    {
        Task<IReadOnlyCollection<TEntity>> Get();
        Task Add(TEntity kitten);
        Task Update(TEntity kitten);
        Task Delete(TId id);
    }
}