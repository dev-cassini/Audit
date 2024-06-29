using MediatR;

namespace Audit.Application.Commands.Forecourts.Create;

public record CreateForecourtCommand() : IRequest<Guid>;