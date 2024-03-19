using LabAPI.Application.Features.OrderResults.Commands;
using LabAPI.Application.Features.OrderResults.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Api.Controllers;

[ApiController]
[Route("api/orderResult")]
public sealed class OrderResultController(IMediator mediator) : ControllerBase
{
	[HttpPost]
	public async Task<ActionResult> CreateOrderResult([FromBody] CreateOrderResultDto dto)
	{
		await mediator.Send(new CreateOrderResultCommand(dto));
		return Ok();
	}  
}