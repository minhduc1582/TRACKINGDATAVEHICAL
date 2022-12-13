using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eshop_api.Helpers;
using eshop_api.Services.Images;
using eshop_pbl6.Entities;
using eshop_pbl6.Models.DTO.Positions;
using TrackingDataVehical.Models.DTO.Positions;

namespace eshop_api.Services.Positions
{
    public class PositionService : IPositionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PositionService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PositionDto> AddPositionByUser(CreateUpdatePosition createUpdatePosition, string username)
        {
            int iduser = _context.AppUsers.FirstOrDefault(x => x.Username == username).Id;
            
            Position position = new Position();
            position.longitude = createUpdatePosition.longitude;
            position.latitude = createUpdatePosition.latitude;
            position.datetime = createUpdatePosition.datetime;
            position.idUser = iduser;
            position.speed = createUpdatePosition.speed;
            if(!string.IsNullOrEmpty(createUpdatePosition.image1)){
                IFormFile image1 = ConvertBase64toFile.Base64ToImage(createUpdatePosition.image1);
                position.url1 = CloudImage.UploadImage(image1);
            }
            if(!string.IsNullOrEmpty(createUpdatePosition.image2)){
                IFormFile image2 = ConvertBase64toFile.Base64ToImage(createUpdatePosition.image2);
                position.url1 = CloudImage.UploadImage(image2);
            }
            var result = _context.Positions.Add(position);
            await _context.SaveChangesAsync();
            PositionDto positionDto = _mapper.Map<Position, PositionDto>(result.Entity);
            return positionDto;
        }

        public async Task<PositionDto> AddPositionByUser(CreateUpdatePositionByFileDto createUpdatePosition, string username)
        {
             int iduser = _context.AppUsers.FirstOrDefault(x => x.Username == username).Id;
            
            Position position = new Position();
            position.longitude = createUpdatePosition.longitude;
            position.latitude = createUpdatePosition.latitude;
            position.datetime = createUpdatePosition.datetime;
            position.idUser = iduser;
            position.speed = createUpdatePosition.speed;
            position.url1 = CloudImage.UploadImage(createUpdatePosition.image1);
            position.url1 = CloudImage.UploadImage(createUpdatePosition.image2);
            var result = _context.Positions.Add(position);
            await _context.SaveChangesAsync();
            PositionDto positionDto = _mapper.Map<Position, PositionDto>(result.Entity);
            return positionDto;
        }

        public async Task<List<PositionDto>> GetPosition()
        {
           var position = _context.Positions.ToList();
           var images = _context.Images.ToList();
           List<PositionDto> positionDtos = new List<PositionDto>();
           foreach(var item in position)
           {
                PositionDto positionDto = _mapper.Map<Position, PositionDto>(item);
                positionDtos.Add(positionDto);
           }
           return await Task.FromResult(positionDtos);
        }

        public async Task<List<PositionDto>> GetPositionByDate(int day, int month)
        {
            var position = _context.Positions.ToList();
            var images = _context.Images.ToList();
            List<PositionDto> positionDtos = new List<PositionDto>();
            if(month!=0)
            {
                if(day!=0)
                {
                    foreach(var item in position)
                    {
                        if(Convert.ToInt64(item.datetime.Date) == day && Convert.ToInt64(item.datetime.Month) == month)
                        {
                            PositionDto positionDto = _mapper.Map<Position, PositionDto>(item);
                            positionDtos.Add(positionDto);
                        }
                    }
                }
                else
                {
                    foreach(var item in position)
                    {
                        if(Convert.ToInt64(item.datetime.Month) == month)
                        {
                            PositionDto positionDto = _mapper.Map<Position, PositionDto>(item);
                            positionDtos.Add(positionDto);
                        }
                    }
                }
            }
            else
            {
                foreach(var item in position)
                {
                    PositionDto positionDto = _mapper.Map<Position, PositionDto>(item);
                    positionDtos.Add(positionDto);
                }
            }
            return await Task.FromResult(positionDtos);
        }

        public async Task<List<PositionDto>> GetPositionByDateMonth(DateTime begin, DateTime end)
        {
            var position = _context.Positions.ToList();
            var images = _context.Images.ToList();
            List<PositionDto> positionDtos = new List<PositionDto>();
            if(begin.ToString() != null && end.ToString() != null)
            {
                position = _context.Positions.Where(x => x.datetime >= begin && x.datetime <= end).ToList();
                foreach(var item in position)
                {
                    PositionDto positionDto = _mapper.Map<Position, PositionDto>(item);
                    positionDtos.Add(positionDto);
                }
            }
            else if(begin.ToString() == null)
            {
                position = _context.Positions.Where(x => x.datetime >= begin).ToList();
                foreach(var item in position)
                {
                    PositionDto positionDto = _mapper.Map<Position, PositionDto>(item);
                    positionDtos.Add(positionDto);
                }
            }
            else if(end.ToString() == null)
            {
                position = _context.Positions.Where(x => x.datetime <= end).ToList();
                foreach(var item in position)
                {
                    PositionDto positionDto = _mapper.Map<Position, PositionDto>(item);
                    positionDtos.Add(positionDto);
                }
            }
            return await Task.FromResult(positionDtos);
        }

        public async Task<List<PositionDto>> GetPositionByUser(string username)
        {
            int iduser = _context.AppUsers.FirstOrDefault(x => x.Username == username).Id;
            var position = _context.Positions.Where(x => x.idUser == iduser).ToList();
            var images = _context.Images.ToList();
            List<PositionDto> positionDtos = new List<PositionDto>();
            foreach(var item in position)
            {
                PositionDto positionDto = _mapper.Map<Position, PositionDto>(item);
                positionDtos.Add(positionDto);
            }
            return await Task.FromResult(positionDtos);
        }
        // public Task<List<DrivingStatistic>> DriveStatistics(DateTime date)
        // {
        //     var position = _context.Positions.Where(x => x.datetime.Day == date.Day && x.datetime.Month == date.Month).ToList();
        //     int j = 0;
        //     double avg=0;
        //     double[][] list = new double[position.Count()][];
        //     List<DrivingStatistic> statistics = new List<DrivingStatistic>();
        //     foreach(var i in position)
        //     {
        //         list[j] = new double[2];
        //         int k = 0;
        //         list[j][k] = i.latitude;
        //         k++;
        //         list[j][k] = i.longitude;
        //         j++;
        //         avg+=i.speed;
        //     }
        //     avg/=position.Count();
        //     statistics.Add(new DrivingStatistic{
        //         averageSpeed = avg,
        //         road = list
        //     });
        //     return Task.FromResult(statistics);
        // }
        public Task<DrivingStatistic> DailyDriveStatistics(DateTime date)
        {
            var position = _context.Positions.Where(x => x.datetime.Day == date.Day && x.datetime.Month == date.Month).ToList();
            int j = 0;
            double avg=0;
            double[][] list = new double[position.Count()][];
            DrivingStatistic statistics = new DrivingStatistic();
            foreach(var i in position)
            {
                list[j] = new double[2];
                int k = 0;
                list[j][k] = i.latitude;
                k++;
                list[j][k] = i.longitude;
                j++;
                avg+=i.speed;
            }
            avg/=position.Count();
            statistics.averageSpeed = avg;
            statistics.road = list;
            statistics.date = date.ToShortDateString();
            return Task.FromResult(statistics);
        }

        public Task<DrivingStatistic> DailyDriveStatistics(int day, int month)
        {
            var position = _context.Positions.Where(x => x.datetime.Day == day && x.datetime.Month == month).ToList();
            int j = 0;
            double avg=0;
            double[][] list = new double[position.Count()][];
            DateTime date = DateTime.Now;
            DrivingStatistic statistics = new DrivingStatistic();
            foreach(var i in position)
            {
                list[j] = new double[2];
                int k = 0;
                list[j][k] = i.longitude;
                k++;
                list[j][k] = i.latitude;
                j++;
                avg+=i.speed;
                date = i.datetime;
            }
            avg/=position.Count();
            statistics.averageSpeed = avg;
            statistics.road = list;
            statistics.date = date.ToShortDateString();
            return Task.FromResult(statistics);
        }

        public async Task<List<DrivingStatistic>> MonthlyDriveStatistics(string username, int month)
        {
            var userId = _context.AppUsers.FirstOrDefault(x=> x.Username == username).Id;
            var position = _context.Positions.Where(x=> x.datetime.Month == month && x.idUser == userId).OrderBy(x=> x.datetime.Day).ToList();
            List<DrivingStatistic> statistics = new List<DrivingStatistic>();
            int day = 0;
            foreach(var i in position)
            {
                if(i.datetime.Day != day && day!= 0)
                {
                    var statistic1 = await DailyDriveStatistics(day, month);
                    statistics.Add(statistic1);
                }
                day = i.datetime.Day;
            }
            var statistic = await DailyDriveStatistics(day, month);
            statistics.Add(statistic);
            return statistics;
        }
        // public async Task<List<AllDriverStatistic>> MonthlyDriverStatistics(int month)
        // {
        //     var position = _context.Positions.Where(x=> x.datetime.Month == month).OrderBy(x=> x.datetime.Day).ToList();
        //     List<AllDriverStatistic> statistics = new List<AllDriverStatistic>();
        //     int idUser = 0;
        //     foreach(var i in position)
        //     {
        //         if(i.datetime.Day != day && day!= 0)
        //         {
        //             var statistic1 = await DailyDriveStatistics(day, month);
        //             statistics.Add(statistic1);
        //         }
        //         day = i.datetime.Day;
        //     }
        //     var statistic = await DailyDriveStatistics(day, month);
        //     statistics.Add(statistic);
        //     return statistics;
        // }
    }
}