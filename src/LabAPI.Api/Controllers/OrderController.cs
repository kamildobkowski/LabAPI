using LabAPI.Application.Dtos.Orders;
using LabAPI.Application.Features.Orders.Commands;
using LabAPI.Application.Features.Orders.Queries;
using LabAPI.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Api.Controllers;

[ApiController]
[Route("api/order")]
public sealed class OrderController(IMediator mediator) : ControllerBase
{
	[HttpPost]
	public async Task<ActionResult> Create([FromBody] CreateOrderDto dto)
	{
		var id = await mediator.Send(new CreateOrderCommand(dto));
		return Created($"api/order/{id}", null);
	}

	[HttpGet]
	public async Task<ActionResult<PagedList<OrderDto>>> GetPage([FromQuery] int page = 1,
		[FromQuery] int pageSize = 20, [FromQuery] string? filterBy = null,
		[FromQuery] string? filter = null, [FromQuery] string? orderBy = "OrderNumber",
		[FromQuery] bool asc = true)
	{
		var list = await mediator.Send(
			new GetAllOrderQuery(page, pageSize, filterBy, filter, orderBy, asc));
		return Ok(list);
	}
	
	[HttpGet("{orderNumber}")]
	public async Task<ActionResult<OrderDto>> GetByOrderNumber([FromRoute] string orderNumber)
	{
		var dto = await mediator.Send(new GetOrderQuery(orderNumber));
		return Ok(dto);
	}

	[HttpDelete("{orderNumber}")]
	public async Task<ActionResult> DeleteByOrderNumber([FromRoute] string orderNumber)
	{
		await mediator.Send(new DeleteOrderCommand(orderNumber));
		return NoContent();
	}
}