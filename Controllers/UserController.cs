using System;
using System.Linq;
using System.Threading.Tasks;
using Freelance_Api.Models;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCompany([FromBody] RegisterCompany model)
        {
            if (ModelState.IsValid)
            {
                var user = new Company
                    {UserName = model.CompanyName, Email = model.Email, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now};
                var result = await _userManager.CreateAsync(user, model.Password);
                
                if (!result.Succeeded)
                    return Ok(string.Join(",",
                        result.Errors?.Select(error => error.Description) ?? throw new InvalidOperationException()));
                await _signInManager.SignInAsync(user, false);
                var token = AuthHelperService.GenerateJwtToken(model.Email, user,_configuration);

                var rootData = new LoginResponse(token);
                return Created("api/v1/authentication/register", rootData);
            }

            var errorMessage =
                string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return BadRequest(errorMessage);
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterStudent model)
        {
            if (ModelState.IsValid)
            {
                var user = new Student
                {
                    Firstname = model.FirstName, Lastname = model.LastName, UserName = model.UserName,
                    Email = model.Email,
                    CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                
                if (!result.Succeeded)
                    return Ok(string.Join(",",
                        result.Errors?.Select(error => error.Description) ?? throw new InvalidOperationException()));
                await _signInManager.SignInAsync(user, false);
                var token = AuthHelperService.GenerateJwtToken(model.Email, user,_configuration);
                var rootData = new LoginResponse(token);
                return Created("api/v1/authentication/register", rootData);
            }

            var errorMessage =
                string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return BadRequest(errorMessage);
        }

        [HttpGet]
        public async Task<ActionResult> GetUserData()
        {
            var user = await _userManager.GetUserAsync(User);
            return Ok(user);
        }
        
        [HttpPost]
        public async Task<ActionResult> UpdateStudent([FromBody] Student model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (!ModelState.IsValid || user == null) return Ok(user);
            user = model;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return Ok(string.Join(",",
                    result.Errors?.Select(error => error.Description) ?? throw new InvalidOperationException()));
            var errorMessage =
                string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return BadRequest(errorMessage);

        }
    }
}