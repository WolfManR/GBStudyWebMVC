using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstractions.Models;
using BusinessLayer.Abstractions.Services;
using BusinessLayer.Abstractions.Validations;
using DataLayer.Abstractions.Filters;
using DataLayer.Abstractions.Repositories;
using KittenData = DataLayer.Abstractions.Entities.Kitten;
using MapsterMapper;

namespace BusinessLayer.Services
{
    public class KittensService : Service<Kitten, KittenData, int, IKittensRepository>, IKittensService
    {
        private readonly IKittenCardsRepository _cardsRepository;

        public KittensService(IKittensRepository repository,
                              IMapper mapper,
                              IValidationService<Kitten> entityValidation,
                              IKittenCardsRepository cardsRepository) 
            : base(repository, mapper, entityValidation)
        {
            _cardsRepository = cardsRepository;
        }

        public async Task<IReadOnlyCollection<Kitten>> GetPage(int page, int pageSize)
        {
            var data = await Repository.GetFiltered(new PageFilter(page, pageSize));
            return data.Select(Mapper.Map<Kitten>).ToList();
        }

        public async Task<IReadOnlyCollection<Kitten>> SearchFor(KittenSearchData searchData)
        {
            var data = await Repository.GetFiltered(searchFilterData: Mapper.Map<KittenSearchFilterData>(searchData));
            return data.Select(Mapper.Map<Kitten>).ToList();
        }

        public async Task<IReadOnlyCollection<Kitten>> GetPageFromSearch(int page, int pageSize, KittenSearchData searchData)
        {
            var data = await Repository.GetFiltered(new PageFilter(page, pageSize), Mapper.Map<KittenSearchFilterData>(searchData));
            return data.Select(Mapper.Map<Kitten>).ToList();
        }

        public async Task<IEnumerable<Clinic>> ListClinicsWhereKittenRegistered(int kittenId)
        {
            var data = await Repository.ListClinicsWhereKittenRegistered(kittenId);
            return Mapper.Map<IEnumerable<Clinic>>(data);
        }

        public async Task<KittenCard> GetKittenCardFromClinic(int kittenId, int clinicId)
        {
            var data = await _cardsRepository.Get(kittenId, clinicId);
            return Mapper.Map<KittenCard>(data);
        }

        public async Task<FullKittenCard> GetKittenFullCard(int kittenId)
        {
            var data = await _cardsRepository.Get(kittenId);
            return Mapper.Map<FullKittenCard>(data);
        }
    }
}