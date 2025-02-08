using Audit.Domain.Tooling;
using Audit.Infrastructure.Persistence.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Audit.Api.Endpoints.Pumps;

public static class CreatePump
{
    public record Response(Guid Id);
    
    public static WebApplication RegisterCreatePumpEndpoint(this WebApplication webApplication)
    {
        webApplication
            .MapPost("forecourts/{forecourtId:guid}/lanes/{laneId:guid}/pumps", Handler)
            .AllowAnonymous()
            .WithTags(nameof(Pumps))
            .Produces(StatusCodes.Status200OK, typeof(Response))
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .ProducesValidationProblem();
            
        return webApplication;
    }

    private static async Task<IResult> Handler(
        [FromRoute] Guid forecourtId,
        [FromRoute] Guid laneId,
        AuditDbContext auditDbContext,
        IDateTimeProvider dateTimeProvider,
        CancellationToken cancellationToken)
    {
        var forecourt = await auditDbContext.Forecourts
            .Include(x => x.Lanes)
            .ThenInclude(x => x.Pumps)
            .SingleOrDefaultAsync(x => x.Id == forecourtId, cancellationToken);
        
        if (forecourt is null)
        {
            var errors = new Dictionary<string, string[]> { { "ForecourtId", [$"Forecourt {forecourtId} not found."] } };
            var problemDetails = new HttpValidationProblemDetails(errors);
            return Results.BadRequest(problemDetails);
        }
        
        var lane = forecourt.Lanes.SingleOrDefault(x => x.Id == laneId);
        if (lane is null)
        {
            var errors = new Dictionary<string, string[]> { { "LaneId", [$"Lane {laneId} not found."] } };
            var problemDetails = new HttpValidationProblemDetails(errors);
            return Results.BadRequest(problemDetails);
        }

        var pump = lane.AddPump(dateTimeProvider);
        await auditDbContext.SaveChangesAsync(cancellationToken);
        
        return Results.Ok(new Response(pump.Id));
    }
}