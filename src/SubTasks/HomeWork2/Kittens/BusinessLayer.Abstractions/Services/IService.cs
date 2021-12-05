using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Abstractions.Models;
using BusinessLayer.Abstractions.Validations;

namespace BusinessLayer.Abstractions.Services
{
    public interface IService<TEntity, in TId> where TEntity : IEntity<TId>
    {
        Task<IReadOnlyCollection<TEntity>> Get();
        Task<IOperationResult> Add(TEntity kitten);
        Task<IOperationResult> Update(TEntity kitten);
        Task Delete(TId id);
    }
}