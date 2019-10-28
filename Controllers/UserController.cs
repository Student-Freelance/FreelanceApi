using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Freelance_Api.Models.Identity;
using Freelance_Api.Models.Requests;
using Freelance_Api.Models.Responses;
using Freelance_Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Freelance_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    // [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


         // GET api/user/userdata
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult> UserData()
        {
            var user = await _userManager.GetUserAsync(User);
            var userData = new AppUser()
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Location = user.Location,
                Email = user.Email
            };
            return Ok(userData);
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterEntity model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { Firstname = model.FirstName, Lastname = model.LastName, Location = model.Location, UserName = model.Email, Email = model.Email, CreatedOn  = DateTime.Now };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return Ok(string.Join(",",
                        result.Errors?.Select(error => error.Description) ?? throw new InvalidOperationException()));
                await _signInManager.SignInAsync(user, false);
                var token = AuthHelperService.GenerateJwtToken(model.Email, user, _configuration);

                var rootData = new LoginResponse(token, user.Firstname, user.Email);
                return Created("api/v1/authentication/register", rootData);
            }
            var errorMessage = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return BadRequest(errorMessage);
        }

     
        // POST api/user/login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody]LoginEntity model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (!result.Succeeded) return StatusCode((int) HttpStatusCode.Unauthorized, "Bad Credentials");
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                var token = AuthHelperService.GenerateJwtToken(model.Email, appUser, _configuration);

                var rootData = new LoginResponse(token, appUser.UserName, appUser.Email);
                return Ok(rootData);
            }
            var errorMessage = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return BadRequest(errorMessage ?? "Bad Request");
        }
    }
}