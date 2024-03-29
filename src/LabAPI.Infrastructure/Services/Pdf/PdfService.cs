using LabAPI.Application.Common.Interfaces;
using LabAPI.Application.Features.Accounts.Repository;
using LabAPI.Application.Features.Orders.Repository;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Enums;
using LabAPI.Domain.ValueObjects;
using LabAPI.Infrastructure.Services.Pdf.OrderResults;
using Microsoft.Extensions.Logging;
using QuestPDF.Fluent;

namespace LabAPI.Infrastructure.Services.Pdf;

public sealed class PdfService(IPdfFileRepository pdfFileRepository, IOrderRepository orderRepository, 
	IEmailService emailService, ICustomerRepository customerRepository, ILogger<PdfService> logger) : IPdfService
{
	public async Task CreateOrderPdf(Order order, OrderResultDocumentModel model)
	{
		try
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
		catch(Exception e)
		{
			logger.LogError("Error while creating pdf file" + e.Message);
			throw;
		}
		
	}
	
	public async Task<byte[]> GetOrderPdf(Order order)
	{
		var pdf = await pdfFileRepository.GetFile($"Order_{order.OrderNumber}.pdf");
		return pdf;
	}
}