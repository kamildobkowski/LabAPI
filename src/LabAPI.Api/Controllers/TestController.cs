using LabAPI.Application.Dtos.Tests;
using LabAPI.Application.Features.Tests.Commands;
using LabAPI.Application.Features.Tests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Api.Controllers;

[ApiController]
[Route("api/tests")]
public class TestController(IMediator mediator) : ControllerBase
{
	[HttpPost]
	public async Task<ActionResult> CreateTest([FromBody] CreateTestDto dto)
	{
		await mediator.Send(new CreateTestCommand(dto));
		return Created();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<TestDto>> GetTest([FromRoute] string id)
	{
		var dto = await mediator.Send(new GetTestQuery(id));
		return Ok(dto);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteTest([FromRoute] string id)
	{
		await mediator.Send(new DeleteTestCommand(id));
		return NoContent();
	}
}