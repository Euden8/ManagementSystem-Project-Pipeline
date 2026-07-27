using FluentValidation;

namespace ManagementSystem.Application.Phases.Commands.UpdatePhase;

public class UpdatePhaseCommandValidator : AbstractValidator<UpdatePhaseCommand>
{
    public UpdatePhaseCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name of phase is required.")
            .MaximumLength(100);

        RuleFor(x => x.Sequence)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ColorHex)
            .NotEmpty()
            .Matches("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")
            .WithMessage("ColorHex must be a valid hex format.");
    }
}