using BusinessLayer.Abstractions.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Abstractions.Services
{
    public interface IClinicsService : IService<Clinic, int>
    {
        Task<IEnumerable<Kitten>> ListKittensInClinic(int clinicId);
        Task RegisterKittenInClinic(int clinicId, int kittenId);
        Task UnRegisterKittenFromClinic(int clinicId, int kittenId);
    }
}