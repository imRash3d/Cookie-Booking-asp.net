using CookieBooking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Infrastructure.Contracts
{
   public interface ITokenService
    {
        string CreateToken(User user);
    }
}
