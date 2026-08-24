using ManagementSystem.Application.Projects.Queries.GetProjectScheduleVariance;
using ManagementSystem.Application.Projects.Queries.GetProjectSlaMetrics;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}/sla-metrics")]
    public async Task<ActionResult<ProjectSlaMetricsDto>> GetSlaMetrics(
        Guid id,
        [FromQuery] double thresholdDays = 30)
    {
        var result = await _mediator.Send(new GetProjectSlaMetricsQuery(id, thresholdDays));
        return Ok(result);
    }

    [HttpGet("schedule-variance")]
    public async Task<ActionResult<List<ProjectScheduleVarianceDto>>> GetScheduleVariance(
    [FromQuery] bool atRiskOnly = true)
    {
        var result = await _mediator.Send(new GetProjectScheduleVarianceQuery(atRiskOnly));
        return Ok(result);
    }
}