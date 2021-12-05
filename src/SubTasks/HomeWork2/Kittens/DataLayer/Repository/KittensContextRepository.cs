using DataLayer.Abstractions.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.EF;
using DataLayer.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Mapster;
using MapsterMapper;
using dbEntities = DataBase.Abstractions.Entities;

namespace DataLayer.Repository
{
    public abstract class KittensContextRepository<TEntity, TDbEntity, TId> : IRepository<TEntity, TId> where TEntity : IEntity<TId> where TDbEntity : class, dbEntities::IEntity<TId>
    {
        protected readonly KittensContext Context;
        protected readonly IMapper Mapper;

        protected KittensContextRepository(KittensContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            return await Context.Set<TDbEntity>().AsNoTracking().ProjectToType<TEntity>().ToListAsync();
        }

        public async Task Add(TEntity entity)
        {
            var entry = Mapper.Map<TDbEntity>(entity);
            await Context.AddAsync(entry);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(TId id)
        {
            var entity = await Context.Set<TDbEntity>().FirstOrDefaultAsync(e => Equals(e.Id, id));
            if (entity is null) return;
            Context.Entry(entity).State = EntityState.Deleted;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Update(TEntity entity)
        {
            var entry = Mapper.Map<TDbEntity>(entity);
            Context.Update(entry);
            await Context.SaveChangesAsync();
        }
    }
}