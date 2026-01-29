using Application.Reservations.CreateReservation;
using Application.Tests.Reservations;
using Xunit;

namespace Application.Tests.Reservations.CreateReservation;

public class CreateReservationHandlerTests
{
    [Fact]
    public async Task HandleAsync_Should_Create_Reservation_And_Return_Id()
    {
        // Arrange
        var repo = new FakeReservationRepository();
        var handler = new CreateReservationHandler(repo);

        var request = new CreateReservationRequest(
            CustomerName: "Cristian",
            Date: DateTime.UtcNow.AddDays(1),
            Notes: "demo"
        );

        // Act
        var response = await handler.HandleAsync(request, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.Single(repo.Stored);
        Assert.Equal("Cristian", repo.Stored[0].CustomerName);
    }

    [Fact]
    public async Task HandleAsync_Should_Throw_When_CustomerName_Is_Empty()
    {
        // Arrange
        var repo = new FakeReservationRepository();
        var handler = new CreateReservationHandler(repo);

        var request = new CreateReservationRequest(
            CustomerName: "   ",
            Date: DateTime.UtcNow.AddDays(1),
            Notes: null
        );

        // Act + Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => handler.HandleAsync(request, CancellationToken.None)
        );
    }
}
