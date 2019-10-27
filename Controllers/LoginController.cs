using System;
using System.Threading.Tasks;
using Freelance_Api.Models.APIs.Login.CampusNet;
using Freelance_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freelance_Api.Controllers.APIs.Login
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController: ControllerBase
    {
       
        [HttpPost("/CampusNet")]
        public async Task<IActionResult> PostAsync([FromBody] CnUserAuth cNUserAuthBody)
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