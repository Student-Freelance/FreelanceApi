using System.Collections.Generic;
using Freelance_Api.Models;
using Freelance_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Freelance_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController: ControllerBase
    {
        private readonly EmployerService _employerService;

        public EmployerController(EmployerService employerService)
        {
            _employerService = employerService;
        }

        [HttpGet]
        public ActionResult<List<Employe>> get() =>
            _employerService.Get();

        [HttpGet("{id:length(24)}")]
        public ActionResult<Employe> Get(string id)
        {
            var employe = _employerService.Get(id);

            if (employe == null)
            {
                NotFound();
            }

            return employe;
        }
        
        [HttpPost]
        public ActionResult<Employe> Create(Employe employe)
        {
            _employerService.Create(employe);

            return employe;
        }
        
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Employe employein)
        {
            var emplpye = _employerService.Get(id);

            if (emplpye == null)
            {
                return NotFound();
            }

            _employerService.Update(id, employein);

            return NoContent();
        }
        
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var employe = _employerService.Get(id);

            if (employe == null)
            {
                return NotFound();
            }

            _employerService.Remove(employe.Id);

            return NoContent();
        }
    }
}