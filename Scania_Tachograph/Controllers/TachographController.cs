using Core.Application.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Scania_Tachograph.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TachographController : ControllerBase
    {
        private readonly ITachographDataService _tachographDataService;
        public TachographController(ITachographDataService tachographDataService)
        {
            _tachographDataService = tachographDataService;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var result = await _tachographDataService.GenerateData(file);
            return Ok(result);
        }
        [HttpGet("get-single-drive-violation")]
        public async Task<IActionResult> GetSingleDriveViolations()
        {
            var result = await _tachographDataService.GetDriversWithSingleDriveTimeViolations();
            return Ok(result);
        }
        [HttpGet("get-rest-time-violation")]
        public async Task<IActionResult> GetDriversWithRestTimeViolations()
        {
            var result = await _tachographDataService.GetDriversWithRestTimeViolations();
            return Ok(result);
        }
        [HttpGet("get-day-drive-violation")]
        public async Task<IActionResult> GetDriversWithDayDriveTimeViolations()
        {
            var result = await _tachographDataService.GetDriversWithDayDriveTimeViolations();
            return Ok(result);
        }
        [HttpGet("get-week-drive-violation")]
        public async Task<IActionResult> GetDriversWithWeekDriveTimeViolations()
        {
            var result = await _tachographDataService.GetDriversWithWeekDriveTimeViolations();
            return Ok(result);
        }
    }
}
