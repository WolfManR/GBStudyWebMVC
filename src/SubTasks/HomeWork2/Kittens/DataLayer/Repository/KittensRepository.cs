using System;
using System.Collections.Generic;
using System.Linq;
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
    public class KittensRepository : KittensContextRepository<Kitten, int>, IKittensRepository
    {
        public KittensRepository(IDbContextFactory<KittensContext> contextFactory) : base(contextFactory) {}
        
        public async Task<IEnumerable<Kitten>> GetFiltered(PageFilter pageFilter = default, KittenSearchFilterData searchFilter = null)
        {
            await using var context = ContextFactory.CreateDbContext();
            if (!await context.Kittens.AnyAsync()) return Array.Empty<Kitten>();
            var query = context.Kittens.AsQueryable();

            if (pageFilter != PageFilter.Empty())
            {
                query = query.GetPage(pageFilter).OrderByDescending(k => k.Id);
            }

            if (searchFilter is not null)
            {
                query = query.SearchWith(new KittenSearchFilter{ Data = searchFilter });
            }
            
            return await query.ProjectToType<Kitten>().ToListAsync();
        }
    }
}
