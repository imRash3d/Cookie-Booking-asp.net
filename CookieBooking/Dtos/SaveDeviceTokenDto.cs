using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Dtos
{
    public class SaveDeviceTokenDto
    {
        public string UserId { get; set; }
        public string DeviceToken { get; set; }
    }
}
