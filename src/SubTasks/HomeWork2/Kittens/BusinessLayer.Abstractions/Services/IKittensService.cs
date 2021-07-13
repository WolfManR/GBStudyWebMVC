using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Abstractions.Models;

namespace BusinessLayer.Abstractions.Services
{
    public interface IKittensService
    {
        Task<IReadOnlyCollection<Kitten>> Get();
        Task Add(Kitten kitten);
        Task Update(Kitten kitten);
        Task Delete(int id);
        Task<IReadOnlyCollection<Kitten>> GetPage(int page, int pageSize);
        Task<IReadOnlyCollection<Kitten>> SearchFor(KittenSearchData searchData);
        Task<IReadOnlyCollection<Kitten>> GetPageFromSearch(int page, int pageSize, KittenSearchData searchData);
    }
}