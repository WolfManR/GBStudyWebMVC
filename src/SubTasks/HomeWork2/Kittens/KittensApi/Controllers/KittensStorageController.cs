using KittensApi.Controllers.Requests;
using KittensApi.Controllers.Responses;
using Mapster;

using MapsterMapper;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase;
using DataBase.Repository;

namespace KittensApi.Controllers
{
    [Route("kittens")]
    [ApiController]
    public class KittensStorageController : ControllerBase
    {
        private readonly IKittensRepository kittensRepository;
        private readonly IMapper mapper;

        public KittensStorageController(IKittensRepository kittensRepository, IMapper mapper)
        {
            this.kittensRepository = kittensRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task CreateAsync(KittenCreateRequest request)
        {
            await kittensRepository.Add(mapper.Map<Kitten>(request));
        }

        [HttpGet]
        public async Task<IEnumerable<KittenGetResponse>> GetAsync()
        {
            var data = await kittensRepository.Get();
            return data.Adapt<List<KittenGetResponse>>();
        }
    }
}
