using Audit.Api.Endpoints.Pumps.Queries;

namespace Audit.Api.Endpoints.Pumps;

public static class WebApplicationExtensions
{
    public static WebApplication RegisterPumpEndpoints(this WebApplication webApplication)
    {
        webApplication
            .RegisterPumpQueryEndpoints();

        return webApplication;
    }
}