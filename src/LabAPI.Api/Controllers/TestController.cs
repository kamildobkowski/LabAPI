using LabAPI.Application.Dtos.Tests;
using LabAPI.Application.Features.Tests.Commands;
using LabAPI.Application.Features.Tests.Queries;
using LabAPI.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Api.Controllers;

[ApiController]
[Route("api/tests")]
public class TestController(IMediator mediator) : ControllerBase
{
	[HttpPost]
	public async Task<ActionResult> CreateTest([FromBody] CreateTestDto dto)
	{
		await mediator.Send(new CreateTestCommand(dto));
		return Created();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<TestDto>> Get([FromRoute] string id)
	{
		var dto = await mediator.Send(new GetTestQuery(id));
		return Ok(dto);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete([FromRoute] string id)
	{
		await mediator.Send(new DeleteTestCommand(id));
		return NoContent();
	}

	[HttpPut("{id}")]
	public async Task<ActionResult> Update([FromRoute] string id, [FromBody] CreateTestDto dto)
	{
		await mediator.Send(new UpdateTestCommand(id, dto));
		return Ok();
	}

	[HttpGet]
	public async Task<ActionResult<PagedList<TestDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20,
		[FromQuery] string? filterBy = null, [FromQuery] string? filter = null, [FromQuery] string? orderBy = null, 
		[FromQuery] bool asc = true)
	{
		var pagedList = await mediator.Send(new GetAllTestsQuery(page, pageSize, filterBy, filter, orderBy, asc));
		return Ok(pagedList);
	}
}