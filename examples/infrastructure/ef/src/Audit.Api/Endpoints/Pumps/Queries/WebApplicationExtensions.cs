namespace Audit.Api.Endpoints.Pumps.Queries;

public static class WebApplicationExtensions
{
    public static WebApplication RegisterPumpQueryEndpoints(this WebApplication webApplication)
    {
        webApplication
            .RegisterGetPumpAuditRecordsEndpoint();

        return webApplication;
    }
}