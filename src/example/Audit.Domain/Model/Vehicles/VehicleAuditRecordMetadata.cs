namespace Audit.Domain.Model.Vehicles;

public class VehicleAuditRecordMetadata
{
    public Guid Id { get; } = Guid.NewGuid();
    public Guid VehicleAuditRecordId { get; }
    public string PropertyName { get; } = null!;
    public string? OriginalValue { get; }
    public string? UpdatedValue { get; }
    
    public VehicleAuditRecordMetadata(
        VehicleAuditRecord vehicleAuditRecord, 
        string propertyName, 
        string? originalValue, 
        string? updatedValue)
    {
        VehicleAuditRecordId = vehicleAuditRecord.Id;
        PropertyName = propertyName;
        OriginalValue = originalValue;
        UpdatedValue = updatedValue;
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    private VehicleAuditRecordMetadata() { }
    #endregion
}