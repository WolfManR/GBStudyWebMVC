using Mapster;
using BusinessLayer.Abstractions.Models;
using KittenData = DataLayer.Abstractions.Entities.Kitten;

namespace BusinessLayer.Maps
{
    public class KittensMaps : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<KittenData, Kitten>().TwoWays();
        }
    }
}
