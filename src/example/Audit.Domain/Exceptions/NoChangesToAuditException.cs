namespace Audit.Domain.Exceptions;

public class NoChangesToAuditException : Exception
{
    public NoChangesToAuditException(object entity, Guid id) 
        : base($"Tried to create an audit record for entity of type {entity.GetType()} " +
               $"with id {id} but there was no changes to audit.") { }
}