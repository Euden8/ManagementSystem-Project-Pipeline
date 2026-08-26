using ManagementSystem.Application.Projects.Queries.GetProjectSlaMetrics;
using ManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
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

   
    [HttpGet("{id:guid}/history")]
    public async Task<IActionResult> GetPhaseHistory(Guid id)
    {
        return Ok();
    }

    [HttpPost("{id:guid}/history")]
    public async Task<IActionResult> AddPhaseHistory(Guid id, [FromBody] ProjectPhaseHistory entry)
    {
        return CreatedAtAction(nameof(GetPhaseHistory), new { id }, entry);
    }

    [HttpPut("{id:guid}/history/{historyId:guid}")]
    public IActionResult UpdatePhaseHistory(Guid id, Guid historyId)
    {
        return StatusCode(StatusCodes.Status405MethodNotAllowed,
            new { Message = "Project phase history records are immutable and cannot be modified." });
    }

    [HttpDelete("{id:guid}/history/{historyId:guid}")]
    public IActionResult DeletePhaseHistory(Guid id, Guid historyId)
    {
        return StatusCode(StatusCodes.Status405MethodNotAllowed,
            new { Message = "Project phase history records are immutable and cannot be deleted." });
    }
}