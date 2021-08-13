using CookieBooking.Dtos;
using CookieBooking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Infrastructure.Contracts
{
    public interface IMailService
    {
     
        void SendMail(EmailModelDto emailDataDto);
       
    }
}
