using Domain.Reservations;

namespace Application.Reservations;

public interface IReservationRepository
{
    Task AddAsync(Reservation reservation, CancellationToken ct);
    Task<IReadOnlyList<Reservation>> ListAsync(CancellationToken ct);
}
