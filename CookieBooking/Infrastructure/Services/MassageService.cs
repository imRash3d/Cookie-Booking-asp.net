using AutoMapper;
using CookieBooking.Dtos;
using CookieBooking.Entities;
using CookieBooking.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly DbContextService _context;
        private readonly IMapper _mapper;
  
        public MessageService(DbContextService context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
      
        }
        public void AddMessage(Message message)
        {

            _context.Messages.Add(message);
            _context.SaveChanges();
        }

        public List<Message> GetSendMessages(string senderId)
        {
            return _context.Messages.Where(x => x.SenderId == senderId).ToList();
        }
        public List<Message> GetReceivedMessages(string receiverId)
        {
            return _context.Messages.Where(x => x.ReceiverId == receiverId).ToList();
        }
    }
}
