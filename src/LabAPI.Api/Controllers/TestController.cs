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

	[HttpGet("{shortName}")]
	public async Task<ActionResult<TestDto>> GetTestFromName([FromRoute] string shortName)
	{
		var dto = await mediator.Send(new GetTestFromShortNameQuery(shortName));
		return Ok(dto);
	}
}