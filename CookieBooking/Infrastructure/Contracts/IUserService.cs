using CookieBooking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Infrastructure.Contracts
{
    public interface IUserService
    {
        List<User> Getusers();
        void SaveUser(User user);
        bool IsEmailExist(string email);
    }
}
