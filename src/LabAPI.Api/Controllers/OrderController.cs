using LabAPI.Application.Features.Orders.Commands;
using LabAPI.Application.Features.Orders.Dtos;
using LabAPI.Application.Features.Orders.Queries;
using LabAPI.Domain.Common;
using LabAPI.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Api.Controllers;

[ApiController]
[Route("api/order")]
[Authorize]
[Authorize(Policy = "IsLabWorker")]
public sealed class OrderController(IMediator mediator, ILogger<OrderController> logger) 
	: ControllerBase
{
	[HttpPost]
	public async Task<ActionResult> Create([FromBody] CreateOrderDto dto)
	{
		logger.LogInformation("Create Order endpoint invoked");
		var id = await mediator.Send(new CreateOrderCommand(dto));
		return Created($"api/order/{id}", null);
	}

	[HttpGet]
	public async Task<ActionResult<PagedList<OrderDto>>> GetPage([FromQuery] int page = 1,
		[FromQuery] int pageSize = 20, [FromQuery] string? filterBy = null,
		[FromQuery] string? filter = null, [FromQuery] string? orderBy = "OrderNumber",
		[FromQuery] bool asc = true)
	{
		logger.LogInformation("Get Page endpoint invoked");
		var list = await mediator.Send(
			new GetAllOrderQuery(page, pageSize, filterBy, filter, orderBy, asc));
		return Ok(list);
	}
	
	[HttpGet("{orderNumber}")]
	public async Task<ActionResult<OrderDto>> GetByOrderNumber([FromRoute] string orderNumber)
	{
		logger.LogInformation("Get Order endpoint invoked");
		var dto = await mediator.Send(new GetOrderQuery(orderNumber));
		return Ok(dto);
	}
	
	[HttpGet("pesel")]
	[AllowAnonymous]
	public async Task<ActionResult> GetFileByPesel([FromQuery] string? pesel, [FromQuery] string orderNumber)
	{
		logger.LogInformation("Get File by PESEL endpoint invoked");
		var userPesel = User.Claims.FirstOrDefault(c => c.Type == Claims.Pesel)?.Value;
		if (userPesel is not null && pesel is null)
			pesel = userPesel;
		if(pesel is null)
			throw new BadHttpRequestException("No PESEL provided");
		var file = await mediator.Send(new GetOrderResultByPeselQuery(new GetOrderByPeselDto(pesel, orderNumber)));
		var filestream = new MemoryStream(file);
		return File(filestream, "application/pdf", $"{orderNumber}.pdf");
	}

	[HttpDelete("{orderNumber}")]
	public async Task<ActionResult> DeleteByOrderNumber([FromRoute] string orderNumber)
	{	
		logger.LogInformation("Delete Order endpoint invoked");
		await mediator.Send(new DeleteOrderCommand(orderNumber));
		return NoContent();
	}

	[HttpPut("{orderNumber}")]
	public async Task<ActionResult> Update([FromRoute] string orderNumber, [FromBody] UpdateOrderDto dto)
	{
		logger.LogInformation("Update Order endpoint invoked");
		await mediator.Send(new UpdateOrderCommand(orderNumber, dto));
		return Ok();
	}

	[HttpPatch("{orderNumber}")]
	public async Task<ActionResult> AddResults([FromRoute] string orderNumber, [FromBody] CreateOrderResultDto dto)
	{
		logger.LogInformation("Add OrderResults endpoint invoked");
		if (orderNumber != dto.OrderNumber)
			throw new BadHttpRequestException("Order Numbers do not match");
		await mediator.Send(new AddOrderResultsCommand(dto));
		return Ok();
	}
}