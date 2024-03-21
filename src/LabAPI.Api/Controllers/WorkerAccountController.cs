using LabAPI.Application.Features.Accounts.Commands;
using LabAPI.Application.Features.Accounts.Dtos;
using LabAPI.Application.Features.Accounts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Api.Controllers;

[ApiController]
[Route("/api/worker")]
public sealed class WorkerAccountController(IMediator mediator) : ControllerBase
{
	[HttpPost("register")]
	public async Task<ActionResult> Register([FromBody] RegisterWorkerDto dto)
	{
		var response = await mediator.Send(new RegisterWorkerCommand(dto));
		return Ok(response);
	}

	[HttpPost("login")]
	public async Task<ActionResult<string>> Login([FromBody] LoginDto dto)
	{
		var token = await mediator.Send(new LoginWorkerQuery(dto));
		return Ok(token);
	}

	[HttpPatch("changePassword")]
	public async Task<ActionResult> ChangePassword([FromBody] ChangeWorkerPasswordDto dto)
	{
		await mediator.Send(new ChangeWorkerPasswordCommand(dto));
		return Ok();
	}
}