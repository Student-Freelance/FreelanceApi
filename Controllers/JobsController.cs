using System.Collections.Generic;
using Freelance_Api.Models;
using Freelance_Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freelance_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class JobsController: ControllerBase
    {
        private readonly JobService _jobService;

        public JobsController(JobService jobService)
        {
            _jobService = jobService;
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
        public ActionResult<JobModel> Create(JobModel jobModel)
        {
            _jobService.Create(jobModel);

            return jobModel;
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, JobModel jobin)
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