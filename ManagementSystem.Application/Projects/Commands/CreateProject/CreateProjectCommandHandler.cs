using FluentValidation;
using FluentValidation.Results;
using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Application.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateProjectCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;

        if (string.IsNullOrWhiteSpace(currentUserId))
        {
            throw new UnauthorizedAccessException(
                "An authenticated user is required to create a project.");
        }

        if (await _context.Projects
                .IgnoreQueryFilters()
                .AnyAsync(project => project.Code == request.Code, cancellationToken))
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

        if (request.PlannedStartDate.HasValue || request.PlannedEndDate.HasValue)
        {
            project.SetPlannedDates(
                request.PlannedStartDate,
                request.PlannedEndDate,
                currentUserId);
        }

        _context.Projects.Add(project);
        await _context.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}
