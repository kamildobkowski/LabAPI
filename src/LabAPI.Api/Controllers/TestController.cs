using LabAPI.Application.Dtos.Tests;
using LabAPI.Application.Features.Tests.Commands;
using LabAPI.Application.Features.Tests.Queries;
using LabAPI.Domain.Common;
using LabAPI.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Api.Controllers;

[ApiController]
[Route("api/tests")]
[Authorize(Policy = "IsLabWorker")]
public class TestController(IMediator mediator, ILogger<TestController> logger) : ControllerBase
{
	[HttpPost]
	public async Task<ActionResult> CreateTest([FromBody] CreateTestDto dto)
	{
		logger.LogInformation("Create Test endpoint invoked");
		await mediator.Send(new CreateTestCommand(dto));
		return Created();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<TestDto>> Get([FromRoute] string id)
	{
		logger.LogInformation("Get Test endpoint invoked");
		var dto = await mediator.Send(new GetTestQuery(id));
		return Ok(dto);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete([FromRoute] string id)
	{
		logger.LogInformation("Delete Test endpoint invoked");
		await mediator.Send(new DeleteTestCommand(id));
		return NoContent();
	}

	[HttpPut("{id}")]
	public async Task<ActionResult> Update([FromRoute] string id, [FromBody] UpdateTestDto dto)
	{
		logger.LogInformation("Update Test endpoint invoked");
		await mediator.Send(new UpdateTestCommand(id, dto));
		return Ok();
	}

	[HttpGet]
	public async Task<ActionResult<PagedList<TestDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20,
		[FromQuery] string? filter = null, [FromQuery] string? orderBy = null, 
		[FromQuery] bool asc = true)
	{
		logger.LogInformation("Get Tests Page endpoint invoked");
		var pagedList = await mediator.Send(new GetAllTestsQuery(page, pageSize, filter, orderBy, asc));
		return Ok(pagedList);
	}
}