using System.Threading.Tasks;
using BusinessLayer.Abstractions.Models.Analyzes;

namespace BusinessLayer.Abstractions.Services
{
    public interface IAnalysisService
    {
        Task<int> AssignAnalysisToKitten(InspectionAnalysis analysis, int clinicId, int kittenId);
        Task<int> AssignAnalysisToKitten(BloodAnalysis analysis, int clinicId, int kittenId);
    }
}