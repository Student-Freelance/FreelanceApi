using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Freelance_Api.Models.CampusNet;
using Freelance_Api.Models.Identity;
using Freelance_Api.Models.Requests;
using Freelance_Api.Models.Responses;
using Freelance_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Freelance_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
     

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginEntity model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (!result.Succeeded) return StatusCode((int) HttpStatusCode.Unauthorized, "Bad Credentials");
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.UserName);
                var token = AuthHelperService.GenerateJwtToken(model.UserName, appUser);
                var rootData = new LoginResponse(token);
                return Ok(rootData);
            }
            var errorMessage =
                string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return BadRequest(errorMessage ?? "Bad Request");
        }


        [HttpPost]
        public async Task<IActionResult> CampusNetLogin([FromBody] CnUserAuth cNUserAuthBody)
        {
            int responseStatusCode;

            responseStatusCode = await HttpService.UserCampusNetAuthHttpRequestAsync(cNUserAuthBody);
            Console.WriteLine(responseStatusCode);


            if (responseStatusCode == 401)
            {
                return BadRequest(ModelState);
            }

            return Ok(responseStatusCode);
        }
    }
}