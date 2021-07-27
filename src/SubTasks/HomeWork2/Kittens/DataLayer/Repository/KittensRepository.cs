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
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

using dbEntities = DataBase.Abstractions.Entities;

namespace DataLayer.Repository
{
    public class KittensRepository : KittensContextRepository<Kitten, dbEntities::Kitten, int>, IKittensRepository
    {
        public KittensRepository(KittensContext context, IMapper mapper) : base(context, mapper) {}
        
        public async Task<IEnumerable<Kitten>> GetFiltered(PageFilter pageFilter = default, KittenSearchFilterData searchFilter = null)
        {
            if (!await Context.Kittens.AnyAsync()) return Array.Empty<Kitten>();
            var query = Context.Kittens.AsQueryable();

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

        public async Task<IEnumerable<Clinic>> ListClinicsWhereKittenRegistered(int kittenId)
        {
            var clinic = await Context.Kittens.AsNoTracking().Include(c => c.Clinics).SingleOrDefaultAsync(c => c.Id == kittenId);
            return clinic is null
                ? Array.Empty<Clinic>()
                : Mapper.Map<IEnumerable<Clinic>>(clinic.Clinics);
        }
    }
}
