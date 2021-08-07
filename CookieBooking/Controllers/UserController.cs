
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

namespace CookieBooking.Controllers
{
    public class UserController : BaseApiController
    {
       // private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
           // _tokenService = tokenService;
        }




        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()

        {
            return _userService.Getusers();
        }

        



    }
}
