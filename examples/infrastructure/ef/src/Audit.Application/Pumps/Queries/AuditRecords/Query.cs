using Audit.Application.Pumps.Queries.AuditRecords.Dtos;
using MediatR;

namespace Audit.Application.Pumps.Queries.AuditRecords;

public record Query(
    Guid ForecourtId, 
    Guid LaneId, 
    Guid PumpId) : IRequest<IEnumerable<PumpAuditRecordDto>>;