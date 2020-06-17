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

namespace ssrcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IFcmTokenRepository _fcmTokenRepository;

        public UsersController(IUserRepository userRepository, IFcmTokenRepository fcmTokenRepository)
        {
            _userRepository = userRepository;
            _fcmTokenRepository = fcmTokenRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            var currentUser = await _userRepository.FindByUid("JcNIdM8KXYglFppYcWnIIXTbyqg2");
            int UserId = currentUser.Id;
            List<string> ListFcmToken = _fcmTokenRepository.GetFcmToken(UserId);
            foreach (string FcmToken in ListFcmToken)
            {
                await Helpers.Utils.PushNotificationAsync(FcmToken, "Title", "Message");
            }
            return Ok(users);
        }
    }
}