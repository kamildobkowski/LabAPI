using LabAPI.Application.Features.OrderResults.Commands;
using LabAPI.Application.Features.OrderResults.Dtos;
using LabAPI.Application.Features.OrderResults.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Api.Controllers;

[ApiController]
[Route("api/orderResult")]
public sealed class OrderResultController(IMediator mediator) : ControllerBase
{
	[HttpPost]
	public async Task<ActionResult> Create([FromBody] CreateOrderResultDto dto)
	{
		await mediator.Send(new CreateOrderResultCommand(dto));
		return Ok();
	}

	[HttpGet("{orderNumber}")]
	public async Task<ActionResult<OrderResultDto>> Get([FromRoute] string orderNumber)
	{
		var dto = await mediator.Send(new GetOrderResultQuery(orderNumber));
		return Ok(dto);
	}
}