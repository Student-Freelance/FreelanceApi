using System.Threading.Tasks;
using Freelance_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Freelance_Api.Controllers.APIs
{
    [Route("api/cvr/search/vat")]
    [ApiController]
    public class CvrController : ControllerBase
    {
        [HttpGet("/{option}/{param}")]
        [AllowAnonymous]
        public async Task<IActionResult> PostAsync(string option, string param)
        {
            int responseStatusCode;
            var responseFromHttpRequest = await HttpService.CvrVatHttpRequestAsync(option,param);
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