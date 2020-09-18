using System.Net.Http;
using System.Threading.Tasks;
using Drop.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using Xunit;

namespace Drop.Tests.Integration.Controllers
{
    public class HomeControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        [Fact]
        public async Task get_home_endpoint_should_return_message()
        {
            // Act
            var response = await _client.GetAsync("api");
            
            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            content.ShouldBe("Drop API [dev]");
        }
        
        private readonly HttpClient _client;

        public HomeControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
    }
}