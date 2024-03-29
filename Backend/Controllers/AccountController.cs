﻿using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Freelance_Api.Models;
using Freelance_Api.Models.Identity;
using Freelance_Api.Models.Requests;
using Freelance_Api.Models.Responses;
using Freelance_Api.Services;
using Google.Apis.Auth;
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

        [HttpPost("[Action]")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (!result.Succeeded) return StatusCode((int) HttpStatusCode.Unauthorized, "Bad Credentials");
                var appUser = _userManager.Users.FirstOrDefault(r => r.UserName == model.UserName);
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

        [HttpPost("[Action]")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePwModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Bad Credentials");
            var user = await _userManager.GetUserAsync(User);
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded) return Ok("User Updated");


            return BadRequest(result.Errors);
        }

        [Obsolete]
        [HttpPost("[Action]")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleAuth([FromBody] TokenModel model)
        {
            if (!ModelState.IsValid) return Unauthorized("Google token is invalid");
            var validPayload = await GoogleJsonWebSignature.ValidateAsync(model.access_token);
            if (validPayload == null) return Unauthorized("Google token is invalid");
            var appUser = _userManager.Users.FirstOrDefault(r => r.Email == validPayload.Email);

            if (appUser != null)
            {
                await _signInManager.SignInAsync(appUser, false);
                var token = JwtHelperService.GenerateJwtToken(validPayload.Name, appUser, _configuration);
                var rootData = new LoginResponseModel(token);
                return Ok(rootData);
            }

            {
                var user = new StudentModel
                {
                    Firstname = validPayload.GivenName, Lastname = validPayload.FamilyName, Role = Role.Student,
                    UserName = validPayload.GivenName,
                    LocationModel = new LocationModel {Street = "", City = "", Number = "", Zip = ""},
                    Email = validPayload.Email, Logo = validPayload.Picture,
                    CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now
                };
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest(string.Join(",",
                        result.Errors?.Select(error => error.Description) ?? throw new InvalidOperationException()));
                }

                await _signInManager.SignInAsync(user, false);
                var token = JwtHelperService.GenerateJwtToken(validPayload.Name, user, _configuration);
                var rootData = new LoginResponseModel(token);
                return Ok(rootData);
            }
        }

        [Obsolete]
        [HttpGet("[Action]")]
        [AllowAnonymous]
        public ActionResult CampusNetLogin()
        {
            return Redirect(
                "https://auth.dtu.dk/dtu/?service=https://devops01.eitlab.diplom.dtu.dk/api/Account/Callback");
        }

        [Obsolete]
        [HttpGet("[Action]")]
        [AllowAnonymous]
        public async Task<ActionResult> Callback(string ticket)
        {
            try
            {
                var response = await HttpService.UserCampusNetAuthHttpRequestAsync(ticket);
                var valdation = response.Split("\n");
                if (valdation[0].Equals("no"))
                {
                    return Unauthorized("Login failed, reply was:" + valdation[0]);
                }

                var username = valdation[1];
                var email = $"{username}@student.dtu.dk";
                var appUser = _userManager.Users.FirstOrDefault(r => r.Email == email);

                if (appUser != null)
                {
                    await _signInManager.SignInAsync(appUser, false);
                    var token = JwtHelperService.GenerateJwtToken(username, appUser, _configuration);
                    return Redirect($"https://freelance-portal.herokuapp.com/?token={token}");
                }

                {
                    var user = new StudentModel
                    {
                        UserName = username,
                        Firstname = username,
                        Role = Role.Student,
                        Lastname = "",
                        Email = email,
                        LocationModel = new LocationModel {Street = "", City = "", Number = "", Zip = ""},
                        CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now
                    };
                    var result = await _userManager.CreateAsync(user);
                    if (!result.Succeeded)
                    {
                        return BadRequest(string.Join(",",
                            result.Errors?.Select(error => error.Description) ??
                            throw new InvalidOperationException()));
                    }

                    await _signInManager.SignInAsync(user, false);
                    var token = JwtHelperService.GenerateJwtToken(username, user, _configuration);
                    return Redirect($"https://freelance-portal.herokuapp.com/?token={token}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }
    }
}