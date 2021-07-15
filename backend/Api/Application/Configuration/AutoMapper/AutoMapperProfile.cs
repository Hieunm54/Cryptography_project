using AutoMapper;
using APITemplate.Dto;
using APITemplate.Domain.Entities;

namespace APITemplate.Application.Configuration.AutoMapper
{

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Block, BlockDto>().ReverseMap();
        }
    }
}
