using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Freelance_Api.Models;
using Freelance_Api.Models.CampusNet;
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
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUserModel> _signInManager;
        private readonly UserManager<AppUserModel> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _imapper;

        public AccountController(SignInManager<AppUserModel> signInManager, UserManager<AppUserModel> userManager,
            IConfiguration configuration, IMapper imapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _imapper = imapper;
        }

        [HttpPost ("[Action]")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (!result.Succeeded) return StatusCode((int) HttpStatusCode.Unauthorized, "Bad Credentials");
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.UserName);
                var token = JwtHelperService.GenerateJwtToken(model.UserName, appUser, _configuration);
                var rootData = new LoginResponseModel(token);
                return Ok(rootData);
            }

            var errorMessage =
                string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return BadRequest(errorMessage ?? "Bad Request");
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            object response = null;

            var user = await _userManager.GetUserAsync(User);
            if (user.GetType() == typeof(StudentModel))
            {
                response = _imapper.Map<PrivateStudentDataModel>(user);
            }

            if (user.GetType() == typeof(CompanyModel))
            {
                response = _imapper.Map<PrivateCompanyDataModel>(user);
            }

            if (response == null)
            {
                return NotFound("User not found");
            }

            return Ok(response);
        }
        
        [HttpPost ("[Action]")]
        [AllowAnonymous]
        public async Task<IActionResult> CampusNetLogin([FromBody] CnUserAuthModel cNUserAuthModelBody)
        {
            int responseStatusCode;

            responseStatusCode = await HttpService.UserCampusNetAuthHttpRequestAsync(cNUserAuthModelBody);
            Console.WriteLine(responseStatusCode);


            if (responseStatusCode == 401)
            {
                return BadRequest(ModelState);
            }

            return Ok(responseStatusCode);
        }
        
        
        
        
    }
}