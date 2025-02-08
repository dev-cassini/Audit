using System.Net;
using System.Net.Http.Json;
using Audit.Api.Endpoints.Forecourts;
using Audit.Api.Endpoints.Lanes;

namespace Audit.ComponentTests.Endpoints.Pumps;

[TestFixture]
public class CreatePumpTests : ApiTestBase
{
    [Test]
    public async Task WhenHappyPath_ThenResponseIs200Ok()
    {
        var createForecourtRequest = new CreateForecourt.Request("Test Forecourt");
        var createForecourtResponse = await _httpClient.PostAsJsonAsync($"/api/forecourts", createForecourtRequest);
        
        Assert.That(createForecourtResponse.IsSuccessStatusCode, Is.True);
        
        var forecourtId = (await createForecourtResponse.Content.ReadFromJsonAsync<CreateForecourt.Response>())?.Id;
        var createLaneResponse = await _httpClient.PostAsync($"/api/forecourts/{forecourtId}/lanes", null);
        
        Assert.That(createLaneResponse.IsSuccessStatusCode, Is.True);
        
        var laneId = (await createLaneResponse.Content.ReadFromJsonAsync<CreateLane.Response>())?.Id;
        var createPumpResponse = await _httpClient.PostAsync($"/api/forecourts/{forecourtId}/lanes/{laneId}/pumps", null);
        
        Assert.That(createPumpResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}