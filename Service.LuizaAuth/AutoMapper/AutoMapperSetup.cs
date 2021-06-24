using AutoMapper;
using Domain.LuizaAuth.Entities;
using Domain.LuizaAuth.DTOs;

namespace Service.LuizaAuth.AutoMapper
{
    public class AutoMapperSetup: Profile
    {
        public AutoMapperSetup()
        {
            CreateMap<UserDto, User>().ReverseMap();            
        }
    }
}
