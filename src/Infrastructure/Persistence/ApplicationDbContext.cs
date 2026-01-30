using Domain.Reservations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>(b =>
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.CustomerName)
                .IsRequired()
                .HasMaxLength(80);

            b.Property(x => x.Notes)
                .HasMaxLength(500);

            b.Property(x => x.Date)
                .IsRequired();
        });
    }
}
