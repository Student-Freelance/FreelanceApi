using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Freelance_Api.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace Freelance_Api.Controllers.APIs
{
    [Route("api/cvr/search/vat")]
    [ApiController]
    public class VatController : ControllerBase
    {
        [HttpPost("{vatFromEndPoint}")]
        [AllowAnonymous]
        public async Task<IActionResult> PostAsync(string vatFromEndPoint)
        {
            int responseStatusCode;
            var responseFromHttpRequest = await CVRVatHTTPRequestAsync(vatFromEndPoint);
            var responseContentFromHttpRequest = await responseFromHttpRequest.Content.ReadAsStringAsync();
            
            responseStatusCode = (int) responseFromHttpRequest.StatusCode;
            
            if (responseStatusCode == 401)
            {
                return BadRequest(ModelState);
            }

            var responseContentFromHttpRequestToJSON = JsonConvert.DeserializeObject(responseContentFromHttpRequest);

            return Ok(responseContentFromHttpRequestToJSON);
        }
        
        protected async Task<HttpResponseMessage> CVRVatHTTPRequestAsync(string vatFromQuery)
        {
            string baseApiURL = "https://cvrapi.dk/api";

            HttpClient client = new HttpClient();
            
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
                "[DTU@Gruppe0] [Freelance-portal] - [Ali M] [aamoussa97@gmail.com]");
            
            var parameters = new Dictionary<string, string>();
            parameters.Add("vat", vatFromQuery);
            parameters.Add("country", "DK");

            string baseApiURLWithParameters = baseApiURL.AttachParameters(parameters);
   
            var response = await client.GetAsync(baseApiURLWithParameters);
            
            return response;
        }
    }
}