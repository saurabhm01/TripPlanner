using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Tripzz.Service.Interface;

namespace Trippzz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        public IConfigurationRoot Configuration { get; }


        public AuthController(IUserService _userService, IHostingEnvironment env)
        {
            userService = _userService;
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();
            Configuration = builder.Build();
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[HttpPost, Route("login/saveuser")]
        //public JsonResult SaveUserDetail(UserDetailModel modal)
        //{
        //    try
        //    {
        //        var entity = new tUserDetails();
        //        entity.Name = modal.Name;
        //        entity.Password = modal.Password;
        //        userService.AddOrUpdateUserDetail(entity);

        //        var token = GetJwtSecurityToken(entity);

        //        if (Configuration != null)
        //        {
        //            return Json(new
        //            {
        //                status = "success",
        //                token = new JwtSecurityTokenHandler().WriteToken(token),
        //                expiration = token.ValidTo
        //            });
        //        }

        //    }
        //    catch (Exception error)
        //    {
        //        return Json(new { status = "failure" });
        //        throw error;
        //    }
        //    return Json(new { status = "success" });
        //}

        //[HttpPost, Route("login/token")]
        //public IActionResult CreateToken(UserDetailModel model)
        //{

        //    if (string.IsNullOrWhiteSpace(model.Name) && string.IsNullOrWhiteSpace(model.Password))
        //    {
        //        return BadRequest("Failed to login");

        //    }

        //    var user = userService.VerifyUserDetail(model.Name, model.Password);

        //    if (user == null)
        //    {
        //        return BadRequest("Invalid login name or password.");
        //    }

        //    var token = GetJwtSecurityToken(user);

        //    if (Configuration != null)
        //    {
        //        return Ok(new
        //        {
        //            token = new JwtSecurityTokenHandler().WriteToken(token),
        //            expiration = token.ValidTo
        //        });
        //    }
        //    return BadRequest("Unable to process the request");
        //}
        //private static IEnumerable<Claim> GetTokenClaims(tUserDetails user)
        //{
        //    return new List<Claim> {
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Sub, user.Name)
        //    };
        //}

        //private JwtSecurityToken GetJwtSecurityToken(tUserDetails user)
        //{
        //    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("TokenAuthentication:SecretKey").Value));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    return new JwtSecurityToken(
        //        issuer: Configuration.GetSection("TokenAuthentication:Issuer").Value,
        //        audience: Configuration.GetSection("TokenAuthentication:Audience").Value,
        //        claims: GetTokenClaims(user),
        //        expires: DateTime.UtcNow.AddHours(24),
        //        signingCredentials: creds
        //    );
        //}


    }
}