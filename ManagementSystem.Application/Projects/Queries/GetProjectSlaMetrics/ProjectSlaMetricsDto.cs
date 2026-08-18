namespace ManagementSystem.Application.Projects.Queries.GetProjectSlaMetrics;

public class ProjectSlaMetricsDto
{
    public Guid ProjectId { get; set; }
    public double TotalCycleTimeDays { get; set; }
    public bool IsSlaBreached { get; set; }
    public List<PhaseDurationDto> PhaseDurations { get; set; } = new();
}

public class PhaseDurationDto
{
    public Guid PhaseId { get; set; }
    public double DurationDays { get; set; }
    public double TargetSlaDays { get; set; }
    public bool IsExceeded { get; set; }
}