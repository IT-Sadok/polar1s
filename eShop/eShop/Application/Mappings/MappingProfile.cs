using AutoMapper;
using eShop.Application.DTOs.Register;
using eShop.Persistence.Models;

namespace eShop.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserDTO, ApplicationUser>();
        }
    }
}
