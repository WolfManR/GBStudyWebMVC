using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Abstractions.Models;
using BusinessLayer.Abstractions.Services;
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
    }
}