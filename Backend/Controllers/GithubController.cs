using System;
using System.Net;
using System.Threading.Tasks;
using Freelance_Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Freelance_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GitHubController : ControllerBase
    {   
        [HttpGet("{userNameFromEndPoint}")]
        #nullable enable
        public async Task<IActionResult> Get(string userNameFromEndPoint, string? repos=null)
        {
           
            
                var responseFromHttpRequest = await HttpService.GithubReposHttpRequestAsync(userNameFromEndPoint,repos); 
                var responseContentFromHttpRequest = await responseFromHttpRequest.Content.ReadAsStringAsync();
                var responseStatusCode =  responseFromHttpRequest.StatusCode;
                if (responseStatusCode == HttpStatusCode.Unauthorized)
                {
                    return BadRequest(ModelState);
                }

                var responseContentFromHttpRequestToJson = JsonConvert.DeserializeObject(responseContentFromHttpRequest);
                return Ok(responseContentFromHttpRequestToJson);
            
            
        }
        
    }
}