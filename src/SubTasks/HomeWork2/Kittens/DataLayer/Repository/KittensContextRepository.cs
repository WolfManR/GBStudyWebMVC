using DataLayer.Abstractions.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.EF;
using DataLayer.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace DataLayer.Repository
{
    public abstract class KittensContextRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : IEntity<TId>
    {
        protected readonly IDbContextFactory<KittensContext> ContextFactory;

        protected KittensContextRepository(IDbContextFactory<KittensContext> contextFactory) => ContextFactory = contextFactory;

        public async Task<IEnumerable<TEntity>> Get()
        {
            await using var context = ContextFactory.CreateDbContext();
            return await context.Kittens.ProjectToType<TEntity>().ToListAsync();
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
            var entity = await context.Kittens.FirstOrDefaultAsync(e => Equals(e.Id, id));
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