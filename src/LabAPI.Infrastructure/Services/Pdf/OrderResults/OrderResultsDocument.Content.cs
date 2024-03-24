using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace LabAPI.Infrastructure.Services.Pdf.OrderResults;

public partial class OrderResultsDocument
{
	private void ComposeContent(IContainer container)
	{
			container.Column(s =>
			{
				s.Item().PaddingBottom(20).Row(row =>
				{
					row.RelativeItem().PaddingTop(10).PaddingBottom(0)
						.Background(Colors.Grey.Lighten2).AlignCenter()
						
						.Text("Sprawozdanie z badań laboratoryjnych").Bold()
						.FontSize(17);
				});
				foreach (var i in model.TestResults)
				{
					s.Item().Column(col =>
					{
						col.Item().Background(Colors.Grey.Lighten3).Row(r =>
						{
							var text = i.Name == i.ShortName ? $"{i.ShortName}" : $"{i.Name} ({i.ShortName})";
							r.RelativeItem().Text(text).Bold().FontSize(13);
						});
						col.Item().Padding(1).Component(new TestComponent(i));
					});
				}
			});
			
	}

	private void ComposeTest(IContainer container)
	{
		
	}

	private void ComposeTable(IContainer container)
	{
		
	}
}