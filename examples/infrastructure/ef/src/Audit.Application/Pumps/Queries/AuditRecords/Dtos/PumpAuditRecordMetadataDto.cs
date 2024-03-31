namespace Audit.Application.Pumps.Queries.AuditRecords.Dtos;

/// <summary>
/// Details of the state modifications associated with an audit record.
/// </summary>
/// <param name="PropertyName">Name of property that changed.</param>
/// <param name="OriginalValue">Original value of property.</param>
/// <param name="UpdatedValue">Updated value of property.</param>
public record PumpAuditRecordMetadataDto(
    string PropertyName,
    string? OriginalValue,
    string? UpdatedValue);