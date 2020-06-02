using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ssrcore.Helpers;
using ssrcore.Repositories;
using ssrcore.ViewModels;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace ssrcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        //private UserManager<Users> _userManager;
        //private readonly IMapper _mapper;
        //private readonly IConfiguration _config;


        //public AuthenticateController(UserManager<Users> userManager, IMapper mapper, IConfiguration config)
        //{
        //    _userManager = userManager;
        //    _mapper = mapper;
        //    _config = config;
        //}


        //[HttpPost]
        //[Route("Register")]
        //public async Task<IActionResult> Register([FromBody] RegisterModel model)
        //{
        //    var user = new Users()
        //    {
        //        UserName = model.UserName,
        //        Email = model.Email,
        //        FullName = model.FullName,
        //        PhoneNumber = model.PhoneNumber,
        //    };

        //    try
        //    {
        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            await _userManager.AddToRoleAsync(user, model.Role);
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        //[HttpPost]
        //[Route("Login")]
        //public IActionResult Login([FromBody] LoginModel model)
        //{
        //    var user = _userRepository.FindByUsername(model.Username);
        //    if (user != null && _userRepository.CheckPassword(model.Username, model.Password))
        //    {
        //        return Ok(new
        //        {
        //            id_token = GenerateToken(user)
        //        }); 
        //    }
        //    return Unauthorized();
        //}


        //[HttpPost("auth/google")]
        //[ProducesDefaultResponseType]
        //public async Task<JsonResult> GoogleLogin(GoogleLoginRequest request)
        //{
        //    Payload payload;
        //    try
        //    {
        //        payload = await ValidateAsync(request.IdToken, new ValidationSettings
        //        {
        //            Audience = new[] { _config.GetSection("AppSettings:ClientId").Value }
        //        });

        //    }
        //    catch
        //    {
        //        // Invalid token
        //    }
        //    return null;
        //}

        //public async Task<Users> GetOrCreateExternalLoginUser(string provider, string key, string email, string firstName, string lastName)
        //{
        //    var user = await _userRepository.FindByLogin(provider, key);
        //    if (user != null)
        //        return user;

        //    user = await _userRepository.FindByEmail(email);
        //    if (user == null)
        //    {
        //        user = new Users
        //        {
        //            Email = email,
        //            Username = email,
        //            FullName = firstName + " " + lastName,
        //        };

        //        await _userRepository.Create(user, "123456");
        //    }

        //    var info = new ViewModels.UserLoginInfo
        //    {
        //        Provider = provider,
        //        Key = key,
        //        ProviderDisplayName = provider.ToUpperInvariant()
        //    };
        //    var result = await _userRepository.AddUserLogin(user, info);
        //    if (result)
        //    {
        //        return user;
        //    }

        //    return null;
        //}

        //public string GenerateToken(Users user)
        //{
        //    var role = _roleRepository.FindRole(user);
        //    var authClaims = new List<Claim>
        //        {
        //            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        //            new Claim(ClaimTypes.NameIdentifier, user.Username),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //            new Claim("roles", role)
        //        };

        //    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
        //    var apiUrl = _config.GetSection("AppSettings:Url").Value;
        //    var token = new JwtSecurityToken(
        //        issuer: apiUrl,
        //        audience: apiUrl,
        //        expires: DateTime.Now.AddYears(13),
        //        claims: authClaims,
        //        signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
        //        );

        //    string id_token = new JwtSecurityTokenHandler().WriteToken(token);

        //    return id_token;
        //}


    }
}