using KittensApi.Controllers.Requests;
using KittensApi.Controllers.Responses;
using MapsterMapper;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstractions.Models;
using BusinessLayer.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;

namespace KittensApi.Controllers
{
    [Route("kittens")]
    [Authorize(Policy = "UserOnly")]
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

        [HttpGet("/page/{page:int}pagesize/{size:int}")]
        public async Task<IEnumerable<KittenGetResponse>> GetPageAsync([FromRoute] int size, [FromRoute] int page)
        {
            var data = await _kittensService.GetPage(page, size);
            return data.Select(_mapper.Map<KittenGetResponse>);
        }

        [HttpGet("search")]
        public async Task<IEnumerable<KittenGetResponse>> GetWithSearchAsync([FromQuery] KittenSearchRequest searchRequest)
        {
            var data = await _kittensService.SearchFor(_mapper.Map<KittenSearchData>(searchRequest));
            return data.Select(_mapper.Map<KittenGetResponse>);
        }

        [HttpGet("/page/{page:int}pagesize/{size:int}/search")]
        public async Task<IEnumerable<KittenGetResponse>> GetPageWithSearchAsync([FromRoute] int size, [FromRoute] int page, [FromQuery] KittenSearchRequest searchRequest)
        {
            var data = await _kittensService.GetPageFromSearch(page, size, _mapper.Map<KittenSearchData>(searchRequest));
            return data.Select(_mapper.Map<KittenGetResponse>);
        }

        [HttpPost("{kittenId:int}/clinics")]
        public async Task<IEnumerable<ClinicGetResponse>> ListClinicsWhereKittenRegistered(int kittenId)
        {
            var data = await _kittensService.ListClinicsWhereKittenRegistered(kittenId);
            return _mapper.Map<IEnumerable<ClinicGetResponse>>(data);
        }
    }
}
