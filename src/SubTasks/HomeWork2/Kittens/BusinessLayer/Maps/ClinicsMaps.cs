using BusinessLayer.Abstractions.Models;
using Mapster;
using ClinicData = DataLayer.Abstractions.Entities.Clinic;

namespace BusinessLayer.Maps
{
    public class ClinicsMaps : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<ClinicData, Clinic>().TwoWays();
        }
    }
}