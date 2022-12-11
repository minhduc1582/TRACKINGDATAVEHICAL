using AutoMapper;
using eshop_pbl6.Entities;
using eshop_pbl6.Models.DTO.Positions;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Position, PositionDto>();
    }
}