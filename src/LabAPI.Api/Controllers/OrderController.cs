using LabAPI.Application.Dtos.Orders;
using LabAPI.Application.Features.Orders.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Api.Controllers;

[ApiController]
[Route("api/order")]
public sealed class OrderController(IMediator mediator) : ControllerBase
{
	[HttpPost]
	public async Task<ActionResult> CreateOrder([FromBody] CreateOrderDto dto)
	{
		var id = await mediator.Send(new CreateOrderCommand(dto));
		return Created($"api/order/{id}", null);
	}
}