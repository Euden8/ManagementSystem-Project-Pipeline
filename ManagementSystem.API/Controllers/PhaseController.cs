using MediatR;
using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Application.Phases.Commands.CreatePhase;

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
}