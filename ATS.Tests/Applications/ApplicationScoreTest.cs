
using ATS.Tests.Infrastructure;
using ATS.Dtos.Jobs;
using ATS.Dtos.Applications;
using ATS.Enums;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace ATS.Tests.Applications;



public class ApplicationScoreTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApplicationScoreTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }



    [Fact]
    public async Task Score_Should_Overwrite_When_Submitted_Twice()
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

        var appResponse = await _client.PostAsJsonAsync(
            $"/api/jobs/{job.Id}/applications",
            new
            {
                Name = "Test User",
                Email = "test@email.com",
                CoverLetter = "Hello"
            }, options: options);
        appResponse.EnsureSuccessStatusCode();

        var application = await appResponse.Content
            .ReadFromJsonAsync<ApplicationResponseDto>(options: options);

        Assert.NotNull(application);

        var request1 = new HttpRequestMessage(
          HttpMethod.Put,
          $"/api/applications/{application!.Id}/scores/culture-fit");

        request1.Headers.Add("X-Team-Member-Id", "11111111-1111-1111-1111-111111111111");
        request1.Content = JsonContent.Create(new
        {
            score = 3,
            comment = "first submission"
        }, options: options);

        var firstScoreResponse = await _client.SendAsync(request1);
       firstScoreResponse.EnsureSuccessStatusCode();

        var request2 = new HttpRequestMessage(
            HttpMethod.Put,
            $"/api/applications/{application.Id}/scores/culture-fit");

        request2.Headers.Add("X-Team-Member-Id", "11111111-1111-1111-1111-111111111111");
        request2.Content = JsonContent.Create(new
        {
            score = 4,
            comment = "updated submission"
        }, options: options);

        var secondScoreResponse = await _client.SendAsync(request2);
        secondScoreResponse.EnsureSuccessStatusCode();

        var profile = await _client.GetFromJsonAsync<ApplicationProfileResponseDto>(
            $"/api/applications/{application!.Id}", options: options);

        Assert.NotNull(profile);


        var cultureFitScores = profile!.Scores
            .Where(s => s.Type == ApplicationScoreTypeEnum.CultureFit)
            .ToList();

        Assert.Single(cultureFitScores);

        var score = cultureFitScores[0];

        Assert.Equal(4, score.Score);
        Assert.Equal("updated submission", score.Comment);



    }















}
   