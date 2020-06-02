using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ssrcore.Repositories;
using ssrcore.ViewModels;

namespace ssrcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        //private RoleManager<Roles> _roleManager;

        //public RoleController(RoleManager<Roles> roleManager)
        //{
        //    _roleManager = roleManager;
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateRole([FromBody] RoleModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var role = new Roles { Name = model.Role};
        //        var result = await _roleManager.CreateAsync(role);

        //        if (result.Succeeded)
        //        {
        //            return new JsonResult("Create successfully");
        //        }
        //        return new JsonResult(result.Errors);
        //    }
        //    return BadRequest();
        //}
    }
}