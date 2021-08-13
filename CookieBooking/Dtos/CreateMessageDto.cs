using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Dtos
{
    public class CreateMessageDto
    {
        public string ReceiverId { get; set; }
        public string Content { get; set; }
    }
}
