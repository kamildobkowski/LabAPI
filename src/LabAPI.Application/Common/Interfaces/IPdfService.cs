using LabAPI.Domain.ValueObjects;

namespace LabAPI.Application.Common.Interfaces;

public interface IPdfService
{
	void CreateOrderPdf(OrderResultDocumentModel model);
}