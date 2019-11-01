using System.Threading.Tasks;
using Freelance_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Freelance_Api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class GitHubController : ControllerBase
    {
        [HttpGet("{userNameFromEndPoint}")]
        public async Task<IActionResult> Get(string userNameFromEndPoint, string? repos=null)
        {
            int responseStatusCode;
            var responseFromHttpRequest = await HttpService.GithubReposHttpRequestAsync(userNameFromEndPoint,repos);
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