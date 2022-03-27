using AutoMapper;
using DatingAppSocial.DTOs;
using DatingAppSocial.Entities;
using DatingAppSocial.Extensions;
using System.Linq;

namespace DatingAppSocial.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MembersDto>()
                .ForMember(dest => dest.PhotoUrl,opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url)).ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>();
            CreateMap<memberUpdateDto, AppUser>();
            CreateMap<RegisterDto, AppUser>();
        }
    }
}
