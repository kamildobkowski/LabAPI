using AutoMapper;
using LabAPI.Application.Features.OrderResults.Dtos;
using LabAPI.Application.Features.OrderResults.Repository;
using LabAPI.Domain.Entities;
using MediatR;

namespace LabAPI.Application.Features.OrderResults.Commands;

public sealed record CreateOrderResultCommand(CreateOrderResultDto Dto) : IRequest;

internal sealed class CreateOrderResultCommandHandler(IOrderResultRepository repository, IMapper mapper)
	: IRequestHandler<CreateOrderResultCommand>
{
	public async Task Handle(CreateOrderResultCommand request, CancellationToken cancellationToken)
	{
		var entity = mapper.Map<OrderResult>(request.Dto);
		await repository.Create(entity);
	}
} 