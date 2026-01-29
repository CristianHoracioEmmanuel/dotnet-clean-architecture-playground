using Domain.Reservations;

namespace Application.Reservations.CreateReservation;

public sealed class CreateReservationHandler
{
    private readonly Reservations.IReservationRepository _repo;

    public CreateReservationHandler(Reservations.IReservationRepository repo)
    {
        _repo = repo;
    }

    public async Task<CreateReservationResponse> HandleAsync(
        CreateReservationRequest request,
        CancellationToken ct)
    {
        var reservation = new Reservation(
            request.CustomerName,
            request.Date,
            request.Notes ?? string.Empty);

        await _repo.AddAsync(reservation, ct);

        return new CreateReservationResponse(reservation.Id);
    }
}
