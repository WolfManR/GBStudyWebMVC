using DataLayer.Abstractions.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.EF;
using DataLayer.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Mapster;
using dbEntities = DataBase.Abstractions.Entities;

namespace DataLayer.Repository
{
    public abstract class KittensContextRepository<TEntity, TDbEntity, TId> : IRepository<TEntity, TId> where TEntity : IEntity<TId> where TDbEntity : class, dbEntities::IEntity<TId>
    {
        protected readonly IDbContextFactory<KittensContext> ContextFactory;

        protected KittensContextRepository(IDbContextFactory<KittensContext> contextFactory) => ContextFactory = contextFactory;

        public async Task<IEnumerable<TEntity>> Get()
        {
            await using var context = ContextFactory.CreateDbContext();
            return await context.Set<TDbEntity>().ProjectToType<TEntity>().ToListAsync();
        }

        public async Task Add(TEntity entity)
        {
            await using var context = ContextFactory.CreateDbContext();
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(TId id)
        {
            await using var context = ContextFactory.CreateDbContext();
            var entity = await context.Set<TDbEntity>().FirstOrDefaultAsync(e => Equals(e.Id, id));
            if (entity is null) return;
            context.Entry(entity).State = EntityState.Deleted;
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Update(TEntity entity)
        {
            await using var context = ContextFactory.CreateDbContext();
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}