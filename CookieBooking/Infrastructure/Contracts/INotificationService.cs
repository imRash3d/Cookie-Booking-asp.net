using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Infrastructure.Contracts
{
    public interface INotificationService
    {
          Task<bool> NotifyAsync(string to, string title, string body);
    }
}
