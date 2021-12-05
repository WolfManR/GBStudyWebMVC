using DataLayer.Abstractions.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Abstractions.Repositories
{
    public interface IClinicsRepository : IRepository<Clinic, int>
    {
        Task<IEnumerable<Kitten>> ListKittensInClinic(int clinicId);
        Task RegisterKittenInClinic(int clinicId, int kittenId);
        Task UnRegisterKittenFromClinic(int clinicId, int kittenId);
    }
}