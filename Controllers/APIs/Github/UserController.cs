using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

namespace Freelance_Api.Controllers.APIs
{
    [Route("api/github/user/")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        [HttpPost("{userNameFromEndPoint}")]
        [AllowAnonymous]
        public async Task<IActionResult> PostAsync(string userNameFromEndPoint)
        {
            int responseStatusCode;
            var responseFromHttpRequest = await GithubUserHTTPRequestAsync(userNameFromEndPoint);
            var responseContentFromHttpRequest = await responseFromHttpRequest.Content.ReadAsStringAsync();
     
            responseStatusCode = (int) responseFromHttpRequest.StatusCode;
            
            if (responseStatusCode == 401)
            {
                return BadRequest(ModelState);
            }

            var responseContentFromHttpRequestToJSON = JsonConvert.DeserializeObject(responseContentFromHttpRequest);

            return Ok(responseContentFromHttpRequestToJSON);
        }
        
        protected async Task<HttpResponseMessage> GithubUserHTTPRequestAsync(string userNameFromQuery)
        {
            string baseApiURL = "https://api.github.com/users/";

            HttpClient client = new HttpClient();
            
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
                            "Freelance-Portal");
            
            string baseApiURLWithParameter = String.Format("https://api.github.com/users/{0}", userNameFromQuery);
   
            var response = await client.GetAsync(baseApiURLWithParameter);
            
            return response;
        }
    }
}