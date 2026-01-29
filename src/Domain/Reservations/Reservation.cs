namespace Domain.Reservations;

public sealed class Reservation
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string CustomerName { get; private set; }
    public DateTime Date { get; private set; }
    public string Notes { get; private set; }

    private Reservation() { }

    public Reservation(string customerName, DateTime date, string notes)
    {
        if (string.IsNullOrWhiteSpace(customerName))
            throw new ArgumentException("CustomerName is required.", nameof(customerName));

        CustomerName = customerName.Trim();
        Date = date;
        Notes = notes?.Trim() ?? string.Empty;
    }
}
