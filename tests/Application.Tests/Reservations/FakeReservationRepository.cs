using Application.Reservations;
using Domain.Reservations;

namespace Application.Tests.Reservations;

public sealed class FakeReservationRepository : IReservationRepository
{
    public List<Reservation> Stored { get; } = new();

    public Task AddAsync(Reservation reservation, CancellationToken ct)
    {
        Stored.Add(reservation);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<Reservation>> ListAsync(CancellationToken ct)
        => Task.FromResult((IReadOnlyList<Reservation>)Stored.ToList());
}
