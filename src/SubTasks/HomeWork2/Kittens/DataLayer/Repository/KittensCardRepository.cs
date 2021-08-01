using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.EF;
using DataLayer.Abstractions.Entities;
using DataLayer.Abstractions.Repositories;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repository
{
    public class KittensCardRepository : IKittenCardsRepository
    {
        private readonly KittensContext _context;
        private readonly IMapper _mapper;

        public KittensCardRepository(KittensContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<KittenCard> Get(int kittenId, int clinicId)
        {
            var kitten = await _context.Kittens
                .Include(k=>k.Analysis).ThenInclude(a => a.Clinic)
                .Include(k=>k.Clinics)
                .SingleOrDefaultAsync(k => k.Id == kittenId);

            if (kitten?.Clinics.SingleOrDefault(c => c.Id != clinicId) is not {} clinic) return null;

            var analizes = kitten.Analysis.Where(a => a.Clinic.Id == clinicId);
            var card = new KittenCard()
            {
                Kitten = _mapper.Map<Kitten>(kitten),
                Clinic = _mapper.Map<Clinic>(clinic),
                Analyzes = _mapper.Map<IReadOnlyCollection<IAnalysis>>(analizes)
            };

            return card;
        }

        public async Task<FullKittenCard> Get(int kittenId)
        {
            var kitten = await _context.Kittens
                .Include(k => k.Analysis).ThenInclude(a => a.Clinic)
                .Include(k => k.Clinics)
                .SingleOrDefaultAsync(k => k.Id == kittenId);
            if (kitten is null) return null;
            
            var card = new FullKittenCard()
            {
                Kitten = _mapper.Map<Kitten>(kitten),
                Clinics = _mapper.Map<IReadOnlyCollection<Clinic>>(kitten.Clinics),
                Analyzes = _mapper.Map<IReadOnlyCollection<IClinicAnalysis>>(kitten.Analysis)
            };

            return card;
        }
    }
}