using MediatR;

namespace ManagementSystem.Application.Attachments.Commands.DeleteAttachment;

public record DeleteAttachmentCommand(Guid Id) : IRequest<bool>;