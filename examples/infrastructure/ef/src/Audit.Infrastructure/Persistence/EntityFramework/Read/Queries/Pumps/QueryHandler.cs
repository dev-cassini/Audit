using Audit.Application.Pumps.Queries.GetPumpAuditRecords;
using Audit.Application.Pumps.Queries.GetPumpAuditRecords.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Audit.Infrastructure.Persistence.EntityFramework.Read.Queries.Pumps;

public class QueryHandler(DbContext readDbContext) : IQueryHandler, IRequestHandler<Query, IEnumerable<PumpAuditRecordDto>>
{
    public async Task<IEnumerable<PumpAuditRecordDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        return await readDbContext.ForecourtsView
            .Where(x => x.Id == query.ForecourtId)
            .Where(x => x.Lanes.Any(y => y.Id == query.LaneId))
            .Where(x => x.Lanes.SelectMany(y => y.Pumps).Any(y => y.Id == query.PumpId))
            .Select(x => new PumpAuditRecordDto())
            .ToListAsync(cancellationToken);
    }
}