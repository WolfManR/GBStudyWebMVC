using System.Threading.Tasks;
using DataLayer.Abstractions.Entities;

namespace DataLayer.Abstractions.Repositories
{
    public interface IAnalysisRepository
    {
        Task<int> AssignAnalysisToKitten(BloodAnalysis analysis, int clinicId, int kittenId);
        Task<int> AssignAnalysisToKitten(InspectionAnalysis analysis, int clinicId, int kittenId);
    }
}