using System.Linq;
using System.Threading.Tasks;
using DataBase.EF;
using DataLayer.Abstractions.Entities;
using DataLayer.Abstractions.Repositories;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Data = DataBase.Abstractions.Entities;

namespace DataLayer.Repository
{
    public class AnalysisRepository : IAnalysisRepository
    {
        private readonly KittensContext _context;
        private readonly IMapper _mapper;

        public AnalysisRepository(KittensContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AssignAnalysisToKitten(InspectionAnalysis analysis, int clinicId, int kittenId)
        {
            var kitten = await _context.Kittens.Include(k => k.Clinics).SingleOrDefaultAsync(k => k.Id == kittenId);
            if (kitten is null) return -1;
            var clinic = await _context.Clinics.SingleOrDefaultAsync(c => c.Id == clinicId);
            if (clinic is null) return -1;

            if (kitten.Clinics.All(c => c.Id != clinicId))
            {
                kitten.Clinics.Add(clinic);
            }

            var dataAnalysis = _mapper.Map<Data::Analyzes.InspectionAnalysis>(analysis);
            dataAnalysis.Clinic = clinic;
            dataAnalysis.Patient = kitten;

            await _context.Analysis.AddAsync(dataAnalysis);
            await _context.SaveChangesAsync();

            return dataAnalysis.Id;
        }

        public async Task<int> AssignAnalysisToKitten(BloodAnalysis analysis, int clinicId, int kittenId)
        {
            var kitten = await _context.Kittens.Include(k => k.Clinics).SingleOrDefaultAsync(k => k.Id == kittenId);
            if (kitten is null) return -1;
            var clinic = await _context.Clinics.SingleOrDefaultAsync(c => c.Id == clinicId);
            if (clinic is null) return -1;

            if (kitten.Clinics.All(c => c.Id != clinicId))
            {
                kitten.Clinics.Add(clinic);
            }

            var dataAnalysis = _mapper.Map<Data::Analyzes.BloodAnalysis>(analysis);
            dataAnalysis.Clinic = clinic;
            dataAnalysis.Patient = kitten;

            await _context.Analysis.AddAsync(dataAnalysis);
            await _context.SaveChangesAsync();

            return dataAnalysis.Id;
        }
    }
}