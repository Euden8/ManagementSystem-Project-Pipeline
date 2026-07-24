using ManagementSystem.Domain.Entities;
using MediatR;

namespace ManagementSystem.Application.Projects.Commands.CreateProject
{
    public record CreateProjectCommand
    (
        string Code,
        string Title,
        string? Description,
        Guid CurrentPhaseId,
        ProjectPriority Priority,
        string OwnerUserId,
        DateTime? PlannedStartDate,
        DateTime? PlannedEndDate
    ) : IRequest<Guid>;
}


