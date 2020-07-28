using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ssrcore.Helpers;
using ssrcore.Models;
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
  

        public UsersController(IUserService userService, IStaffService staffService, IMapper mapper)
        {
            _userService = userService;
            _staffService = staffService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsers();
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
            var result = await _userService.UpdateUser(username, model);
            if (user.RoleId == Constants.Roles.ROLE_STAFF)
            {
                var newStaff = await _staffService.UpdateStaff(user.Id, model);
                result.DepartmentId = newStaff.DepartmentId;
                result.DepartmentNm = newStaff.DepartmentNm;
            }
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