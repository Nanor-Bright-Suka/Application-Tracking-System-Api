using System.Net;
using ATS.Tests.Infrastructure;
using ATS.Dtos.Jobs;
using ATS.Dtos.Applications;
using ATS.Enums;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace ATS.Tests.Applications;



public class ApplicationDuplicateTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApplicationDuplicateTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }




        [Fact]
    public async Task Should_Reject_Duplicate_Applications_By_Email()
    {

        var options = new JsonSerializerOptions  {
             PropertyNameCaseInsensitive = true,
            Converters =  {
            new JsonStringEnumConverter() }
        };

        var jobRequest = new HttpRequestMessage(HttpMethod.Post, "/api/jobs");

        jobRequest.Headers.Add(
            "X-Team-Member-Id",
            "11111111-1111-1111-1111-111111111111");

        jobRequest.Content = JsonContent.Create(new
        {
            Title = "Backend Dev",
            Description = "Test Job",
            Location = "Remote",
            Status = JobStatusEnum.Open
        }, options: options);

        var jobResponse = await _client.SendAsync(jobRequest);
        jobResponse.EnsureSuccessStatusCode();

        var json = await jobResponse.Content.ReadAsStringAsync();
        var job = JsonSerializer.Deserialize<JobResponseDto>(json, options);
   
        Assert.NotNull(job);
        
    var firstApp = await _client.PostAsJsonAsync(
    $"/api/jobs/{job!.Id}/applications",
    new
    {
        Name = "Aaron Smith",
        Email = "miketyson@test.com",
        CoverLetter = "First application"
    }, options: options);

  firstApp.EnsureSuccessStatusCode();


    var secondApp = await _client.PostAsJsonAsync(
    $"/api/jobs/{job!.Id}/applications",
    new
    {
        Name = "Jery doku",
        Email = "miketyson@test.com",
        CoverLetter = "Duplicate application"
    }, options: options);


    Assert.Equal(HttpStatusCode.BadRequest, secondApp.StatusCode);

    Assert.Equal("application/problem+json",
    secondApp.Content.Headers.ContentType?.MediaType);






    }
}