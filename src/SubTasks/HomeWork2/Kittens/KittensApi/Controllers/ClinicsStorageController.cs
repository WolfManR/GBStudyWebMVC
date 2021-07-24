using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Abstractions.Models;
using BusinessLayer.Abstractions.Services;
using MapsterMapper;
using KittensApi.Controllers.Requests;
using KittensApi.Controllers.Responses;
using Microsoft.AspNetCore.Authorization;

namespace KittensApi.Controllers
{
    [Route("clinics")]
    [Authorize(Policy = "UserOnly")]
    [ApiController]
    public class ClinicsStorageController : ControllerBase
    {
        private readonly IClinicsService _clinicsService;
        private readonly IMapper _mapper;

        public ClinicsStorageController(IClinicsService clinicsService, IMapper mapper)
        {
            _clinicsService = clinicsService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task CreateAsync(ClinicCreateRequest request)
        {
            await _clinicsService.Add(_mapper.Map<Clinic>(request));
        }

        [HttpPut]
        public async Task UpdateAsync([FromBody] ClinicUpdateRequest request)
        {
            await _clinicsService.Update(_mapper.Map<Clinic>(request));
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
