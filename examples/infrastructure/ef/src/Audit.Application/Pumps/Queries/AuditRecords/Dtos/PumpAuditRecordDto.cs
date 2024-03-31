namespace Audit.Application.Pumps.Queries.AuditRecords.Dtos;

/// <summary>
/// An audit record from a unit of work in which the pump's state was modified.
/// </summary>
/// <param name="Id">Unique identifier of the audit record.</param>
/// <param name="Timestamp">Creation timestamp.</param>
/// <param name="Metadata">Details of the state modification.</param>
public record PumpAuditRecordDto(
    Guid Id,
    DateTimeOffset Timestamp,
    IEnumerable<PumpAuditRecordMetadataDto> Metadata);