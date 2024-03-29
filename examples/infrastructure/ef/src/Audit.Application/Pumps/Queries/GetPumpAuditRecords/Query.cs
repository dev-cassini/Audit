namespace Audit.Application.Pumps.Queries.GetPumpAuditRecords;

public record Query(Guid ForecourtId, Guid LaneId, Guid PumpId);