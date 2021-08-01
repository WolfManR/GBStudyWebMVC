using KittensApi.Controllers.Requests;
using KittensApi.Controllers.Responses;
using MapsterMapper;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstractions.Models;
using BusinessLayer.Abstractions.Services;
using KittensApi.Validations.Abstractions;
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
        private readonly IValidationService<KittenCreateRequest> _createRequestValidator;
        private readonly IValidationService<KittenUpdateRequest> _updateRequestValidator;

        public KittensStorageController(IKittensService kittensService,
                                        IMapper mapper,
                                        IValidationService<KittenCreateRequest> createRequestValidator,
                                        IValidationService<KittenUpdateRequest> updateRequestValidator)
        {
            _kittensService = kittensService;
            _mapper = mapper;
            _createRequestValidator = createRequestValidator;
            _updateRequestValidator = updateRequestValidator;
        }

        [HttpPost]
        public async Task<KittenCreateResponse> CreateAsync(KittenCreateRequest request)
        {
            var failures = _createRequestValidator.ValidateEntry(request);
            if (failures.Count > 0)
            {
                return new(failures, false);
            }
            var result = await _kittensService.Add(_mapper.Map<Kitten>(request));
            return new(_mapper.Map<IReadOnlyList<IOperationFailure>>(result.Failures), result.Succeed);
        }

        [HttpPut]
        public async Task<KittenUpdateResponse> UpdateAsync([FromBody] KittenUpdateRequest request)
        {
            var failures = _updateRequestValidator.ValidateEntry(request);
            if (failures.Count > 0)
            {
                return new(failures, false);
            }
            var result = await _kittensService.Update(_mapper.Map<Kitten>(request));
            return new(_mapper.Map<IReadOnlyList<IOperationFailure>>(result.Failures), result.Succeed);
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

        [HttpGet("/page/{page:int}/pagesize/{size:int}")]
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

        [HttpGet("/page/{page:int}/pagesize/{size:int}/search")]
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

        [HttpGet("card/{kittenId:int}/{clinicId:int}")]
        public async Task<KittenCardFromClinicResponse> GetKittenCardFromClinic([FromRoute] int kittenId, [FromRoute] int clinicId)
        {
            var data = await _kittensService.GetKittenCardFromClinic(kittenId, clinicId);
            return _mapper.Map<KittenCardFromClinicResponse>(data);
        }

        [HttpGet("card/{kittenId:int}")]
        public async Task<KittenFullCardResponse> GetKittenFullCard([FromRoute] int kittenId)
        {
            var data = await _kittensService.GetKittenFullCard(kittenId);
            return _mapper.Map<KittenFullCardResponse>(data);
        }
    }
}
