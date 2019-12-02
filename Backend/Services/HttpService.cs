using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Freelance_Api.Extensions;

namespace Freelance_Api.Services

{
    public class HttpService
    {
        private static readonly HttpClient Client = new HttpClient();
        private static readonly string baseApiUrl = "https://cvrapi.dk/api";

        public static async Task<string> UserCampusNetAuthHttpRequestAsync(string token)
        {
            var url =
                $"https://auth.dtu.dk/dtu/validate?service=https://devops01.eitlab.diplom.dtu.dk/api/Account/Callback&ticket={token}";

            var response = await Client.GetAsync(url);
            var respContent = await response.Content.ReadAsStringAsync();
            return respContent;
        }
#nullable enable
        public static async Task<HttpResponseMessage> GithubReposHttpRequestAsync(string userNameFromQuery,
            string? repo)
        {
            Client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
                "Freelance-Portal");

            var baseApiUrlWithParameter = repo != null
                ? $"https://api.github.com/users/{userNameFromQuery}/{repo}"
                : $"https://api.github.com/users/{userNameFromQuery}";


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