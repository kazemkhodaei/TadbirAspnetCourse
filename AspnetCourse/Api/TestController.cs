using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspnetCourse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AspnetCourse.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController(IConfiguration configuration, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            Configuration = configuration;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
        }

        public IConfiguration Configuration { get; }
        public UserManager<IdentityUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public SignInManager<IdentityUser> SignInManager { get; }

        [HttpPost("create-user")]
        public async Task CreateUser(User user)
        {
            var identityUser = new IdentityUser
            {
                UserName = user.Name,
                Email = user.Email
            };

            IdentityResult result = await UserManager.CreateAsync(identityUser, user.Password);
        }

        public async Task CreateRole(IdentityRole role)
        {
            var identityUser = new IdentityRole
            {
                Name = role.Name
            };
            
            IdentityResult result = await RoleManager.CreateAsync(role);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<string> Login([FromBody]User details)
        {
            var returnUrl = "/";
            string token = null;

            if (ModelState.IsValid)
            {
                IdentityUser user = await UserManager.FindByEmailAsync(details.Email);

                UserManager.AddClaimAsync(null, new Claim(ClaimTypes.StreetAddress, "tehran - tadbir"));
                if (user != null)
                {
                    // cancels any existing session that the user has
                    await SignInManager.SignOutAsync();

                    var result = await SignInManager.CheckPasswordSignInAsync(user, details.Password, false);
                    
                    if (result.Succeeded)
                    {
                        // authentication successful so generate jwt token
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes("jwt secret code as string");

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                new Claim(ClaimTypes.Name, user.Id)
                            }),
                            Expires = DateTime.UtcNow.AddMinutes(1),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };

                        var tokenObj = tokenHandler.CreateToken(tokenDescriptor);
                        token = tokenHandler.WriteToken(tokenObj);
                    }
                }
            }

            return token;
        }

        [HttpPost("session")]
        public async Task<ContentResult> SetSession()
        {
            HttpContext.Session.SetString("name", "my value 123");
            await Task.Delay(5000);
            return Content(HttpContext.User.Identity.Name);
        }

        [HttpGet("session")]
        public async Task<ContentResult> GetSession()
        {
            return Content(HttpContext.Session.GetString("name"));
        }

        [Authorize(Policy = "NotAdminPolicy")]
        [HttpPut]
        public async Task<ContentResult> Info()
        {
            return Content(HttpContext.User.Identity.Name);
        }

        [Authorize]
        [HttpPatch]
        public async Task<ContentResult> Info2()
        {
            return Content(HttpContext.User.Identity.Name);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}