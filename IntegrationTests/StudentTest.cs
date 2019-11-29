using System;
using System.Net.Http;
using System.Threading.Tasks;
using Freelance_Api;
using Tests.Tests;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class StudentTest : IClassFixture<TestFixture<Startup>>
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private HttpClient Client;

        public StudentTest(TestFixture<Startup> fixture, ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            Client = fixture.Client;
        }
        
        
        [Fact]
        public async Task TestGetStockItemsAsync()
        {
            // Arrange
            var request = "/api/Students";

            // Act
            var response = await Client.GetAsync(request);
            _testOutputHelper.WriteLine(response.ToString());

            // Assert
            response.EnsureSuccessStatusCode();
            
        }

        
    }
    
    
}