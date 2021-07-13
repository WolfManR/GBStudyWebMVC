using Mapster;
using BusinessLayer.Abstractions.Models;
using DataLayer.Abstractions.Filters;
using KittenData = DataLayer.Abstractions.Entities.Kitten;

namespace BusinessLayer.Maps
{
    public class KittensMaps : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<KittenData, Kitten>().TwoWays();
            config.ForType<KittenSearchFilterData, KittenSearchData>();
        }
    }
}
