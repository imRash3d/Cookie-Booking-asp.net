
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
    public class UserController : BaseApiController
    {
       
        private readonly IUserService _userService;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        private readonly IPhotoService _photoService;
        public UserController(IUserService userService , 
            IMailService mailService , 
            IConfiguration configuration,
            IPhotoService photoService
            )
        {
            _userService = userService;
            _mailService = mailService;
            _configuration = configuration;
            _photoService = photoService;
        }




        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()

        {
            var users = _userService.Getusers();
            return await Task.FromResult(users);
        }



        [Authorize]
        [HttpGet("{id}")]
        public async Task< ActionResult<UserDto>> GetUser(string id )

        {
            var user = _userService.GetUser(id);
            return await Task.FromResult(user);
        }

        [Authorize]
      //  [HttpGet("{id}")]
        public async Task<ActionResult<bool>> SendMail(string id)

        {
            UserDto user = _userService.GetUser(id);
            var dateContext = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            dateContext.Add("DisplayName", user.FirstName +" " + user.LastName);
          // dateContext.Add("date", "05 Aug 2009");
          // dateContext.Add("amount", "GBP200");

            EmailModelDto emailModel = new EmailModelDto
            {
                EmailTemplateName = _configuration["EmailTemplateName"],
                To = user.Email,
                DataContext = dateContext
            };
           
            _mailService.SendMail(emailModel);
          
            return await Task.FromResult(true);

        }

        [Authorize]
        [HttpPost("upload-photo")]
        public async Task<ActionResult<Image>> UploadImage(IFormFile file )

        {

            string userId = User.GetUserId();

            var result = await _photoService.UploadPhoto(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            Image image = new Image
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                ConnectionId = userId,
                ConnectionType = StaticKeyValue.User.Key,
                Id= Guid.NewGuid().ToString()
            };

            _userService.SaveUserProfileImage(image);
            return await Task.FromResult(image);

        }


        [Authorize]
        [HttpPost("save-device-token")]
        public async Task<ActionResult<CommandResponse>> SaveDeviceToken(SaveDeviceTokenDto model)

        {

            CommandResponse response = new CommandResponse();
            response.Result = null;
            if (!String.IsNullOrEmpty(model.DeviceToken))
            {
                SaveDeviceTokenDto deviceTokenDto = new SaveDeviceTokenDto
                {
                    UserId = User.GetUserId(),
                    DeviceToken = model.DeviceToken
                };

                _userService.SaveDeviceToken(deviceTokenDto);
                response.Success = true;

             
            }else
            {
                response.Success = false;
                response.ErrorMessage = "Device Token can not be null";
                
            }
            return await Task.FromResult(response);
        }
    }
}
