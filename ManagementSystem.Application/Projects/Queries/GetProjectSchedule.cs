namespace ManagementSystem.Application.Projects.Queries.GetProjectScheduleVariance;

public class ProjectScheduleVarianceDto
{
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public DateTime PlannedStartDate { get; set; }
    public DateTime PlannedEndDate { get; set; }
    public DateTime? ActualStartDate { get; set; }
    public DateTime? ActualEndDate { get; set; }
    public int StartVarianceDays { get; set; }
    public int EndVarianceDays { get; set; }
    public bool IsAtRisk { get; set; }
    public string RiskReason { get; set; } = string.Empty;
}