using LabAPI.Application.Features.Accounts.Commands;
using LabAPI.Application.Features.Accounts.Dtos;
using LabAPI.Application.Features.Accounts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Api.Controllers;

[ApiController]
[Route("/api/worker")]
public sealed class WorkerAccountController(IMediator mediator, ILogger<WorkerAccountController> logger) 
	: ControllerBase
{
	[Authorize(Roles="Admin")]
	[HttpPost("register")]
	public async Task<ActionResult> Register([FromBody] RegisterWorkerDto dto)
	{
		logger.LogInformation("Worker Register endpoint invoked");
		var response = await mediator.Send(new RegisterWorkerCommand(dto));
		return Ok(response);
	}

	[HttpPost("login")]
	public async Task<ActionResult<string>> Login([FromBody] LoginDto dto)
	{
		logger.LogInformation("Worker Login endpoint invoked");
		var token = await mediator.Send(new LoginWorkerQuery(dto));
		return Ok(token);
	}

	[HttpPatch("changePassword")]
	public async Task<ActionResult> ChangePassword([FromBody] ChangeWorkerPasswordDto dto)
	{
		logger.LogInformation("Worker Change Password endpoint invoked");
		await mediator.Send(new ChangeWorkerPasswordCommand(dto));
		return Ok();
	}
}