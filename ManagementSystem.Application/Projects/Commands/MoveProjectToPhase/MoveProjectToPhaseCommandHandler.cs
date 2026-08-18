using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Domain;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Infrastructure.Persistence.Repositories;
using MediatR;

namespace ManagementSystem.Application.Projects.Commands.MoveProjectToPhase;

public sealed class MoveProjectToPhaseCommandHandler
    : IRequestHandler<MoveProjectToPhaseCommand, Guid>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IPhaseRepository _phaseRepository;
    private readonly IProjectPhaseHistoryRepository _historyRepository;
    private readonly ICurrentUserService _currentUserService;

    public MoveProjectToPhaseCommandHandler(
        IProjectRepository projectRepository,
        IPhaseRepository phaseRepository,
        IProjectPhaseHistoryRepository historyRepository,
        ICurrentUserService currentUserService)
    {
        _projectRepository = projectRepository;
        _phaseRepository = phaseRepository;
        _historyRepository = historyRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(
        MoveProjectToPhaseCommand request,
        CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;

        if (string.IsNullOrWhiteSpace(currentUserId))
        {
            throw new UnauthorizedAccessException(
                "An authenticated user is required to move a project between phases.");
        }

        var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project is null)
        {
            throw new KeyNotFoundException($"Project '{request.ProjectId}' was not found.");
        }

        var previousPhase = project.CurrentPhase
            ?? throw new InvalidOperationException(
                $"Project '{project.Id}' has no current phase loaded; cannot validate transition.");

        var targetPhase = await _phaseRepository.GetByIdAsync(request.TargetPhaseId, cancellationToken);

        if (targetPhase is null)
        {
            throw new KeyNotFoundException($"Phase '{request.TargetPhaseId}' was not found.");
        }

        PhaseTransitionRules.EnsureValidTransition(previousPhase, targetPhase);

        var lastHistoryEntry = await _historyRepository.GetLastForProjectAsync(
            project.Id,
            cancellationToken);

        var enteredPreviousPhaseAt = lastHistoryEntry?.ChangedAt ?? project.CreatedAt;
        var occurredAt = DateTime.UtcNow;

        project.MoveToPhase(targetPhase, occurredAt, currentUserId);

        var historyEntry = ProjectPhaseHistory.Create(
            project.Id,
            previousPhase,
            targetPhase,
            currentUserId,
            occurredAt,
            enteredPreviousPhaseAt,
            request.Note);

        await _historyRepository.AddAsync(historyEntry, cancellationToken);
        await _projectRepository.SaveChangesAsync(cancellationToken);

        return historyEntry.Id;
    }
}