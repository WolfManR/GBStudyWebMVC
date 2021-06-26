using KittensApi.Controllers.Requests;
using KittensApi.Controllers.Responses;
using KittensApi.DAL.Repository;
using KittensApi.Models;

using Mapster;

using MapsterMapper;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;

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
        public void Create(KittenCreateRequest request)
        {
            kittensRepository.Add(mapper.Map<Kitten>(request));
        }

        [HttpGet]
        public IEnumerable<KittenGetResponse> Get()
        {
            return kittensRepository.Get().Adapt<IEnumerable<KittenGetResponse>>();
        }
    }
}
