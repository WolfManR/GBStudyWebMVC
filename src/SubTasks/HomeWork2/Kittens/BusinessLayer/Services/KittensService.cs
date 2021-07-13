using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstractions.Models;
using BusinessLayer.Abstractions.Services;
using DataLayer.Abstractions.Filters;
using DataLayer.Abstractions.Repositories;
using KittenData = DataLayer.Abstractions.Entities.Kitten;
using MapsterMapper;

namespace BusinessLayer.Services
{
    public class KittensService : IKittensService
    {
        private readonly IKittensRepository _repository;
        private readonly IMapper _mapper;

        public KittensService(IKittensRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<Kitten>> Get()
        {
            var data = await _repository.Get();
            return _mapper.Map<List<Kitten>>(data);
        }

        public Task Add(Kitten kitten)
        {
            return _repository.Add(_mapper.Map<KittenData>(kitten));
        }

        public Task Update(Kitten kitten)
        {
            return _repository.Update(_mapper.Map<KittenData>(kitten));
        }

        public Task Delete(int id)
        {
            return _repository.Delete(id);
        }

        public async Task<IReadOnlyCollection<Kitten>> GetPage(int page, int pageSize)
        {
            var data = await _repository.GetFiltered(new PageFilter() { Page = page, Size = pageSize });
            return data.Select(_mapper.Map<Kitten>).ToList();
        }

        public async Task<IReadOnlyCollection<Kitten>> SearchFor(KittenSearchData searchData)
        {
            var data = await _repository.GetFiltered(searchFilterData: _mapper.Map<KittenSearchFilterData>(searchData));
            return data.Select(_mapper.Map<Kitten>).ToList();
        }

        public async Task<IReadOnlyCollection<Kitten>> GetPageFromSearch(int page, int pageSize, KittenSearchData searchData)
        {
            var data = await _repository.GetFiltered(new PageFilter() { Page = page, Size = pageSize }, _mapper.Map<KittenSearchFilterData>(searchData));
            return data.Select(_mapper.Map<Kitten>).ToList();
        }
    }
}