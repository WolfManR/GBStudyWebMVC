using System.Threading.Tasks;
using BusinessLayer.Abstractions.Models.Analyzes;
using Data = DataLayer.Abstractions.Entities;
using BusinessLayer.Abstractions.Services;
using DataLayer.Abstractions.Repositories;
using MapsterMapper;

namespace BusinessLayer.Services
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IAnalysisRepository _repository;
        private readonly IMapper _mapper;

        public AnalysisService(IAnalysisRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> AssignAnalysisToKitten(InspectionAnalysis analysis, int clinicId, int kittenId)
        {
            var result = _repository.AssignAnalysisToKitten(_mapper.Map<Data::InspectionAnalysis>(analysis), clinicId, kittenId);
            return result;
        }

        public Task<int> AssignAnalysisToKitten(BloodAnalysis analysis, int clinicId, int kittenId)
        {
            var result = _repository.AssignAnalysisToKitten(_mapper.Map<Data::BloodAnalysis>(analysis), clinicId, kittenId);
            return result;
        }
    }
}