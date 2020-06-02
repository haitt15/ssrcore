using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ssrcore.Repositories;
using ssrcore.ViewModels;

namespace ssrcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("hai")]
        public IActionResult Test1()
        {
            return new JsonResult("UnAuthorized");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Test2()
        {
            return new JsonResult("Authorized");
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyToken(LoginRequest request)
        {
            var auth = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance;

            try
            {
                var response = await auth.VerifyIdTokenAsync(request.IdToken);
                if (response != null)
                    return Accepted();
            }
            catch (FirebaseException ex)
            {
                return BadRequest();
            }

            return BadRequest();
        }
    }
}