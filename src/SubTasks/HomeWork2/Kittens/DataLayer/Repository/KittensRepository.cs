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
    public class KittensRepository : KittensContextRepository<Kitten, dbEntities::Kitten, int>, IKittensRepository, IKittenCardsRepository
    {
        public KittensRepository(KittensContext context, IMapper mapper) : base(context, mapper) {}
        
        public async Task<IEnumerable<Kitten>> GetFiltered(PageFilter pageFilter = default, KittenSearchFilterData searchFilter = null)
        {
            if (!await Context.Kittens.AnyAsync()) return Array.Empty<Kitten>();
            var query = Context.Kittens.AsQueryable();

            if (pageFilter is not null)
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

        public async Task<KittenCard> Get(int kittenId, int clinicId)
        {
            var kitten = await Context.Kittens
                .Include(k => k.Analysis).ThenInclude(a => a.Clinic)
                .Include(k => k.Clinics)
                .SingleOrDefaultAsync(k => k.Id == kittenId);

            if (kitten?.Clinics.SingleOrDefault(c => c.Id == clinicId) is not { } clinic) return null;

            var analizes = kitten.Analysis.Where(a => a.Clinic.Id == clinicId);
            var card = new KittenCard()
            {
                Kitten = Mapper.Map<Kitten>(kitten),
                Clinic = Mapper.Map<Clinic>(clinic),
                Analyzes = Mapper.Map<IReadOnlyCollection<IAnalysis>>(analizes)
            };

            return card;
        }

        public async Task<FullKittenCard> Get(int kittenId)
        {
            var kitten = await Context.Kittens.AsNoTracking()
                .Include(k => k.Analysis).ThenInclude(a => a.Clinic)
                .Include(k => k.Clinics)
                .SingleOrDefaultAsync(k => k.Id == kittenId);
            if (kitten is null) return null;

            var card = new FullKittenCard()
            {
                Kitten = Mapper.Map<Kitten>(kitten),
                Clinics = Mapper.Map<IReadOnlyCollection<Clinic>>(kitten.Clinics.ToList()),
                Analyzes = Mapper.Map<IReadOnlyCollection<IClinicAnalysis>>(kitten.Analysis.ToList())
            };

            return card;
        }
    }
}
