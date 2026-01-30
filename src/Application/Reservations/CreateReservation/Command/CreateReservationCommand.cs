using MediatR;

namespace Application.Reservations.CreateReservation.Command;

public sealed record CreateReservationCommand(
    string CustomerName,
    DateTime Date,
    string? Notes
) : IRequest<Guid>;
