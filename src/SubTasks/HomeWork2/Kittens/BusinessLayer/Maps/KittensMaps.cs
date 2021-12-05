using Mapster;
using BusinessLayer.Abstractions.Models;
using DataLayer.Abstractions.Filters;
using Data = DataLayer.Abstractions.Entities;

namespace BusinessLayer.Maps
{
    public class KittensMaps : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Data::Kitten, Kitten>().TwoWays();
            config.ForType<KittenSearchData, KittenSearchFilterData>();
            config.ForType<Data::KittenCard, KittenCard>();
            config.ForType<Data::FullKittenCard, FullKittenCard>();
        }
    }
}
