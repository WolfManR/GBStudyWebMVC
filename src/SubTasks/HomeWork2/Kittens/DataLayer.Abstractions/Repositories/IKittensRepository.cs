using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Abstractions.Entities;

namespace DataLayer.Abstractions.Repositories
{
    public interface IKittensRepository
    {
        Task Add(Kitten kitten);
        Task<List<Kitten>> Get();
    }
}