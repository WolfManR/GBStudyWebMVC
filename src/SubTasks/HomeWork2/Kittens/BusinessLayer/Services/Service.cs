using System.Collections.Generic;
using BusinessLayer.Abstractions.Services;
using System.Threading.Tasks;
using BusinessLayer.Abstractions.Models;
using DataLayer.Abstractions.Repositories;
using MapsterMapper;
using dataEntities = DataLayer.Abstractions.Entities;

namespace BusinessLayer.Services
{
    public class Service<TEntity, TDataEntity, TId, TRepository> : IService<TEntity, TId> 
        where TEntity : IEntity<TId> 
        where TDataEntity : dataEntities::IEntity<TId> 
        where TRepository : IRepository<TDataEntity, TId>
    {
        protected readonly TRepository Repository;
        protected readonly IMapper Mapper;

        public Service(TRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public async Task<IReadOnlyCollection<TEntity>> Get()
        {
            var data = await Repository.Get();
            return Mapper.Map<List<TEntity>>(data);
        }

        public Task Add(TEntity entity)
        {
            return Repository.Add(Mapper.Map<TDataEntity>(entity));
        }

        public Task Update(TEntity entity)
        {
            return Repository.Update(Mapper.Map<TDataEntity>(entity));
        }

        public Task Delete(TId id)
        {
            return Repository.Delete(id);
        }
    }
}