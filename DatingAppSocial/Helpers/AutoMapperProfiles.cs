using AutoMapper;
using DatingAppSocial.DTOs;
using DatingAppSocial.Entities;
using System.Linq;

namespace DatingAppSocial.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MembersDto>()
                .ForMember(dest => dest.PhotoUrl,opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<Photo, PhotoDto>();
        }
    }
}
