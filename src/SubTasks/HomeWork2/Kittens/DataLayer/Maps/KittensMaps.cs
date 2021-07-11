using DataBase.Abstractions.Entities;
using Mapster;
using KittenData = DataLayer.Abstractions.Entities.Kitten;

namespace DataLayer.Maps
{
    public class KittensMaps : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<KittenData, Kitten>().TwoWays();
        }
    }
}
