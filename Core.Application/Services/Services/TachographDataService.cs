using Core.Application.Dtos;
using Core.Application.Interfaces;
using Core.Application.Services.IServices;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Core.Application.Services.Services;

public class TachographDataService : ITachographDataService
{
    private readonly IBaseRepository<TachographData> _tachographRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TachographDataService> _logger;


    private ResultViewModel resultViewModel = new ResultViewModel();

    public TachographDataService(IBaseRepository<TachographData> tachographRepository, IUnitOfWork unitOfWork, ILogger<TachographDataService> logger)
    {
        _tachographRepository = tachographRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ResultViewModel> GenerateData(IFormFile file)
    {

        try
        {
            if (file == null || file.Length == 0)
            {
                _logger.LogWarning("File is empty");
                return resultViewModel.BindResultViewModel(false, "File is empty", 400, null);
            }

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var jsonContent = await reader.ReadToEndAsync();

                // Assuming the JSON content is an array of objects
                var tachographDataList = JsonConvert.DeserializeObject<List<TachographData>>(jsonContent);

                if (tachographDataList == null || tachographDataList.Count == 0)
                {
                    return resultViewModel.BindResultViewModel(false, "Error parsing JSON data", 400, null);
                }

                await _tachographRepository.AddRangeAsync(tachographDataList);
                var added = await _unitOfWork.CompleteAsync();

                if (added <= 0)
                {
                    resultViewModel.BindResultViewModel(false, "Error While Uploading Drivers Records", 200, added);
                }
                _logger.LogInformation("Data uploaded successfully");
                return resultViewModel.BindResultViewModel(true, "Data uploaded successfully", 200, tachographDataList);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            return resultViewModel.BindResultViewModel(false, $"Error: {ex.Message}", 500, null);
        }
    }
    public async Task<ResultViewModel> GetDriversWithSingleDriveTimeViolations()
    {
        try
        {
            var tachographData = await _tachographRepository.GetAll();
            var violations = tachographData
                .Where(td => td.Activity == "Driving")
                .GroupBy(td => td.DriverId)
                .Where(group => group.Sum(td => CalculateDuration(td.StartTime, td.EndTime)) > 4)
                .Select(group => new DriverViolationDto
                {
                    DriverId = group.Key,
                    ViolationType = "Single Drive Time",
                    ViolationDetails = "Driver exceeded 4 hours of continuous drive time."
                })
                .ToList();
            return resultViewModel.BindResultViewModel(true, "Drivers with Single Drive Time Violations", 200, violations);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            return resultViewModel.BindResultViewModel(false, $"Error: {ex.Message}", 500, null);
        }
    }
    public async Task<ResultViewModel> GetDriversWithRestTimeViolations()
    {
        try
        {
            var tachographData = await _tachographRepository.GetAll();
            var violations = tachographData
                .Where(td => td.Activity == "Rest")
                .GroupBy(td => td.DriverId)
                .Where(group => group.Any(td => CalculateDuration(td.EndTime, td.StartTime) < 0.75)) // Less than 45 minutes of rest (0.75 hours)
                .Select(group => new DriverViolationDto
                {
                    DriverId = group.Key,
                    ViolationType = "Rest Time",
                    ViolationDetails = "Driver had less than 45 minutes of continuous rest."
                })
                .ToList();

            return resultViewModel.BindResultViewModel(true, "Drivers with Rest Time Violations", 200, violations);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            return resultViewModel.BindResultViewModel(false, $"Error: {ex.Message}", 500, null);
        }
    }

    public async Task<ResultViewModel> GetDriversWithDayDriveTimeViolations()
    {
        try
        {
            var tachographData = await _tachographRepository.GetAll();
            var violations = tachographData
                .Where(td => td.Activity == "Driving")
                .GroupBy(td => td.DriverId)
                .Where(group => group.Sum(td => CalculateDuration(td.StartTime, td.EndTime)) > 12)
                .Select(group => new DriverViolationDto
                {
                    DriverId = group.Key,
                    ViolationType = "Day Drive Time",
                    ViolationDetails = "Driver exceeded 12 hours of drive time in a day."
                })
                .ToList();

            return resultViewModel.BindResultViewModel(true, "Drivers with Day Drive Time Violations", 200, violations);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error: {ex.Message}");
            return resultViewModel.BindResultViewModel(false, $"Error: {ex.Message}", 500, null);
        }
    }

    public async Task<ResultViewModel> GetDriversWithWeekDriveTimeViolations()
    {
        try
        {
            var tachographData = await _tachographRepository.GetAll();
            var violations = tachographData
                .Where(td => td.Activity == "Driving")
                .GroupBy(td => td.DriverId)
                .Where(group => group.Sum(td => CalculateDuration(td.StartTime, td.EndTime)) > 60)
                .Select(group => new DriverViolationDto
                {
                    DriverId = group.Key,
                    ViolationType = "Week Drive Time",
                    ViolationDetails = "Driver exceeded 60 hours of drive time in a week."
                })
                .ToList();

            return resultViewModel.BindResultViewModel(true, "Drivers with Week Drive Time Violations", 200, violations);
        }
        catch (Exception ex)
        {
            return resultViewModel.BindResultViewModel(false, $"Error: {ex.Message}", 500, null);
        }
    }

    private double CalculateDuration(string startTime, string endTime)
    {
        TimeSpan duration = DateTime.Parse(endTime) - DateTime.Parse(startTime);
        return Math.Abs(duration.TotalHours);
    }

}
