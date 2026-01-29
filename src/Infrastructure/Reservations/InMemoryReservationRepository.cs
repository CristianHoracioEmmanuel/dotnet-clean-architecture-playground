using Application.Reservations;
using Domain.Reservations;

namespace Infrastructure.Reservations;

public sealed class InMemoryReservationRepository : IReservationRepository
{
    private static readonly List<Reservation> _data = new();

    public Task AddAsync(Reservation reservation, CancellationToken ct)
    {
        _data.Add(reservation);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<Reservation>> ListAsync(CancellationToken ct)
    {
        IReadOnlyList<Reservation> snapshot = _data.ToList();
        return Task.FromResult(snapshot);
    }
}
