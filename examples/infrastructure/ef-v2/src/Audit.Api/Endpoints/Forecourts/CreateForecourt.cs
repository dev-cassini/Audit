using Audit.Domain.Model;
using Audit.Infrastructure.Persistence.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Audit.Api.Endpoints.Forecourts;

public static class CreateForecourt
{
    public record Request(string Name);
    public record Response(Guid Id);
    
    public static WebApplication RegisterCreateForecourtEndpoint(this WebApplication webApplication)
    {
        webApplication
            .MapPost("forecourts", Handler)
            .AllowAnonymous()
            .WithTags(nameof(Forecourts))
            .Produces(StatusCodes.Status200OK, typeof(Response))
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden);
            
        return webApplication;
    }

    private static async Task<IResult> Handler(
        [FromBody] Request request,
        AuditDbContext auditDbContext,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var forecourt = new Forecourt(Guid.NewGuid(), request.Name);
        await auditDbContext.Forecourts.AddAsync(forecourt, cancellationToken);
        await auditDbContext.SaveChangesAsync(cancellationToken);
        
        return Results.Ok(new Response(forecourt.Id));
    }
}