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

        [HttpPut]
        public async Task UpdateAsync([FromBody] KittenUpdateRequest request)
        {
            await _kittensService.Update(_mapper.Map<Kitten>(request));
        }

        [HttpDelete("{id:int}")]
        public async Task DeleteAsync([FromRoute] int id)
        {
            await _kittensService.Delete(id);
        }

        [HttpGet]
        public async Task<IEnumerable<KittenGetResponse>> GetAsync()
        {
            var data = await _kittensService.Get();
            return _mapper.Map<List<KittenGetResponse>>(data);
        }

        [HttpGet("{size:int}/{page:int}")]
        public async Task GetPageAsync([FromRoute] int size, [FromRoute] int page)
        {

        }

        [HttpGet]
        public async Task GetWithSearchAsync([FromQuery] string search)
        {

        }

        [HttpGet("{size:int}/{page:int}")]
        public async Task GetPageWithSearchAsync([FromRoute] int size, [FromRoute] int page, [FromQuery] string search)
        {

        }
    }
}
