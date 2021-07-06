using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public interface IKittensRepository
    {
        Task Add(Kitten kitten);
        Task<List<Kitten>> Get();
    }
}