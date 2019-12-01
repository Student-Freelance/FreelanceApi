using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using AutoMapper;
using Freelance_Api.Models;
using Freelance_Api.Models.Identity;
using Freelance_Api.Models.Responses;
using Freelance_Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Freelance_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class JobsController: ControllerBase
    {
        private readonly JobService _jobService;
        private readonly UserManager<AppUserModel> _userManager;
        private readonly IMapper _imapper;

        public JobsController(JobService jobService, UserManager<AppUserModel> userManager, IMapper imapper)
        {
            _jobService = jobService;
            _userManager = userManager;
            _imapper = imapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<JobModel>> Get() =>
            _jobService.Get();

        [HttpGet("{id:length(24)}")]
        [AllowAnonymous]
        public ActionResult<JobModel> Get(string id)
        {
            var job = _jobService.Get(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }
        [HttpPost]
        public async Task<ActionResult> Post(JobModel jobModel)
        {
            string id= _jobService.Create(jobModel);
            if (!(await _userManager.GetUserAsync(User) is CompanyModel user))
            {
                return NotFound("User not found");
            }
            user.Jobs.Add(id);
            try
            {
                await _userManager.UpdateAsync(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
           
            return Ok();
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, JobModel jobin)
        {
            var job = _jobService.Get(id);

            if (job == null)
            {
                return NotFound();
            }

            _jobService.Update(id, jobin);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var job = _jobService.Get(id);

            if (job == null)
            {
                return NotFound();
            }
            _jobService.Remove(job.Id);
            return NoContent();
        }
    }
}