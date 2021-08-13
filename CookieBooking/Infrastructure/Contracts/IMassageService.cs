using CookieBooking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Infrastructure.Contracts
{
   public interface IMessageService
    {
        void AddMessage(Message message);
        List<Message> GetSendMessages(string senderId);
        List<Message> GetReceivedMessages(string receiverId);

    }
}
