using Audit.Application.Pumps.Queries.AuditRecords.Dtos;

namespace Audit.Application.Pumps.Queries.AuditRecords;

public interface IQueryHandler
{
    /// <summary>
    /// Get a list of audit records associated with a pump.
    /// </summary>
    /// <param name="query">Pump identifiers.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    /// If a pump is found based on the query parameters then a list of audit records
    /// associated with that pump, else an empty list.
    /// </returns>
    Task<IEnumerable<PumpAuditRecordDto>> Handle(Query query, CancellationToken cancellationToken);
}