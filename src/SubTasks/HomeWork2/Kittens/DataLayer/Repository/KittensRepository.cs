using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DataBase.EF;

using DataLayer.Abstractions.Entities;
using DataLayer.Abstractions.Filters;
using DataLayer.Abstractions.Repositories;
using DataLayer.Filters;

using Mapster;

using Microsoft.EntityFrameworkCore;

using KittenSearchFilter = DataLayer.Filters.KittenSearchFilter;

namespace DataLayer.Repository
{
    public class KittensRepository : IKittensRepository
    {
        private readonly IDbContextFactory<KittensContext> _contextFactory;

        public KittensRepository(IDbContextFactory<KittensContext> contextFactory) => _contextFactory = contextFactory;

        public async Task<List<Kitten>> Get()
        {
            await using var context = _contextFactory.CreateDbContext();
            return await context.Kittens.ProjectToType<Kitten>().ToListAsync();
        }

        public async Task Add(Kitten kitten)
        {
            await using var context = _contextFactory.CreateDbContext();
            await context.AddAsync(kitten);
            await context.SaveChangesAsync();
        }

        public async Task Update(Kitten kitten)
        {
            await using var context = _contextFactory.CreateDbContext();
            context.Update(kitten);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await using var context = _contextFactory.CreateDbContext();
            var entity = await context.Kittens.FirstOrDefaultAsync(e => Equals(e.Id, id));
            if(entity is null) return;
            context.Entry(entity).State = EntityState.Deleted;
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Kitten>> GetFiltered(PageFilter pageFilter = default, KittenSearchFilterData searchFilter = null)
        {
            await using var context = _contextFactory.CreateDbContext();
            if (!await context.Kittens.AnyAsync()) return Array.Empty<Kitten>();
            var query = context.Kittens.AsQueryable();

            if (pageFilter != PageFilter.Empty())
            {
                query = query.GetPage(pageFilter);
            }

            if (searchFilter is not null)
            {
                query = query.SearchWith(new KittenSearchFilter{Data = searchFilter });
            }

            return await query.ProjectToType<Kitten>().ToListAsync();
        }
    }
}
