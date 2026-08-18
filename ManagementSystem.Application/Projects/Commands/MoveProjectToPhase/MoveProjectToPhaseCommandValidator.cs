using FluentValidation;

namespace ManagementSystem.Application.Projects.Commands.MoveProjectToPhase;

public class MoveProjectToPhaseCommandValidator
    : AbstractValidator<MoveProjectToPhaseCommand>
{
    public MoveProjectToPhaseCommandValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("A project id is required.");
        RuleFor(x => x.TargetPhaseId).NotEmpty().WithMessage("A target phase id is required.");
        RuleFor(x => x.Note).MaximumLength(1000).WithMessage("Note cannot exceed 1000 characters.");
    }
}