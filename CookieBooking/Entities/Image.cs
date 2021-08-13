using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Entities
{
    
    public class Image
    {
        public string PublicId { get; set; }
        public string Url { get; set; }
        public string Id { get; set; }
        public string ConnectionType { get; set; }
        public string ConnectionId { get; set; }
        //public User User { get; set; }
    }
}
