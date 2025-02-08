using System.Net;
using System.Net.Http.Json;
using Audit.Api.Endpoints.AuditRecords;
using Audit.Api.Endpoints.Forecourts;
using Audit.Api.Endpoints.Lanes;
using Audit.Api.Endpoints.Pumps;

namespace Audit.ComponentTests.Endpoints.Pumps;

[TestFixture]
public class CreatePumpTests : ApiTestBase
{
    [Test]
    public async Task WhenHappyPath_ThenResponseIs200Ok_AndPumpIsCreated()
    {
        var createForecourtRequest = new CreateForecourt.Request("Test Forecourt");
        var createForecourtResponse = await _httpClient.PostAsJsonAsync("/api/forecourts", createForecourtRequest);
        
        Assert.That(createForecourtResponse.IsSuccessStatusCode, Is.True);
        
        var forecourtId = (await createForecourtResponse.Content.ReadFromJsonAsync<CreateForecourt.Response>())?.Id;
        var createLaneResponse = await _httpClient.PostAsync($"/api/forecourts/{forecourtId}/lanes", null);
        
        Assert.That(createLaneResponse.IsSuccessStatusCode, Is.True);
        
        var laneId = (await createLaneResponse.Content.ReadFromJsonAsync<CreateLane.Response>())?.Id;
        var createPumpResponse = await _httpClient.PostAsync($"/api/forecourts/{forecourtId}/lanes/{laneId}/pumps", null);
        
        Assert.That(createPumpResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        var pumpId = (await createPumpResponse.Content.ReadFromJsonAsync<CreatePump.Response>())?.Id;
        var getPumpHttpResponseMessageResponse = await _httpClient.GetAsync($"/api/pumps/{pumpId}");
        
        Assert.That(getPumpHttpResponseMessageResponse.IsSuccessStatusCode, Is.True);
        
        var getPumpResponse = await getPumpHttpResponseMessageResponse.Content.ReadFromJsonAsync<GetPump.Response>();
        Assert.That(getPumpResponse, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(getPumpResponse.Id, Is.EqualTo(pumpId));
            Assert.That(getPumpResponse.Number, Is.EqualTo(1));
            Assert.That(getPumpResponse.Lane.Id, Is.EqualTo(laneId));
            Assert.That(getPumpResponse.Vehicle, Is.Null);
        });
    }

    [Test]
    public async Task WhenHappyPath_ThenPumpAuditRecordIsCreated()
    {
        var createForecourtRequest = new CreateForecourt.Request("Test Forecourt");
        var createForecourtResponse = await _httpClient.PostAsJsonAsync("/api/forecourts", createForecourtRequest);
        
        Assert.That(createForecourtResponse.IsSuccessStatusCode, Is.True);
        
        var forecourtId = (await createForecourtResponse.Content.ReadFromJsonAsync<CreateForecourt.Response>())?.Id;
        var createLaneResponse = await _httpClient.PostAsync($"/api/forecourts/{forecourtId}/lanes", null);
        
        Assert.That(createLaneResponse.IsSuccessStatusCode, Is.True);
        
        var laneId = (await createLaneResponse.Content.ReadFromJsonAsync<CreateLane.Response>())?.Id;
        var createPumpResponse = await _httpClient.PostAsync($"/api/forecourts/{forecourtId}/lanes/{laneId}/pumps", null);
        
        Assert.That(createPumpResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        var pumpId = (await createPumpResponse.Content.ReadFromJsonAsync<CreatePump.Response>())?.Id;
        var getAuditRecordsHttpResponseMessage = await _httpClient.GetAsync("/api/audit-records");
        
        Assert.That(getAuditRecordsHttpResponseMessage.IsSuccessStatusCode, Is.True);
        
        var getAuditRecordsResponse = await getAuditRecordsHttpResponseMessage.Content.ReadFromJsonAsync<GetAuditRecords.Response>();
        Assert.That(getAuditRecordsResponse, Is.Not.Null);
        Assert.That(getAuditRecordsResponse.AuditRecords, Has.Count.EqualTo(1));
        
        var auditRecord = getAuditRecordsResponse.AuditRecords.Single();
        Assert.That(auditRecord.ResourceId, Is.EqualTo(pumpId));
        Assert.That(auditRecord.TimestampUtc, Is.EqualTo(DateTimeOffset.UtcNow).Within(1).Seconds);
        Assert.That(auditRecord.Metadata, Has.Count.EqualTo(3));
        
        var numberAuditRecordMetadata = auditRecord.Metadata.SingleOrDefault(m => m.PropertyName == "Number");
        Assert.That(numberAuditRecordMetadata, Is.Not.Null);
        Assert.That(numberAuditRecordMetadata.OriginalValue, Is.Null);
        Assert.That(numberAuditRecordMetadata.UpdatedValue, Is.EqualTo("1"));
        
        var laneIdAuditRecordMetadata = auditRecord.Metadata.SingleOrDefault(m => m.PropertyName == "LaneId");
        Assert.That(laneIdAuditRecordMetadata, Is.Not.Null);
        Assert.That(laneIdAuditRecordMetadata.OriginalValue, Is.Null);
        Assert.That(laneIdAuditRecordMetadata.UpdatedValue, Is.EqualTo(laneId.ToString()));
        
        var vehicleIdAuditRecordMetadata = auditRecord.Metadata.SingleOrDefault(m => m.PropertyName == "VehicleId");
        Assert.That(vehicleIdAuditRecordMetadata, Is.Not.Null);
        Assert.That(vehicleIdAuditRecordMetadata.OriginalValue, Is.Null);
        Assert.That(vehicleIdAuditRecordMetadata.UpdatedValue, Is.Null);
    }
}