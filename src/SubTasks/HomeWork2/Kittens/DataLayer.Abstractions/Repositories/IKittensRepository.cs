using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Abstractions.Entities;
using DataLayer.Abstractions.Filters;

namespace DataLayer.Abstractions.Repositories
{
    public interface IKittensRepository : IRepository<Kitten, int>
    {
        Task<IEnumerable<Kitten>> GetFiltered(PageFilter pageFilter = default, KittenSearchFilterData searchFilterData = null);
        Task<IEnumerable<Clinic>> ListClinicsWhereKittenRegistered(int kittenId);
    }
}