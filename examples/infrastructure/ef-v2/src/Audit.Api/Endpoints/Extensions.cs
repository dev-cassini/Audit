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
            .RegisterCreatePumpEndpoint();

        return webApplication;
    }
}