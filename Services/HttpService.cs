using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Freelance_Api.Models.APIs.Login.CampusNet;

namespace Freelance_Api.Services
{
    public class HttpService
    {
        public static async Task<int> UserCampusNetAuthHTTPRequestAsync(CNUserAuth cnUserAuth)
        {
            string url = @"https://auth.dtu.dk/dtu/mobilapp.jsp";

            HttpClient client = new HttpClient();
            HttpContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", cnUserAuth.AuthUsername),
                new KeyValuePair<string, string>("password", cnUserAuth.AuthPassword),
            });

            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            content.Headers.Add("X-appname", "DTU Inside Companion");
            content.Headers.Add("X-token", "ae034f83-4bf4-48a9-86c5-a47f03ad6054");

            var response = await client.PostAsync(url, content);

            var respContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(respContent);

            var ResponseStatusCode = (int) response.StatusCode;

            return ResponseStatusCode;
        }
        
        public static async Task<HttpResponseMessage> GithubReposHTTPRequestAsync(string userNameFromQuery)
        {
            string baseApiURL = "https://api.github.com/users/";

            HttpClient client = new HttpClient();
            
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
                "Freelance-Portal");
            
            string baseApiURLWithParameter = String.Format("https://api.github.com/users/{0}/repos", userNameFromQuery);
   
            var response = await client.GetAsync(baseApiURLWithParameter);
            
            return response;
        }
        
    }
    
    
    
    
    }
