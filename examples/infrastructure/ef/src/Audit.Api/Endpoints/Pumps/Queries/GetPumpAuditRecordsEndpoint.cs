using Audit.Application.Pumps.Queries.GetPumpAuditRecords.Dtos;
using MediatR;

namespace Audit.Api.Endpoints.Pumps.Queries;

using Queries = Audit.Application.Pumps.Queries;

public static class GetPumpAuditRecordsEndpoint
{
    public static WebApplication RegisterGetPumpAuditRecordsEndpoint(this WebApplication webApplication)
    {
        webApplication.MapPost("/forecourts/{forecourtId}/lanes/{laneId}/pumps/{pumpId}/audit-records", Query)
            .AllowAnonymous()
            .WithTags(nameof(Pumps))
            .Produces(StatusCodes.Status200OK, typeof(IEnumerable<PumpAuditRecordDto>))
            .Produces(StatusCodes.Status404NotFound);

        return webApplication;
    }
    
    private static async Task<IResult> Query(
        IMediator mediator,
        Guid forecourtId,
        Guid laneId,
        Guid pumpId,
        CancellationToken cancellationToken)
    {
        var query = new Queries.GetPumpAuditRecords.Query(forecourtId, laneId, pumpId);
        var response = (await mediator.Send(query, cancellationToken)).ToList();

        if (response.Any() is false)
        {
            return Results.NotFound();
        }
        
        return Results.Ok(response);
    }
}