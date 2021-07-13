using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Abstractions.Entities;
using DataLayer.Abstractions.Filters;

namespace DataLayer.Abstractions.Repositories
{
    public interface IKittensRepository
    {
        Task Add(Kitten kitten);
        Task Delete(int id);
        Task<List<Kitten>> Get();
        Task<IEnumerable<Kitten>> GetFiltered(PageFilter pageFilter = default, KittenSearchFilterData searchFilterData = null);
        Task Update(Kitten kitten);
    }
}