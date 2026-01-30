using Application.Reservations;
using Domain.Reservations;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Reservations;

public sealed class EfReservationRepository : IReservationRepository
{
    private readonly ApplicationDbContext _db;

    public EfReservationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Reservation reservation, CancellationToken ct)
    {
        _db.Reservations.Add(reservation);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<Reservation>> ListAsync(CancellationToken ct)
    {
        return await _db.Reservations
            .AsNoTracking()
            .OrderByDescending(x => x.Date)
            .ToListAsync(ct);
    }
}
