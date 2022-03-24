using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TimeLogger.IntegrationTests
{
    public class TimeLogsTests : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient _client;

        public TimeLogsTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Create_New_TimeLog()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/projects/1/timelogs")
            {
                Content = new StringContent(JsonSerializer.Serialize(new
                {
                    description = "Initial Interviews",
                    duration = 50
                }), Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
