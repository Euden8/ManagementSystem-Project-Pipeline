using MediatR;
using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Application.Projects.Queries.GetProjectSlaMetrics;

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
}