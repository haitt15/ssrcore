using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.Repositories;
using ssrcore.Services;
using ssrcore.ViewModels;

namespace ssrcore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IStaffService _staffService;
        private readonly IMapper _mapper;
        private readonly IFcmTokenService _fcmTokenService;

        public UsersController(IUserService userService, IStaffService staffService,
                               IFcmTokenService fcmTokenService, IMapper mapper)
        {
            _userService = userService;
            _staffService = staffService;
            _fcmTokenService = fcmTokenService;
            _mapper = mapper;
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

        [HttpPut("{username}")]
        public async Task<ActionResult> UpdateUser(string username, [FromBody] UserModel model)
        {
            var user = await _userService.GetByUserName(username);
            if (user == null)
            {
                return NotFound();
            }

            if (user.RoleId == Constants.Roles.ROLE_STAFF)
            {
                var newStaff = await _staffService.UpdateStaff(user.Id, model);
                return Ok(newStaff);
            }

            var result = await _userService.UpdateUser(username, model);
            return Ok(result);
        }

        [HttpDelete("{username}")]
        public async Task<ActionResult> DeleteUser(string username)
        {
            var result = await _userService.DeleteUser(username);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPost("Staffs")]
        public async Task<IActionResult> CreateStaff([FromBody] UserModel model)
        {

            var user = _mapper.Map<Users>(model);
            var result = await _userService.CreateUser(user, model.Password);
            if (result != null)
            {
                var staffModel = _mapper.Map<UserModel>(result);
                staffModel.DepartmentId = model.DepartmentId;
                var staff = await _staffService.CreateStaff(staffModel);
                return Created("", staff);
            }

            return BadRequest();
        }

        [HttpGet("Staffs")]
        public async Task<IActionResult> GetAllStaff([FromQuery] SearchStaffModel model)
        {
            var result = await _staffService.GetAllStaffs(model);
            return Ok(new
            {
               data = result,
               totalCount = result.TotalCount,
               tolalPages = result.TotalPages
            });
        }



    }
}