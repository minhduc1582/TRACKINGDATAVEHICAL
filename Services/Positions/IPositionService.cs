using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop_pbl6.Models.DTO.Positions;
using TrackingDataVehical.Models.DTO.Positions;

namespace eshop_api.Services.Positions
{
    public interface IPositionService
    {
        Task<PositionDto> AddPositionByUser(CreateUpdatePosition createUpdatePosition, string username);
        Task<PositionDto> AddPositionByUser(CreateUpdatePositionByFileDto createUpdatePosition, string username);
        Task<List<PositionDto>> GetPosition();
        Task<List<PositionDto>> GetPositionByUser(string username);
        Task<List<PositionDto>> GetPositionByDate(int day, int month);
        Task<List<PositionDto>> GetPositionByDateMonth(DateTime begin, DateTime end);
        // Task<List<DrivingStatistic>> DriveStatistics (DateTime date);
        Task<DrivingStatistic> DailyDriveStatistics (string username,DateTime date);
        Task<DrivingStatistic> DailyDriveStatistics (int day, int month);
        Task<List<DrivingStatistic>> MonthlyDriveStatistics (string username, int month);
        // Task<List<AllDriverStatistic>> MonthlyDriverStatistics(int month);
    }
}