using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Abstractions.Models;
using BusinessLayer.Abstractions.Services;
using MapsterMapper;
using KittensApi.Controllers.Requests;
using KittensApi.Controllers.Responses;
using Microsoft.AspNetCore.Authorization;
using KittensApi.Validations.Abstractions;

namespace KittensApi.Controllers
{
    [Route("clinics")]
    //[Authorize(Policy = "UserOnly")]
    [ApiController]
    public class ClinicsStorageController : ControllerBase
    {
        private readonly IClinicsService _clinicsService;
        private readonly IMapper _mapper;
        private readonly IValidationService<ClinicCreateRequest> _createRequestValidator;
        private readonly IValidationService<ClinicUpdateRequest> _updateRequestValidator;

        public ClinicsStorageController(IClinicsService clinicsService,
                                        IMapper mapper,
                                        IValidationService<ClinicCreateRequest> createRequestValidator,
                                        IValidationService<ClinicUpdateRequest> updateRequestValidator)
        {
            _clinicsService = clinicsService;
            _mapper = mapper;
            _createRequestValidator = createRequestValidator;
            _updateRequestValidator = updateRequestValidator;
        }

        [HttpPost]
        public async Task<ClinicCreateResponse> CreateAsync(ClinicCreateRequest request)
        {
            var failures = _createRequestValidator.ValidateEntry(request);
            if (failures.Count > 0)
            {
                return new(failures, false);
            }
            var result = await _clinicsService.Add(_mapper.Map<Clinic>(request));
            return new(_mapper.Map<IReadOnlyList<IOperationFailure>>(result.Failures), result.Succeed);
        }

        [HttpPut]
        public async Task<ClinicUpdateResponse> UpdateAsync([FromBody] ClinicUpdateRequest request)
        {
            var failures = _updateRequestValidator.ValidateEntry(request);
            if (failures.Count > 0)
            {
                return new(failures, false);
            }
            var result = await _clinicsService.Update(_mapper.Map<Clinic>(request));
            return new(_mapper.Map<IReadOnlyList<IOperationFailure>>(result.Failures), result.Succeed);
        }

        [HttpDelete("{id:int}")]
        public async Task DeleteAsync([FromRoute] int id)
        {
            await _clinicsService.Delete(id);
        }
        [HttpGet]
        public async Task<IEnumerable<ClinicGetResponse>> GetAsync()
        {
            var data = await _clinicsService.Get();
            return _mapper.Map<IEnumerable<ClinicGetResponse>>(data);
        }

        [HttpPost("{clinicId:int}/kittens")]
        public async Task<IEnumerable<KittenGetResponse>> ListKittensInClinic(int clinicId)
        {
            var data = await _clinicsService.ListKittensInClinic(clinicId);
            return _mapper.Map<IEnumerable<KittenGetResponse>>(data);
        }

        [HttpPost("{clinicId:int}/register/{kittenId:int}")]
        public async Task RegisterKittenInClinic(int clinicId, int kittenId)
        {
            await _clinicsService.RegisterKittenInClinic(clinicId, kittenId);
        }

        [HttpPost("{clinicId:int}/unregister/{kittenId:int}")]
        public async Task UnRegisterKittenFromClinic(int clinicId, int kittenId)
        {
            await _clinicsService.UnRegisterKittenFromClinic(clinicId, kittenId);
        }
    }
}
