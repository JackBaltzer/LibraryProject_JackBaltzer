using LibraryProject.API.DTOs.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryProject.Tests
{
    // https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-5.0
    public class StartupTests : IClassFixture<WebApplicationFactory<LibraryProject.API.Startup>>
    {

        private readonly WebApplicationFactory<LibraryProject.API.Startup> _factory;

        public StartupTests(WebApplicationFactory<LibraryProject.API.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
        {
            // Arrange
            var client = _factory.CreateClient();


            HttpContent content = new StringContent("{FirstName:'derp'}", Encoding.UTF8, "application/json");


            // Act
            var response = await client.GetAsync("https://localhost:5001/api/Author");
            //var response = await client.PostAsync("/api/Create", content);

            // Assert
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task POST_EndpointsReturnSuccessAndCorrectContentType()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newAuthor = new NewAuthor
            {
                FirstName = "George",
                LastName = "derp",
                MiddleName = "R.R."
            };
            string json = JsonConvert.SerializeObject(newAuthor);
            //string json2 = "{\"FirstName\":\"George\",\"MiddleName\":\"R.R.\",\"LastName\":\"derp\"}";
            string json2 = "{\"FirstName\":\"George\",\"MiddleName\":\"R.R.\"}";
            HttpContent content = new StringContent(json2, Encoding.UTF8, "application/json");


            // Act
            var response = await client.PostAsync("https://localhost:5001/api/Author", content);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode); // Status Code 200-299

            //response.EnsureSuccessStatusCode(); // Status Code 200-299
            //Assert.Equal("application/json; charset=utf-8",
            //    response.Content.Headers.ContentType.ToString());
        }
    }
}
