using AutoMapper;
using LabAPI.Application.Common.Interfaces;
using LabAPI.Application.Features.Orders.Dtos;
using LabAPI.Application.Features.Orders.Repository;
using LabAPI.Domain.Exceptions;
using MediatR;

namespace LabAPI.Application.Features.Orders.Queries;

public sealed record GetOrderResultByPeselQuery(GetOrderByPeselDto Dto) : IRequest<byte[]>;

internal sealed class GetOrderByPeselQueryHandler(IOrderRepository orderRepository, IPdfService pdfService) 
	: IRequestHandler<GetOrderResultByPeselQuery, byte[]>
{
	public async Task<byte[]> Handle(GetOrderResultByPeselQuery request, CancellationToken cancellationToken)
	{
		var order = await orderRepository.GetAsync(r => r.OrderNumber == request.Dto.OrderNumber && r.PatientData.Pesel == request.Dto.Pesel);
		if (order is null)
			throw new NotFoundException("Order not found");
		var file = await pdfService.GetOrderPdf(order);
		return file;
	}
}  