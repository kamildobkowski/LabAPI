using LabAPI.Application.Features.Accounts.Commands;
using LabAPI.Application.Features.Accounts.Dtos;
using LabAPI.Application.Features.Accounts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Api.Controllers;

[ApiController]
[Route("api/accounts")]
public sealed class AccountController(IMediator mediator) : ControllerBase
{
	[HttpPost("register/customer")]
	public async Task<ActionResult> RegisterCustomer([FromBody] RegisterCustomerDto dto)
	{
		await mediator.Send(new RegisterCustomerCommand(dto));
		return Ok();
	}

	[HttpPost("login/customer")]
	public async Task<ActionResult<string>> LoginCustomer([FromBody] LoginDto dto)
	{
		var token = await mediator.Send(new LoginCustomerQuery(dto));
		return Ok(token);
	}
}