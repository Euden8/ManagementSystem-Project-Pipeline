using FluentValidation;

namespace ManagementSystem.Application.Phases.Commands.CreatePhase;

public class CreatePhaseCommandValidator : AbstractValidator<CreatePhaseCommand>
{
    public CreatePhaseCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name of phase is required.")
            .MaximumLength(100);

        RuleFor(x => x.Sequence)
            .GreaterThanOrEqualTo(0).WithMessage("Sequence must be a non-negative number    .");

        RuleFor(x => x.ColorHex)
            .NotEmpty()
            .Matches("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")
            .WithMessage("ColorHex must be a valid hex format.");
    }
}