
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

namespace CookieBooking.Controllers
{
    public class AccountController : BaseApiController
    {
         private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly DbContextService _context;
        public AccountController(IUserService userService , DbContextService context , ITokenService tokenService)
        {
            _userService = userService;
            _context = context;
            _tokenService = tokenService;
        }




        
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto )

        {

            if (!_userService.IsEmailExist(loginDto.Email)) return Unauthorized("Email does not exist ");

            User user = _context.Users.SingleOrDefault(x => x.Email == loginDto.Email);


            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i=0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            UserDto userDto = new UserDto
            {
                UserId = user.UserId,
                Token = _tokenService.CreateToken(user),
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return await Task.FromResult(userDto); ;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if (  _userService.IsEmailExist(registerDto.Email)) return BadRequest("Email Already Exist ");
           

               using var hmac = new HMACSHA512();

    
                var userModel = new User
                {
                   // CreatedDate = DateTime.Now.t,
                    UserId = Guid.NewGuid().ToString(),
                    Email = registerDto.Email.ToLower(),
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Mobile = registerDto.Mobile,
                    Role = registerDto.Role,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                    PasswordSalt = hmac.Key
                };

              _userService.SaveUser(userModel);
              
                UserDto userDto = new UserDto
                    {
                        UserId = userModel.UserId,
                        Token = _tokenService.CreateToken(userModel),
                        FirstName = userModel.FirstName,
                        LastName = userModel.LastName,
                    };
                    return await Task.FromResult(userDto);
          
        }


       



    }
}
