using AutoMapper;
using MenuSaz.Identity.Application.Feature.User.Dtos;
using MenuSaz.Identity.Domain.Models;

namespace MenuSaz.Identity.Application.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(t => t.Firstname, opt => opt.MapFrom(c => c.Firstname))
                .ForMember(t => t.Lastname, opt => opt.MapFrom(c => c.Lastname))
                .ForMember(t => t.Username, opt => opt.MapFrom(c => c.Username))
                .ForMember(t => t.PhoneNumber, opt => opt.MapFrom(c => c.PhoneNumber))
                .ForMember(t => t.IsActive, opt => opt.MapFrom(c => c.IsActive));
        }
    }
}
