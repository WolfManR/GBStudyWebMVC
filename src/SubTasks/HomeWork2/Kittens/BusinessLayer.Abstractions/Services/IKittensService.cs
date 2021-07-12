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
    }
}