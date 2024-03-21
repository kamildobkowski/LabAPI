using LabAPI.Application.Features.Accounts.Commands;
using LabAPI.Application.Features.Accounts.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Api.Controllers;

[ApiController]
[Route("api/accounts")]
public sealed class AccountController(IMediator mediator) : ControllerBase
{
	[HttpPost]
	public async Task<ActionResult> RegisterCustomer([FromBody] RegisterCustomerDto dto)
	{
		await mediator.Send(new RegisterCustomerCommand(dto));
		return Ok();
	}
}