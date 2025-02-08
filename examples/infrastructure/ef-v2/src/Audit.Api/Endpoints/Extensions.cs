using Audit.Api.Endpoints.AuditRecords;
using Audit.Api.Endpoints.Forecourts;
using Audit.Api.Endpoints.Lanes;
using Audit.Api.Endpoints.Pumps;

namespace Audit.Api.Endpoints;

public static class Extensions
{
    public static WebApplication RegisterEndpoints(this WebApplication webApplication)
    {
        webApplication.UsePathBase("/api");

        webApplication
            .RegisterCreateForecourtEndpoint()
            .RegisterCreateLaneEndpoint()
            .RegisterGetPumpEndpoint()
            .RegisterCreatePumpEndpoint()
            .RegisterGetAuditRecordsEndpoint();

        return webApplication;
    }
}