using MediatR;
using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Application.Phases.Commands.CreatePhase;
using ManagementSystem.Application.Phases.Commands.UpdatePhase;
using ManagementSystem.Application.Phases.Commands.DeletePhase;
using ManagementSystem.Application.Phases.Queries.GetAllPhases;
using ManagementSystem.Application.Phases.Queries.GetPhaseById;

namespace ManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhasesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PhasesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePhase([FromBody] CreatePhaseCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { Id = id });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPhases()
    {
        var phases = await _mediator.Send(new GetAllPhasesQuery());
        return Ok(phases);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPhaseById(Guid id)
    {
        var phase = await _mediator.Send(new GetPhaseByIdQuery(id));
        return phase is null ? NotFound() : Ok(phase);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePhase(Guid id, [FromBody] UpdatePhaseCommand command)
    {
        if (id != command.Id) return BadRequest("Id mismatch.");
        var result = await _mediator.Send(command);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePhase(Guid id)
    {
        var result = await _mediator.Send(new DeletePhaseCommand(id));
        return result ? NoContent() : NotFound();
    }
}