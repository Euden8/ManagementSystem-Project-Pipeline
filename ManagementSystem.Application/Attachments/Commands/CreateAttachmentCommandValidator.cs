using FluentValidation;

namespace ManagementSystem.Application.Attachments.Commands.CreateAttachment;

public class CreateAttachmentCommandValidator : AbstractValidator<CreateAttachmentCommand>
{
    public CreateAttachmentCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty();

        RuleFor(x => x.FileName)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.FilePath)
            .NotEmpty();

        RuleFor(x => x.FileSizeBytes)
            .GreaterThan(0);
    }
}