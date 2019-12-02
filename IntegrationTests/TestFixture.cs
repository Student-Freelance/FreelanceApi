using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Freelance_Api;
using Freelance_Api.Models.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class TestFixture
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly ITestOutputHelper _testOutputHelper;

        public TestFixture(WebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData("https://localhost:5001/api/Students")]
        [InlineData("https://localhost:5001/api/Companies")]
        [InlineData("https://localhost:5001/api/Jobs")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            _testOutputHelper.WriteLine(response.ToString());
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("https://localhost:5001/api/Account/Login")]
        public async Task Post_Login(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var login = new LoginModel
            {
                UserName = Environment.GetEnvironmentVariable("Testuser"),
                Password = Environment.GetEnvironmentVariable("Testpassword")
            };
            var stringContent =
                new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, stringContent);
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("https://localhost:5001/api/Account/Login")]
        public async Task Post_Login_Failure(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var login = new LoginModel {UserName = "", Password = ""};
            var stringContent =
                new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, stringContent);
            // Assert
            Assert.Equal(400, (int) response.StatusCode); //badrequest
            Assert.Equal("text/plain; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}