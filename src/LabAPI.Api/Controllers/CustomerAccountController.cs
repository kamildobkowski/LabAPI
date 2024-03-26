using LabAPI.Application.Features.Accounts.Commands;
using LabAPI.Application.Features.Accounts.Dtos;
using LabAPI.Application.Features.Accounts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Api.Controllers;

[ApiController]
[Route("/api/customer")]
public sealed class CustomerAccountController(IMediator mediator, ILogger<CustomerAccountController> logger) 
	: ControllerBase
{
	[HttpPost("register")]
	public async Task<ActionResult> Register([FromBody] RegisterCustomerDto dto)
	{
		logger.LogInformation("Customer Register endpoint invoked");
		await mediator.Send(new RegisterCustomerCommand(dto));
		return Ok();
	}

	[HttpPost("login")]
	public async Task<ActionResult<string>> Login([FromBody] LoginDto dto)
	{
		logger.LogInformation("Customer Login endpoint invoked");
		var token = await mediator.Send(new LoginCustomerQuery(dto));
		return Ok(token);
	}

	[HttpPatch("changepassword")]
	public async Task<ActionResult> ChangePassword([FromBody] ChangeCustomerPasswordDto dto)
	{
		logger.LogInformation("Customer ChangePassword endpoint invoked");
		await mediator.Send(new ChangeCustomerPasswordCommand(dto));
		return Ok();
	}
}