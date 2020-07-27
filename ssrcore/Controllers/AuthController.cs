using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.Services;
using ssrcore.ViewModels;

namespace ssrcore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IStaffService _staffService;
        private readonly IFcmTokenService _fcmTokenService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthController(IUserService userService, IRoleService roleService, IStaffService staffService,
                              IFcmTokenService fcmTokenService, IMapper mapper, IConfiguration config)
        {
            _userService = userService;
            _roleService = roleService;
            _staffService = staffService;
            _fcmTokenService = fcmTokenService;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = _mapper.Map<Users>(model);
            var result = await _userService.CreateUser(user, model.Password);
            if(result != null)
            {
                return Created("", result);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            UserModel staff = null;
            var user = await _userService.GetByUserName(model.Username);
            var result = await _userService.CheckPassWord(model.Username, model.Password);
            if (user != null && result)
            {
                var role = _roleService.GetRole(user);
                if(role == "Staff")
                {
                    staff = await _staffService.GetStaff(user.Id);
                }

                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, role)
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                var firebaseProject = _config.GetSection("AppSettings:FirebaseProject").Value;
                var token = new JwtSecurityToken(
                    issuer: "https://securetoken.google.com/" + firebaseProject,
                    audience: firebaseProject,
                    expires: DateTime.Now.AddYears(13),
                    claims: authClaims,
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    role = role,
                    email = user.Email,
                    fullName = user.FullName,
                    username = user.Username,
                    photo = user.Photo,
                    departmentId = staff != null ? staff.DepartmentId : null,
                    departmentNm = staff != null ? staff.DepartmentNm : null,
                    expiration = token.ValidTo

                });
            }
            return Unauthorized();
        }


        [HttpPost("Google")]
        public async Task<IActionResult> VerifyToken(LoginRequest request)
        {
            var auth = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance;
            var decodeToken = await auth.VerifyIdTokenAsync(request.IdToken);
            if (decodeToken != null)
            {
                string uid = decodeToken.Uid;
                UserRecord user_firebase = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
                int indexEmail = user_firebase.Email.LastIndexOf("@fpt.edu.vn");
                if (indexEmail > 0)
                {
                    var currentUser = await _userService.GetByUserName(uid, "Login");

                    if (currentUser == null)
                    {
                        var user_info = new Users
                        {
                            Username = uid,
                            RoleId = Constants.Roles.ROLE_STUDENT,
                            Email = user_firebase.Email,
                            Phonenumber = user_firebase.PhoneNumber,
                            UserNo = Utils.GetUserNo(user_firebase.Email, user_firebase.DisplayName),
                            FullName = user_firebase.DisplayName,
                            Address = "",
                            DelFlg = false,
                            Photo = user_firebase.PhotoUrl,
                            InsBy = "Admin",
                            InsDatetime = DateTime.Now,
                            UpdBy = "Admin",
                            UpdDatetime = DateTime.Now
                        };
                        currentUser = await _userService.CreateUser(user_info, Constants.Users.PASSWORD);

                    }
                    await _fcmTokenService.CreateFcmToken(currentUser.Id, request.FcmToken);

                    var role = _roleService.GetRole(currentUser);
                    var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, currentUser.Username),
                    new Claim(ClaimTypes.NameIdentifier, currentUser.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, role)
                };

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                    var apiUrl = _config.GetSection("AppSettings:Url").Value;
                    var firebaseProject = _config.GetSection("AppSettings:FirebaseProject").Value;
                    var token = new JwtSecurityToken(
                        issuer: "https://securetoken.google.com/" + firebaseProject,
                        audience: firebaseProject,
                        expires: DateTime.Now.AddYears(13),
                        claims: authClaims,
                        signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
                        );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        role = role,
                        email = currentUser.Email,
                        fullName = currentUser.FullName,
                        photo = currentUser.Photo,
                        username = currentUser.Username,
                        expiration = token.ValidTo
                    });
                }
            }

            return BadRequest();
        }



    }
}