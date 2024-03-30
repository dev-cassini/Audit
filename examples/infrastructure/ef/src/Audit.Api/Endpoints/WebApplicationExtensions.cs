using Audit.Api.Endpoints.Pumps;

namespace Audit.Api.Endpoints;

public static class WebApplicationExtensions
{
    public static WebApplication RegisterEndpoints(this WebApplication webApplication)
    {
        webApplication
            .RegisterPumpEndpoints();

        return webApplication;
    }
}