using Audit.Infrastructure.Persistence.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Audit.Api.Endpoints.Lanes;

public static class CreateLane
{
    public record Response(Guid Id);
    
    public static WebApplication RegisterCreateLaneEndpoint(this WebApplication webApplication)
    {
        webApplication
            .MapPost("forecourts/{forecourtId:guid}/lanes", Handler)
            .AllowAnonymous()
            .WithTags(nameof(Lanes))
            .Produces(StatusCodes.Status200OK, typeof(Response))
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .ProducesValidationProblem();
            
        return webApplication;
    }

    private static async Task<IResult> Handler(
        [FromRoute] Guid forecourtId,
        AuditDbContext auditDbContext,
        CancellationToken cancellationToken)
    {
        var forecourt = await auditDbContext.Forecourts
            .Include(x => x.Lanes)
            .SingleOrDefaultAsync(x => x.Id == forecourtId, cancellationToken);
        
        if (forecourt is null)
        {
            var errors = new Dictionary<string, string[]> { { "ForecourtId", [$"Forecourt {forecourtId} not found."] } };
            var problemDetails = new HttpValidationProblemDetails(errors);
            return Results.BadRequest(problemDetails);
        }

        var lane = forecourt.AddLane();
        await auditDbContext.SaveChangesAsync(cancellationToken);
        
        return Results.Ok(new Response(lane.Id));
    }
}