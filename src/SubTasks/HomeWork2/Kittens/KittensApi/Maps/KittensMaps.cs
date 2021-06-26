using KittensApi.Controllers.Requests;
using KittensApi.Controllers.Responses;
using KittensApi.Models;

using Mapster;

namespace KittensApi.Maps
{
    public class KittensMaps : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<KittenCreateRequest, Kitten>().TwoWays();
            config.ForType<Kitten, KittenGetResponse>().TwoWays();
        }
    }
}
