using Audit.Domain.Tooling;
using Audit.Infrastructure.Persistence.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Audit.Api.Endpoints.Pumps;

public static class GetPump
{
    public record LaneDto(Guid Id, int Number);
    public record VehicleDto(Guid Id);
    public record Response(
        Guid Id,
        int Number,
        LaneDto Lane,
        VehicleDto? Vehicle);
    
    public static WebApplication RegisterGetPumpEndpoint(this WebApplication webApplication)
    {
        webApplication
            .MapGet("pumps/{pumpId:guid}", Handler)
            .AllowAnonymous()
            .WithTags(nameof(Pumps))
            .Produces(StatusCodes.Status200OK, typeof(Response))
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound, typeof(string));
            
        return webApplication;
    }

    private static async Task<IResult> Handler(
        [FromRoute] Guid pumpId,
        AuditDbContext auditDbContext,
        CancellationToken cancellationToken)
    {
        var pump = await auditDbContext.Pumps
            .AsNoTracking()
            .Where(x => x.Id == pumpId)
            .Select(x => new Response(
                x.Id,
                x.Number,
                new LaneDto(x.Lane.Id, x.Lane.Number),
                x.VehicleId == null ? null : new VehicleDto(x.VehicleId.Value)))
            .SingleOrDefaultAsync(cancellationToken);
        
        return pump is null ? Results.NotFound($"Pump {pumpId} not found") : Results.Ok(pump);
    }
}