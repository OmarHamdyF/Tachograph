using Core.Application.Dtos;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Services.IServices;

public interface ITachographDataService
{
    Task<ResultViewModel> GenerateData(IFormFile file);
    Task<ResultViewModel> GetDriversWithSingleDriveTimeViolations();
    Task<ResultViewModel> GetDriversWithRestTimeViolations();
    Task<ResultViewModel> GetDriversWithDayDriveTimeViolations();
    Task<ResultViewModel> GetDriversWithWeekDriveTimeViolations();
}

