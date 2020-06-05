using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
using ssrcore.Repositories;
using ssrcore.ViewModels;

namespace ssrcore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;


        public AuthController(IUserRepository userRepository, IRoleRepository roleRepository,
                                      IMapper mapper, IConfiguration config)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _config = config;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = _mapper.Map<Users>(model);
            try
            {
                var result = await _userRepository.Create(user, model.Password);
                await _userRepository.Save();
                return Created("", result);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userRepository.FindByUsername(model.Username);
            var result = await _userRepository.CheckPassword(model.Username, model.Password);
            if (user != null && result)
            {
                var role = _roleRepository.FindRole(user);
                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("role", role)
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                var apiUrl = _config.GetSection("AppSettings:Url").Value;
                var token = new JwtSecurityToken(
                    issuer: apiUrl,
                    audience: apiUrl,
                    expires: DateTime.Now.AddYears(13),
                    claims: authClaims,
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    role = role,
                    username = user.Username,
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
                    var user = await _userRepository.FindByUid(uid);
                    if (user == null)
                    {
                        var user_info = new Users
                        {
                            Uid = uid,
                            Username = RandomString(8, true),
                            RoleId = Constants.Roles.ROLE_STUDENT,
                            Email = user_firebase.Email,
                            Phonenumber = user_firebase.PhoneNumber,
                            UserNo = GetUserNo(user_firebase.Email, user_firebase.DisplayName),
                            FullName = user_firebase.DisplayName,
                            Address = "",
                            DelFlg = false,
                            Photo = user_firebase.PhotoUrl,
                            InsBy = "Admin",
                            InsDatetime = DateTime.Now,
                            UpdBy = "Admin",
                            UpdDatetime = DateTime.Now
                        };
                        await _userRepository.Create(user_info, Constants.Users.PASSWORD);
                        await _userRepository.Save();
                    }
                    string jwt_token = await auth.CreateCustomTokenAsync(uid);
                    return Ok(new
                    {
                        token = jwt_token,
                        role = Constants.Roles.ROLE_STUDENT,
                        email = user_firebase.Email,
                        fullName = user_firebase.DisplayName
                    });
                }
            }

            return BadRequest();
        }

        private string GetUserNo(String email, String displayName)
        {
            int lastIndex = displayName.LastIndexOf("(");  //lastIndexOf(" ");
            string fullname = displayName.Substring(0, lastIndex).Trim();
            string[] arr = fullname.Split(" ");
            string username = arr[arr.Length - 1];
            int lengthPre = username.Length - 1 + arr.Length;
            email = email.Substring(lengthPre);
            int indexEmail = email.LastIndexOf("@fpt.edu.vn");
            string result = email.Substring(0, indexEmail);
            return result.ToUpper();
        }
        private String GetFirstName(String FullName)
        {
            return "";
        }
        private String GetLastName(String FullName)
        {
            return "";
        }

        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}