using FluentValidation;
using FluentValidation.Results;
using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Domain.Entities;
using MediatR;

namespace ManagementSystem.Application.Projects.Commands.CreateProject;

public sealed class CreateProjectCommandHandler
    : IRequestHandler<CreateProjectCommand, Guid>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateProjectCommandHandler(
        IProjectRepository projectRepository,
        ICurrentUserService currentUserService)
    {
        _projectRepository = projectRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(
        CreateProjectCommand request,
        CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;

        if (string.IsNullOrWhiteSpace(currentUserId))
        {
            throw new UnauthorizedAccessException(
                "An authenticated user is required to create a project.");
        }
        
        if (await _projectRepository.CodeExistsAsync(
                request.Code,
                cancellationToken))
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure(
                    nameof(request.Code),
                    "A project with this code already exists.")
            });
        }



        var project = new PipelineProject(
            Guid.NewGuid(),
            request.Code,
            request.Title,
            request.CurrentPhaseId,
            request.Priority,
            request.OwnerUserId,
            currentUserId,
            request.Description);

        if (request.PlannedStartDate.HasValue ||
            request.PlannedEndDate.HasValue)
        {
            project.SetPlannedDates(
                request.PlannedStartDate,
                request.PlannedEndDate,
                currentUserId);
        }

        await _projectRepository.AddAsync(project, cancellationToken);
        await _projectRepository.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}