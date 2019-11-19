using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
using MongoDB.Bson.IO;

namespace Freelance_Api.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly CompanyService _companyService;
        private readonly SignInManager<AppUserModel> _signInManager;
        private readonly UserManager<AppUserModel> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _imapper;

        public CompaniesController(
            CompanyService companyService, SignInManager<AppUserModel> signInManager,
            UserManager<AppUserModel> userManager, IConfiguration configuration, IMapper imapper)
        {
            _companyService = companyService;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _imapper = imapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public Task<List<PublicCompanyDataModel>> Get() =>
            _companyService.GetPublicCompanies();


        [HttpGet("{companyname}")]
        [AllowAnonymous]
        public async Task<ActionResult<PublicCompanyDataModel>> Get(string companyname)
        {
            var user = await _companyService.GetCompanyById(companyname);

            return user ?? (ActionResult<PublicCompanyDataModel>) NotFound("Company not found");
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] RegisterCompanyModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _imapper.Map<CompanyModel>(model);
                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(string.Join(",",
                        result.Errors?.Select(error => error.Description) ?? throw new InvalidOperationException()));
                }

                await _signInManager.SignInAsync(user, false);
                var token = JwtHelperService.GenerateJwtToken(model.Email, user, _configuration);
                var rootData = new LoginResponseModel(token);
                return Created("Created", rootData);
            }

            var errorMessage =
                string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return BadRequest(errorMessage);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] PrivateCompanyDataModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage =
                    string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return BadRequest(errorMessage);
            }
            
            if (!(await _userManager.GetUserAsync(User) is CompanyModel user))
            {
                return NotFound("User not found");
            }


            _imapper.Map(model, user);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(string.Join(",",
                    result.Errors?.Select(error => error.Description) ?? throw new InvalidOperationException()));
            }

            return Ok("User Updated");
        }
    }
}