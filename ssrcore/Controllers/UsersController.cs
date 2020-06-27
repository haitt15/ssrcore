using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.Repositories;
using ssrcore.Services;

namespace ssrcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IFcmTokenService _fcmTokenService;

        public UsersController(IUserService userService, IFcmTokenService fcmTokenService)
        {
            _userService = userService;
            _fcmTokenService = fcmTokenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            //var currentUser = await _userService.GetByUserName("JcNIdM8KXYglFppYcWnIIXTbyqg2");
            //int UserId = currentUser.Id;
            //List<string> ListFcmToken = await _fcmTokenService.GetAllFcmToken(UserId);
            //foreach (string FcmToken in ListFcmToken)
            //{
            //    await Helpers.Utils.PushNotificationAsync(FcmToken, "Title", "Message");
            //}
            return Ok(users);
        }
    }
}