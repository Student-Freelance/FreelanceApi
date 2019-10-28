using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Freelance_Api.Extensions;
using Freelance_Api.Models.CampusNet;

namespace Freelance_Api.Services

{
    public class HttpService
    {
        private static readonly HttpClient Client = new HttpClient();
        private static readonly string baseApiUrl = "https://cvrapi.dk/api";

        public static async Task<int> UserCampusNetAuthHttpRequestAsync(CnUserAuth cnUserAuth)
        {
            string url = @"https://auth.dtu.dk/dtu/mobilapp.jsp";


            HttpContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", cnUserAuth.AuthUsername),
                new KeyValuePair<string, string>("password", cnUserAuth.AuthPassword),
            });

            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            content.Headers.Add("X-appname", "DTU Inside Companion");
            content.Headers.Add("X-token", "ae034f83-4bf4-48a9-86c5-a47f03ad6054");

            var response = await Client.PostAsync(url, content);

            var respContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(respContent);

            var responseStatusCode = (int) response.StatusCode;

            return responseStatusCode;
        }

        public static async Task<HttpResponseMessage> GithubReposHttpRequestAsync(string userNameFromQuery, string repo)
        {
            string baseApiUrlWithParameter;
            Client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
                "Freelance-Portal");

            if (repo != null)
            {
                baseApiUrlWithParameter = $"https://api.github.com/users/{userNameFromQuery}/{repo}";
            }
            else
            {
                baseApiUrlWithParameter = $"https://api.github.com/users/{userNameFromQuery}";
            }


            var response = await Client.GetAsync(baseApiUrlWithParameter);

            return response;
        }
        
        public static async Task<HttpResponseMessage> CvrVatHttpRequestAsync(string searchOption, string searchQuery)
        {
            Client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
                "[DTU@Gruppe0] [Freelance-portal] - [Ali M] [aamoussa97@gmail.com]");

            var parameters = new Dictionary<string, string>();
            parameters.Add(searchOption, searchQuery);
            parameters.Add("country", "DK");

            string baseApiUrlWithParameters = baseApiUrl.AttachParameters(parameters);

            var response = await Client.GetAsync(baseApiUrlWithParameters);

            return response;
        }
    }
}