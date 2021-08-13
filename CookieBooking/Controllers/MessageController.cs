
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CookieBooking.Infrastructure.Contracts;
using CookieBooking.Entities;
using CookieBooking.Dtos;
using CookieBooking.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using CookieBooking.Extensions;
using CookieBooking.Constraint;
using CookieBooking.Helpers;

namespace CookieBooking.Controllers
{
    public class MessageController : BaseApiController
    {
       
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMessageService _messageService;
        public MessageController(
            IUserService userService , 
            IConfiguration configuration,
            IMessageService messageService
            )
        {
            _userService = userService;
            _configuration = configuration;
            _messageService = messageService;
        }




        [Authorize]
        [HttpGet("send-messages")]
        public async Task<ActionResult<CommandResponse>> SendMessages()

        {

            CommandResponse response = new CommandResponse();
            string userId = User.GetUserId();
            List<Message> results = _messageService.GetSendMessages(userId);
            response.Result = results;
            response.Success = true;
            return await Task.FromResult(response);
        }


        [Authorize]
        [HttpGet("received-messages")]
        public async Task<ActionResult<CommandResponse>> ReceivedMessages()

        {
            CommandResponse response = new CommandResponse();
            string userId = User.GetUserId();
            List<Message> results = _messageService.GetReceivedMessages(userId);
            response.Result = results;
            response.Success = true;
            return await Task.FromResult(response);
        }


        [Authorize]
        [HttpPost("send")]
        public async Task<ActionResult<CommandResponse>> SendMessage(CreateMessageDto createMessageDto )

        {
            CommandResponse response = new CommandResponse();
            string senderId = User.GetUserId();
            var sender = _userService.GetUser(senderId);
            var receiver = _userService.GetUser(createMessageDto.ReceiverId);
            Message message = new Message
            {
                ReceiverId = createMessageDto.ReceiverId,
                SenderId = User.GetUserId(),
                Content = createMessageDto.Content,
                SenderImgUrl= sender.ProfileImageUrl,
                SenderName= sender.FirstName + " "+sender.LastName,
                ReceiverImgUrl = receiver.ProfileImageUrl,
                ReceiverName = receiver.FirstName + " " + receiver.LastName
            };

            _messageService.AddMessage(message);
            response.Result = message;
            response.Success = true;
            return await Task.FromResult(response);

        }
    }
}
