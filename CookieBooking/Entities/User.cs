using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Entities
{
    public class User
    {
        public string UserId { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Role { get; set; }
        public string Address { get; set; }
        public string DeviceToken { get; set; }
       // public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ProfileImageUrl { get; set; }
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
      //  public Image ProfileImage { get; set; }
    }
}
