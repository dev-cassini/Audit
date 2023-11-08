namespace Audit.Domain;

public interface IAuditable
{
    void AddAuditRecord();
}