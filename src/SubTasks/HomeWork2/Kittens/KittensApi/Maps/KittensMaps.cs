using BusinessLayer.Abstractions.Models;
using KittensApi.Controllers.Requests;
using KittensApi.Controllers.Responses;

using Mapster;

namespace KittensApi.Maps
{
    public class KittensMaps : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<KittenCreateRequest, Kitten>().TwoWays();
            config.ForType<Kitten, KittenGetResponse>().TwoWays();
            config.ForType<KittenUpdateRequest, Kitten>();
            config.ForType<KittenSearchRequest, KittenSearchData>();
        }
    }
}
