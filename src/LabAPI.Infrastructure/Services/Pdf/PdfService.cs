using LabAPI.Application.Common.Interfaces;
using LabAPI.Domain.ValueObjects;
using LabAPI.Infrastructure.Services.Pdf.OrderResults;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace LabAPI.Infrastructure.Services.Pdf;

public sealed class PdfService(IPdfFileRepository pdfFileRepository) : IPdfService
{
	public void CreateOrderPdf(OrderResultDocumentModel model)
	{
		QuestPDF.Settings.License = LicenseType.Community;
		var doc = new OrderResultsDocument(model);
		var document = doc.GeneratePdf();
		File.WriteAllBytes("dupa.pdf",document);
		pdfFileRepository.UploadFile(document, $"Order_{model.OrderNumber}.pdf");
	}
	
}