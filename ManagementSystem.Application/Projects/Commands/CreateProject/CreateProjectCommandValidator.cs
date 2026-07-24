using FluentValidation;

namespace ManagementSystem.Application.Projects.Commands.CreateProject;

public class CreateProjectCommandValidator
    : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
                .WithMessage("Project code is required.")
            .Must(code => !string.IsNullOrWhiteSpace(code))
                .WithMessage("Project code cannot be blank.")
            .MaximumLength(20)
                .WithMessage("Project code cannot exceed 20 characters.")
            .Matches(@"^[A-Za-z0-9-]+$")
                .WithMessage(
                    "Project code may contain only letters, numbers and hyphens.");

        RuleFor(x => x.Title)
            .NotEmpty()
                .WithMessage("Project title is required.")
            .Must(title => !string.IsNullOrWhiteSpace(title))
                .WithMessage("Project title cannot be blank.")
            .MaximumLength(200)
                .WithMessage("Project title cannot exceed 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(2000)
                .WithMessage("Project description cannot exceed 2000 characters.");

        RuleFor(x => x.CurrentPhaseId)
            .NotEmpty()
                .WithMessage("A current phase is required.");

        RuleFor(x => x.Priority)
            .IsInEnum()
                .WithMessage("Priority must be a valid value.");

        RuleFor(x => x.OwnerUserId)
            .NotEmpty()
                .WithMessage("A project owner is required.")
            .Must(ownerId => !string.IsNullOrWhiteSpace(ownerId))
                .WithMessage("Project owner cannot be blank.");

        RuleFor(x => x)
            .Must(command =>
                !command.PlannedStartDate.HasValue ||
                !command.PlannedEndDate.HasValue ||
                command.PlannedEndDate.Value >= command.PlannedStartDate.Value)
            .WithMessage(
                "Planned end date cannot be earlier than planned start date.");
    }
}