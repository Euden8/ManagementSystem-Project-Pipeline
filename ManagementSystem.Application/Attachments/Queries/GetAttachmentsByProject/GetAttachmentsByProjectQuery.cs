using ManagementSystem.Domain.Entities;
using MediatR;

namespace ManagementSystem.Application.Attachments.Queries.GetAttachmentsByProject;

public record GetAttachmentsByProjectQuery(
    Guid ProjectId
) : IRequest<List<Attachment>>;