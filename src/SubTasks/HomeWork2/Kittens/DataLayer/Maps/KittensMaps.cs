using Mapster;
using DataLayer.Abstractions.Entities;
using Data = DataBase.Abstractions.Entities;

namespace DataLayer.Maps
{
    public class KittensMaps : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Data::Kitten, Kitten>().TwoWays();
        }
    }
}
