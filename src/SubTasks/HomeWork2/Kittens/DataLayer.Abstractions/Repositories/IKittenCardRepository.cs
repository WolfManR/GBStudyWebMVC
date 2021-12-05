using System.Threading.Tasks;
using DataLayer.Abstractions.Entities;

namespace DataLayer.Abstractions.Repositories
{
    public interface IKittenCardsRepository
    {
        Task<KittenCard> Get(int kittenId, int clinicId);
        Task<FullKittenCard> Get(int kittenId);
    }
}