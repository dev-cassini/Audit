using MediatR;

namespace Audit.Domain.Events;

public record ForecourtCreated(Guid Id) : INotification;