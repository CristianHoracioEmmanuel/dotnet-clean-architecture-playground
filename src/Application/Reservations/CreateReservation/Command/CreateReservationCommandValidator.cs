using FluentValidation;

namespace Application.Reservations.CreateReservation.Command;

public sealed class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(x => x.CustomerName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(80);

        RuleFor(x => x.Date)
            .NotEmpty()
            .Must(d => d > DateTime.UtcNow.AddMinutes(-1))
            .WithMessage("Date must be in the future.");

        RuleFor(x => x.Notes)
            .MaximumLength(500);
    }
}
