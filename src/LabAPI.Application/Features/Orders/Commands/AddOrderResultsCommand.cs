using LabAPI.Application.Common.Interfaces;
using LabAPI.Application.Features.Orders.Dtos;
using LabAPI.Domain.Exceptions;
using LabAPI.Domain.Repositories;
using LabAPI.Domain.ValueObjects;
using MediatR;

namespace LabAPI.Application.Features.Orders.Commands;

public sealed record AddOrderResultsCommand(CreateOrderResultDto Dto) : IRequest;

internal sealed class AddOrderResultsCommandHandler(IOrderRepository repository, IPdfService pdfService, ITestRepository testRepository) 
	: IRequestHandler<AddOrderResultsCommand>
{
	public async Task Handle(AddOrderResultsCommand request, CancellationToken cancellationToken)
	{
		var entity = await repository.GetAsync(r => r.OrderNumber == request.Dto.OrderNumber);
		if (entity is null)
			throw new NotFoundException();
		entity.AddResults(request.Dto.Results);
		repository.UpdateAsync(entity);
		await repository.SaveChangesAsync();
	}
} 