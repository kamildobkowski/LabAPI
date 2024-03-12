using LabAPI.Application.Dtos.Tests;
using LabAPI.Application.Features.Tests.Commands;
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
}