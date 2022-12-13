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
    }
}