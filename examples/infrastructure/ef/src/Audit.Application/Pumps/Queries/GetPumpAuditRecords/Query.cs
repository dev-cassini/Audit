using MediatR;

namespace Audit.Application.Pumps.Queries.GetPumpAuditRecords;

public record Query(
    Guid ForecourtId, 
    Guid LaneId, 
    Guid PumpId) : IRequest<IEnumerable<Dtos.PumpAuditRecordDto>>;