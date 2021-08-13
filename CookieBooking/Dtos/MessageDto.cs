using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Dtos
{
    public class ReceivedMessageDto
    {

         
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string Content { get; set; }

        public string ReceiverName { get; set; }
        public string ReceiverImgUrl { get; set; }
        public string SenderName{ get; set; }

    }
}
