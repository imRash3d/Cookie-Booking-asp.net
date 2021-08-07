using CookieBooking.Entities;
using CookieBooking.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Infrastructure.Services
{
    public class UserService : IUserService
    {
        public readonly DbContextService _context;


        public UserService(DbContextService context)
        {
            _context = context;
        }
        public List<User> Getusers()
        {
           return _context.Users.ToList();
           
        }

        public User Login(string userName)
        {
            throw new NotImplementedException();
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
    }
}
