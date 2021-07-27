using System.Collections.Generic;
using BusinessLayer.Abstractions.Services;
using System.Threading.Tasks;
using BusinessLayer.Abstractions.Models;
using BusinessLayer.Abstractions.Validations;
using DataLayer.Abstractions.Repositories;
using MapsterMapper;
using dataEntities = DataLayer.Abstractions.Entities;

namespace BusinessLayer.Services
{
    public class Service<TEntity, TDataEntity, TId, TRepository> : IService<TEntity, TId> 
        where TEntity : class, IEntity<TId> 
        where TDataEntity : dataEntities::IEntity<TId> 
        where TRepository : IRepository<TDataEntity, TId>
    {
        protected readonly TRepository Repository;
        protected readonly IMapper Mapper;
        private readonly IValidationService<TEntity> _entityValidation;

        public Service(TRepository repository, IMapper mapper, IValidationService<TEntity> entityValidation)
        {
            Repository = repository;
            Mapper = mapper;
            _entityValidation = entityValidation;
        }

        public async Task<IReadOnlyCollection<TEntity>> Get()
        {
            var data = await Repository.Get();
            return Mapper.Map<List<TEntity>>(data);
        }

        public async Task<IOperationResult> Add(TEntity entity)
        {
            var failures = _entityValidation.ValidateEntry(entity);
            if (failures.Count > 0)
            {
                return new OperationResult(failures, false);
            }
            await Repository.Add(Mapper.Map<TDataEntity>(entity));
            return new OperationResult(failures, true);
        }

        public async Task<IOperationResult> Update(TEntity entity)
        {
            var failures = _entityValidation.ValidateEntry(entity);
            if (failures.Count > 0)
            {
                return new OperationResult(failures, false);
            }
            await Repository.Update(Mapper.Map<TDataEntity>(entity));
            return new OperationResult(failures, true);
        }

        public Task Delete(TId id)
        {
            return Repository.Delete(id);
        }
    }
}