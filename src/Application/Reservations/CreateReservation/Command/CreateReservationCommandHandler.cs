using Application.Reservations;
using Domain.Reservations;
using MediatR;

namespace Application.Reservations.CreateReservation.Command;

public sealed class CreateReservationCommandHandler
    : IRequestHandler<CreateReservationCommand, Guid>
{
    private readonly IReservationRepository _repo;

    public CreateReservationCommandHandler(IReservationRepository repo)
    {
        _repo = repo;
    }

    public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken ct)
    {
        var reservation = new Reservation(
            request.CustomerName,
            request.Date,
            request.Notes ?? string.Empty);

        await _repo.AddAsync(reservation, ct);

        return reservation.Id;
    }
}
