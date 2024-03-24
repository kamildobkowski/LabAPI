using LabAPI.Domain.ValueObjects;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace LabAPI.Infrastructure.Services.Pdf.OrderResults;

public partial class OrderResultsDocument(OrderResultDocumentModel model) : IDocument
{
	public void Compose(IDocumentContainer container)
	{
		container
			.Page(page =>
			{
				page.DefaultTextStyle(r => r.FontSize(10));
				page.Header()
					.Padding(30)
					.Element(ComposeHeader);
				page.Content()
					.Padding(30)
					.Element(ComposeContent);
				page.Footer()
					.Height(50);
			});
	}
	
	

	
}