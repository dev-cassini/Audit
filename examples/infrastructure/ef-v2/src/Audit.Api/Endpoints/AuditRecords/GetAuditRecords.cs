using Audit.Infrastructure.Persistence.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Audit.Api.Endpoints.AuditRecords;

public static class GetAuditRecords
{
    public record ActorDto(Guid Id, string Name);
    public record AuditRecordMetadataDto(string PropertyName, string? OriginalValue, string? UpdatedValue);
    public record AuditRecordDto(
        Guid Id,
        Guid ResourceId,
        DateTimeOffset TimestampUtc,
        ActorDto Actor,
        IReadOnlyList<AuditRecordMetadataDto> Metadata);
    
    public record Response(
        int Page,
        int PageSize,
        int TotalCount, 
        IReadOnlyList<AuditRecordDto> AuditRecords);
    
    public static WebApplication RegisterGetAuditRecordsEndpoint(this WebApplication webApplication)
    {
        webApplication
            .MapGet("audit-records", Handler)
            .AllowAnonymous()
            .WithTags(nameof(Pumps))
            .Produces(StatusCodes.Status200OK, typeof(Response))
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound, typeof(string));
            
        return webApplication;
    }

    private static async Task<IResult> Handler(
        [FromQuery] int? page,
        [FromQuery] int? pageSize,
        AuditDbContext auditDbContext,
        CancellationToken cancellationToken)
    {
        var auditRecordsQueryable = auditDbContext.AuditRecords
            .AsNoTracking()
            .Select(x => new AuditRecordDto(
                x.Id,
                x.ResourceId,
                x.TimestampUtc,
                new ActorDto(x.Actor.Id, x.Actor.Name),
                x.Metadata.Select(y => new AuditRecordMetadataDto(
                    y.PropertyName,
                    y.OriginalValue,
                    y.UpdatedValue)).ToList()));
        
        var totalCount = await auditRecordsQueryable.CountAsync(cancellationToken);
        pageSize = pageSize is null or < 1 ? 10 : pageSize.Value;
        var pages = (int)Math.Ceiling((decimal)totalCount / pageSize.Value);
        page = Math.Clamp(page ?? 1, min: 1, max: pages);
        
        if (totalCount is 0)
        {
            return Results.Ok(new Response(page.Value, pageSize.Value, 0, []));
        }
        
        var paginatedAuditRecords = await auditRecordsQueryable
            .Skip((page.Value - 1) * pageSize.Value)
            .Take(pageSize.Value)
            .ToListAsync(cancellationToken);
        
        return Results.Ok(new Response(
            page.Value,
            pageSize.Value,
            totalCount,
            paginatedAuditRecords));
    }
}