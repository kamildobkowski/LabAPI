using AutoMapper;
using LabAPI.Application.Features.Orders.Dtos;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LabAPI.Application.Features.Orders.Commands;

public sealed record UpdateOrderCommand (string OrderNumber, UpdateOrderDto Dto) : IRequest;

internal sealed class UpdateOrderCommandHandler(IOrderRepository repository, IMapper mapper)
	: IRequestHandler<UpdateOrderCommand>
{
	public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
	{
		if (request.Dto.OrderNumber != request.OrderNumber)
			throw new BadHttpRequestException("Invalid Order Number");
		var entity = mapper.Map<Order>(request.Dto);
		repository.UpdateAsync(entity);
		await repository.SaveChangesAsync();
	}
} 