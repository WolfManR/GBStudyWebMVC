using BusinessLayer.Abstractions.Models;
using KittensApi.Controllers.Requests;
using KittensApi.Controllers.Responses;
using Mapster;

namespace KittensApi.Maps
{
    public class ClinicsMaps : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<ClinicCreateRequest, Clinic>().TwoWays();
            config.ForType<Clinic, ClinicGetResponse>().TwoWays();
            config.ForType<ClinicUpdateRequest, Clinic>();
        }
    }
}