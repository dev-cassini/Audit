using Audit.Application.Pumps.Queries.GetPumpAuditRecords.Dtos;

namespace Audit.Application.Pumps.Queries.GetPumpAuditRecords;

public interface IQueryHandler
{
    Task<IEnumerable<PumpAuditRecordDto>> Handle(Query request, CancellationToken cancellationToken);
}