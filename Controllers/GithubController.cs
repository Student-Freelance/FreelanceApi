using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Freelance_Api.Services;
using Newtonsoft.Json;

namespace Freelance_Api.Controllers.APIs
{
    [Route("api/github/")]
    [AllowAnonymous]
    [ApiController]
    public class GithubController : ControllerBase
    {
        [HttpGet("{userNameFromEndPoint}/repos")]
        public async Task<IActionResult> UserRepo(string userNameFromEndPoint)
        {
            int responseStatusCode;
            var responseFromHttpRequest = await HttpService.GithubReposHttpRequestAsync(userNameFromEndPoint);
            var responseContentFromHttpRequest = await responseFromHttpRequest.Content.ReadAsStringAsync();
     
            responseStatusCode = (int) responseFromHttpRequest.StatusCode;
            
            if (responseStatusCode == 401)
            {
                return BadRequest(ModelState);
            }

            var responseContentFromHttpRequestToJson = JsonConvert.DeserializeObject(responseContentFromHttpRequest);

            return Ok(responseContentFromHttpRequestToJson);
        }
        
        [HttpGet("{userNameFromEndPoint}")] 
        public async Task<IActionResult> Userinfo(string userNameFromEndPoint)
        {
            int responseStatusCode;
            var responseFromHttpRequest = await HttpService.GithubUserHttpRequestAsync(userNameFromEndPoint);
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