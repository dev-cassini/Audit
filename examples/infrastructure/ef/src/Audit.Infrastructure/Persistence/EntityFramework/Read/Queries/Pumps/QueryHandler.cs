using Audit.Application.Pumps.Queries.GetPumpAuditRecords;
using Audit.Application.Pumps.Queries.GetPumpAuditRecords.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Audit.Infrastructure.Persistence.EntityFramework.Read.Queries.Pumps;

public class QueryHandler(DbContext readDbContext) : IQueryHandler, IRequestHandler<Query, IEnumerable<PumpAuditRecordDto>>
{
    public async Task<IEnumerable<PumpAuditRecordDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        return await readDbContext.PumpAuditRecordsView
            .Where(x => x.Pump.Lane.ForecourtId == query.ForecourtId)
            .Where(x => x.Pump.LaneId == query.LaneId)
            .Where(x => x.PumpId == query.PumpId)
            .Select(x => new PumpAuditRecordDto(
                x.Id,
                x.Timestamp,
                x.Metadata.Select(y => new PumpAuditRecordMetadataDto(
                    y.PropertyName,
                    y.OriginalValue,
                    y.UpdatedValue))))
            .ToListAsync(cancellationToken);
    }
}