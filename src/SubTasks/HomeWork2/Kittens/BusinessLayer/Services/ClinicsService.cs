using BusinessLayer.Abstractions.Models;
using BusinessLayer.Abstractions.Services;
using DataLayer.Abstractions.Repositories;
using MapsterMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Abstractions.Validations;
using dataEntities = DataLayer.Abstractions.Entities;

namespace BusinessLayer.Services
{
    public class ClinicsService : Service<Clinic, dataEntities::Clinic ,int, IClinicsRepository>, IClinicsService
    {
        public ClinicsService(IClinicsRepository repository,
                              IMapper mapper,
                              IValidationService<Clinic> entityValidation) 
            : base(repository, mapper, entityValidation)
        {
        }

        public async Task<IEnumerable<Kitten>> ListKittensInClinic(int clinicId)
        {
            var data = await Repository.ListKittensInClinic(clinicId);
            return Mapper.Map<IEnumerable<Kitten>>(data);
        }

        public Task RegisterKittenInClinic(int clinicId, int kittenId)
        {
            return Repository.RegisterKittenInClinic(clinicId, kittenId);
        }

        public Task UnRegisterKittenFromClinic(int clinicId, int kittenId)
        {
            return Repository.UnRegisterKittenFromClinic(clinicId, kittenId);
        }
    }
}