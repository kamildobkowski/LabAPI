using LabAPI.Application.Common.Interfaces;
using LabAPI.Application.Features.Orders.Dtos;
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
		entity!.Results = request.Dto.Results;
		if (entity.CheckIfResultsAreReadyAndChangeStatus())
		{
			var model = new OrderResultDocumentModel
			{
				OrderNumber = entity.OrderNumber,
				Date = entity.CreatedAt,
				PatientData = entity.PatientData,
				TestResults = []
			};
			foreach (var i in entity.Results)
			{
				var test = await testRepository.GetAsync(r => r.ShortName == i.Key);
				var testResult = new OrderResultDocumentModel.TestResult
				{
					Name = test!.Name,
					ShortName = test.ShortName,
					Markers = []
				};
				foreach (var j in i.Value!)
				{
					var markerResult =
						new OrderResultDocumentModel.MarkerResult(
							test.Markers.FirstOrDefault(r => r.ShortName == j.Key)!, j.Value);
					testResult.Markers.Add(markerResult);
				}
				model.TestResults.Add(testResult);
			}
			_ = pdfService.CreateOrderPdf(entity, model);
		}
		repository.UpdateAsync(entity);
		await repository.SaveChangesAsync();
	}
} 