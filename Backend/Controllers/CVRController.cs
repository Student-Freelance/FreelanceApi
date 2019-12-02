using System.Threading.Tasks;
using Freelance_Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Freelance_Api.Controllers
{
    [ApiController]
    // ReSharper disable once InconsistentNaming
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //TODO Find a use for this.
    public class CVRController : ControllerBase
    {
        [HttpGet("/{option}/{param}")]
        public async Task<IActionResult> PostAsync(string option, string param)
        {
            int responseStatusCode;
            var responseFromHttpRequest = await HttpService.CvrVatHttpRequestAsync(option, param);
            var responseContentFromHttpRequest = await responseFromHttpRequest.Content.ReadAsStringAsync();

            responseStatusCode = (int) responseFromHttpRequest.StatusCode;

            if (responseStatusCode == 401)
            {
                return BadRequest(ModelState);
            }

            var responseContentFromHttpRequestToJson = JsonConvert.DeserializeObject(responseContentFromHttpRequest);

            return Ok(responseContentFromHttpRequestToJson);
        }
    }
}