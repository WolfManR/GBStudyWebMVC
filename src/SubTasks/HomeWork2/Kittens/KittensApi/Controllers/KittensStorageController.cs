using KittensApi.Controllers.Requests;
using KittensApi.Controllers.Responses;
using MapsterMapper;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Abstractions.Models;
using BusinessLayer.Abstractions.Services;

namespace KittensApi.Controllers
{
    [Route("kittens")]
    [ApiController]
    public class KittensStorageController : ControllerBase
    {
        private readonly IKittensService _kittensService;
        private readonly IMapper _mapper;

        public KittensStorageController(IKittensService kittensService, IMapper mapper)
        {
            _kittensService = kittensService;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task CreateAsync(KittenCreateRequest request)
        {
            await _kittensService.Add(_mapper.Map<Kitten>(request));
        }

        [HttpGet]
        public async Task<IEnumerable<KittenGetResponse>> GetAsync()
        {
            var data = await _kittensService.Get();
            return _mapper.Map<List<KittenGetResponse>>(data);
        }
    }
}
