namespace ManagementSystem.Domain.Entities
{
    public class ProjectPhaseHistory
    {
        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public Guid? FromPhaseId { get; private set; }
        public Guid ToPhaseId { get; private set; }
        public string ChangedByUserId { get; private set; } = string.Empty;
        public DateTime ChangedAt { get; private set; }
        public TimeSpan? DurationInPreviousPhase { get; private set; }
        public string? Note { get; private set; }

        private ProjectPhaseHistory() { }

        public ProjectPhaseHistory(
            Guid id,
            Guid projectId,
            Guid? fromPhaseId,
            Guid toPhaseId,
            string changedByUserId,
            DateTime changedAt,
            TimeSpan? durationInPreviousPhase = null,
            string? note = null)
        {
            if (id == Guid.Empty) throw new ArgumentException("Id is required.", nameof(id));
            if (projectId == Guid.Empty) throw new ArgumentException("ProjectId is required.", nameof(projectId));
            if (toPhaseId == Guid.Empty) throw new ArgumentException("ToPhaseId is required.", nameof(toPhaseId));
            if (changedAt == default) throw new ArgumentException("ChangedAt date is required.", nameof(changedAt));
            if (string.IsNullOrWhiteSpace(changedByUserId)) throw new ArgumentException("ChangedByUserId is required.", nameof(changedByUserId));

            Id = id;
            ProjectId = projectId;
            FromPhaseId = fromPhaseId;
            ToPhaseId = toPhaseId;
            ChangedByUserId = changedByUserId;
            ChangedAt = changedAt;
            DurationInPreviousPhase = durationInPreviousPhase;
            Note = note;
        }
    }
}