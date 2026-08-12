using ManagementSystem.Application.Attachments.Commands.CreateAttachment;
using ManagementSystem.Application.Attachments.Commands.DeleteAttachment;
using ManagementSystem.Application.Attachments.Commands.UpdateAttachment;
using ManagementSystem.Application.Attachments.Queries.GetAttachmentById;
using ManagementSystem.Application.Attachments.Queries.GetAttachmentsByProject;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AttachmentsController : ControllerBase
{
    private readonly ISender _mediator;

    public AttachmentsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateAttachmentCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetByProject), new { projectId = command.ProjectId }, id);
    }

    [HttpGet("project/{projectId:guid}")]
    public async Task<ActionResult> GetByProject(Guid projectId)
    {
        var result = await _mediator.Send(new GetAttachmentsByProjectQuery(projectId));
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var success = await _mediator.Send(new DeleteAttachmentCommand(id));
        return success ? NoContent() : NotFound();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetAttachmentByIdQuery(id));
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateAttachmentCommand command)
    {
        if (id != command.Id)
            return BadRequest("Route ID and payload ID mismatch.");

        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound();
    }
}