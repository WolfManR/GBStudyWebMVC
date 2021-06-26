using KittensApi.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace KittensApi.DAL.Repository
{
    public interface IKittensRepository
    {
        Task Add(Kitten kitten);
        Task<List<Kitten>> Get();
    }
}