using System.Collections.Generic;
using AspNetCore.Identity.Mongo.Model;
using Freelance_Api.Models;
using Freelance_Api.Models.Identity;
using Freelance_Api.Models.Responses;
using Freelance_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Freelance_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PublicUserController : ControllerBase
    {
        private readonly UserService _userService;

        public PublicUserController(
            UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<StudentDataResponse>> GetPublicStudents() =>
            _userService.GetPublicStudents();

        [HttpGet]
        public ActionResult<List<CompanyDataReponse>> GetPublicCompanies() =>
            _userService.GetPublicCompanies();
        
        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Student> Get(string id)
        {
            var student = _userService.Get(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }
        

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Student studentin)
        {
            var student = _userService.Get(id);

            if (student == null)
            {
                return NotFound();
            }

            _userService.Update(id, studentin);

            return NoContent();
        }
        
    }
}