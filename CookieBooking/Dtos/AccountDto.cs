using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Dtos
{
    public class AccountDto
    {
        public string UserId { get; set; }

        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
