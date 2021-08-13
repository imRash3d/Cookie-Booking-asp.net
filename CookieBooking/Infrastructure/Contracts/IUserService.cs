using CookieBooking.Dtos;
using CookieBooking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Infrastructure.Contracts
{
    public interface IUserService
    {
        List<UserDto> Getusers();
        UserDto GetUser(string userId);
        void SaveUser(User user);
        void SaveUserProfileImage(Image image);
        void SaveDeviceToken(SaveDeviceTokenDto model);
        bool IsEmailExist(string email);
    }
}
