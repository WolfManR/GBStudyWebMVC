using DataBase.Abstractions.Entities;
using Mapster;
using ClinicData = DataLayer.Abstractions.Entities.Clinic;

namespace DataLayer.Maps
{
    public class ClinicMaps : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<ClinicData, Clinic>().TwoWays();
        }
    }
}