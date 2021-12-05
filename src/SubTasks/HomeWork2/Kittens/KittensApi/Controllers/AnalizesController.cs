using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BusinessLayer.Abstractions.Models.Analyzes;
using BusinessLayer.Abstractions.Services;
using KittensApi.Controllers.Requests;
using MapsterMapper;

namespace KittensApi.Controllers
{
    [Route("analysis")]
    [ApiController]
    public class AnalizesController : ControllerBase
    {
        private readonly IAnalysisService _analysisService;
        private readonly IMapper _mapper;

        public AnalizesController(IAnalysisService analysisService, IMapper mapper)
        {
            _analysisService = analysisService;
            _mapper = mapper;
        }

        [HttpPost("inspection/{kittenId:int}/{clinicId:int}")]
        public async Task<IActionResult> AssignAnalysisToKitten(
            [FromBody] InspectionAnalysisRequest request,
            [FromRoute] int clinicId,
            [FromRoute] int kittenId)
        {
            var analysis = _mapper.Map<InspectionAnalysis>(request);
            var result = await _analysisService.AssignAnalysisToKitten(analysis, clinicId, kittenId);

            if (result < 0) return BadRequest();
            return Ok(result);
        }

        [HttpPost("blood/{kittenId:int}/{clinicId:int}")]
        public async Task<IActionResult> AssignAnalysisToKitten(
            [FromBody] BloodAnalysisRequest request,
            [FromRoute] int clinicId,
            [FromRoute] int kittenId)
        {
            var analysis = _mapper.Map<BloodAnalysis>(request);
            var result = await _analysisService.AssignAnalysisToKitten(analysis, clinicId, kittenId);

            if (result < 0) return BadRequest();
            return Ok(result);
        }
    }
}
