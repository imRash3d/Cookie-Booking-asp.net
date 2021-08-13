using AutoMapper;
using AutoMapper.QueryableExtensions;
using CookieBooking.Constraint;
using CookieBooking.Dtos;
using CookieBooking.Entities;
using CookieBooking.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly DbContextService _context;
        private readonly IMapper _mapper;

        public UserService(DbContextService context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }
        public List<UserDto> Getusers()
        {
            List<UserDto> users = _context.Users
                 .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                 .ToList();

            List<Image> images = GetProfileImages();
            
            foreach( UserDto user in users)
            {
                Image image = images.Find(x => x.ConnectionId == user.UserId);
                user.ProfileImage = image;
                user.ProfileImageUrl = image?.Url;
            }
            return users;
        }

     

        public void SaveUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public bool IsEmailExist(string email)
        {
            return _context.Users.Any(x => x.Email.ToLower() == email.ToLower());
         
        }

        public  UserDto GetUser(string userId)
        {
            UserDto user = _context.Users.Where(x => x.UserId == userId)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .SingleOrDefault();

            Image image = GetProfileImage(user.UserId);
            user.ProfileImage = image;
            user.ProfileImageUrl = image?.Url;
            return user;
        }


        public void SaveUserProfileImage(Image image)
        {
            _context.Add(image);
            _context.SaveChanges();
        }


        public Image  GetProfileImage(string userId)
        {
            return   _context.Images
                .Where(x => x.ConnectionId == userId && x.ConnectionType == StaticKeyValue.User.Key )
                .SingleOrDefault();
        }

        public List<Image> GetProfileImages()
        {
            return _context.Images.Where(x=>x.ConnectionType == StaticKeyValue.User.Key).ToList();
        }

        public void SaveDeviceToken(SaveDeviceTokenDto model)
        {
            var user = _context.Users.SingleOrDefault(x=>x.UserId == model.UserId);
            user.DeviceToken = model.DeviceToken;
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
