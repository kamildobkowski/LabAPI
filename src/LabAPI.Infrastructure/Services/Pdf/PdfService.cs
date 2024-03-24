using LabAPI.Application.Common.Interfaces;
using LabAPI.Application.Features.Orders.Repository;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Enums;
using LabAPI.Domain.ValueObjects;
using LabAPI.Infrastructure.Services.Pdf.OrderResults;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace LabAPI.Infrastructure.Services.Pdf;

public sealed class PdfService(IPdfFileRepository pdfFileRepository, IOrderRepository orderRepository) : IPdfService
{
	public async Task CreateOrderPdf(Order order, OrderResultDocumentModel model)
	{
		QuestPDF.Settings.License = LicenseType.Community;
		var doc = new OrderResultsDocument(model);
		var document = doc.GeneratePdf();
		await pdfFileRepository.UploadFile(document, $"Order_{model.OrderNumber}.pdf");
		order.Status = OrderStatus.PdfReady;
		await orderRepository.UpdateAsync(order);
	}
	
}