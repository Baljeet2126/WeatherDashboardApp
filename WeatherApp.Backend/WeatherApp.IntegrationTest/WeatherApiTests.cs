 using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;

namespace WeatherApp.WebApi.IntegrationTests;

[TestFixture]
public class WeatherApiTests
{
    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost") 
        });
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Test]
    public async Task GetWeather_ReturnsFiveCities()
    {
        var req = new HttpRequestMessage(HttpMethod.Get, "/api/v1/WeatherDashboard/weather");
        req.Headers.Add("X-User-Id", "test-user");

        var resp = await _client.SendAsync(req);
        resp.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await resp.Content.ReadFromJsonAsync<List<WeatherDto>>();
        body.Should().NotBeNull();
        body!.Count.Should().Be(5);
    }

    [Test]
    public async Task GetTrends_ReturnsTrendsForAllCities()
    {
        var req = new HttpRequestMessage(HttpMethod.Get, "/api/v1/WeatherDashboard/weather/trend");
        var resp = await _client.SendAsync(req);
        resp.EnsureSuccessStatusCode();

        var trends = await resp.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        trends.Should().NotBeNull();
        trends!.Keys.Should().Contain(new[] { "London", "Vienna", "Ljubljana", "Belgrade", "Valletta" });
    }

    [Test]
    public async Task PostUnit_SavesPreference()
    {
        var req = new HttpRequestMessage(HttpMethod.Post, "/api/v1/WeatherDashboard/weather/userpreference")
        {
            Content = JsonContent.Create(new { userId = "test-user", temperatureUnit = "imperial", ShowSunrise = true })
        };
       

        var resp = await _client.SendAsync(req);
        resp.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NoContent);
    }

    private sealed record WeatherDto(
        int CityId,
        string CityName,
        string Description,
        string Icon,
        double Temperature,
        DateTime SunriseTime,
        DateTime SunsetTime,
        string TemperatureUnit,
        string Trend
    );
}
