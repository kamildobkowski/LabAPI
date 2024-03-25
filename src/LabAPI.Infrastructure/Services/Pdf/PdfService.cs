using LabAPI.Application.Common.Interfaces;
using LabAPI.Application.Features.Accounts.Repository;
using LabAPI.Application.Features.Orders.Repository;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Enums;
using LabAPI.Domain.ValueObjects;
using LabAPI.Infrastructure.Services.Pdf.OrderResults;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace LabAPI.Infrastructure.Services.Pdf;

public sealed class PdfService(IPdfFileRepository pdfFileRepository, IOrderRepository orderRepository, 
	IEmailService emailService, ICustomerRepository customerRepository) : IPdfService
{
	public async Task CreateOrderPdf(Order order, OrderResultDocumentModel model)
	{
		var doc = new OrderResultsDocument(model);
		var document = doc.GeneratePdf();
		await pdfFileRepository.UploadFile(document, $"Order_{model.OrderNumber}.pdf");
		order.Status = OrderStatus.PdfReady;
		await orderRepository.UpdateAsync(order);
		
		var customer = await customerRepository.GetAsync(r => r.Pesel == order.PatientData.Pesel);
		if (customer is not null)
		{
			_ = emailService.SendResultReadyEmail(customer.Email, customer.Name, customer.Surname);
		}
	}
	
	public async Task<byte[]> GetOrderPdf(Order order)
	{
		var pdf = await pdfFileRepository.GetFile($"Order_{order.OrderNumber}.pdf");
		return pdf;
	}
}