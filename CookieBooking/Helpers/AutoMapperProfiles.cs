using AutoMapper;
using CookieBooking.Dtos;
using CookieBooking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>();
                //.ForMember(y=>y.ProfileImageUrl , option=>option.MapFrom(x=>x.))
        }
    }
}
