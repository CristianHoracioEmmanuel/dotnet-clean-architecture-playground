namespace Application.Reservations.CreateReservation;

public sealed record CreateReservationRequest(
    string CustomerName,
    DateTime Date,
    string? Notes
);

public sealed record CreateReservationResponse(
    Guid Id
);
