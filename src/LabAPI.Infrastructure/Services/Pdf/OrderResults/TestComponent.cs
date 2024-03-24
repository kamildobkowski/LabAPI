using System.Text;
using LabAPI.Domain.ValueObjects;
using Microsoft.Extensions.Primitives;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace LabAPI.Infrastructure.Services.Pdf.OrderResults;

internal sealed class TestComponent : IComponent
{
	private readonly OrderResultDocumentModel.TestResult _testResult;

	public TestComponent(OrderResultDocumentModel.TestResult testResult)
	{
		_testResult = testResult;
	}
	public void Compose(IContainer container)
	{
		container.Table(table =>
		{
			
			table.ColumnsDefinition(r =>
			{
				r.RelativeColumn(); //name
				r.RelativeColumn(); //result
				r.ConstantColumn(10); //arrow
				r.RelativeColumn(); //unit
				r.RelativeColumn(); //normRange
			});
			table.Header(header =>
			{
				header.Cell().Element(CellStyle).Text("Nazwa");
				header.Cell().Element(CellStyle).AlignCenter().Text("Wynik");
				header.Cell().Element(CellStyle).Text("");
				header.Cell().Element(CellStyle).AlignRight().Text("Jednostka");
				header.Cell().Element(CellStyle).AlignRight().Text("Norma");

				static IContainer CellStyle(IContainer container)
					=> container
						.DefaultTextStyle(r
							=> r.Light().FontSize(10).FontColor(Colors.Grey.Darken2))
						.BorderTop(1)
						.BorderColor(Colors.Black);
			});
			foreach (var i in _testResult.Markers)
			{
				var name = i.Marker.Name;
				if (i.Marker.ShortName != i.Marker.Name)
					name = name + $" ({i.Marker.ShortName})";
				table.Cell().Text(name);
				table.Cell().AlignCenter().Text(i.Result);
				if (i.Norm is not null)
				{
					if (i.Norm == 1)
						table.Cell().AlignLeft().Text(t =>
						{
							t.Span("\u2191")
								.FontFamily(Fonts.Arial)
								.FontColor(Colors.Red.Medium);
						});
					else
						table.Cell().AlignLeft().Text(t =>
						{
							t.Span("\u2193")
								.FontFamily(Fonts.Arial)
								.FontColor(Colors.Red.Medium);
						});
				}
				else
				{
					table.Cell().AlignRight().Text(" ");
				}
				table.Cell().AlignRight().Text(i.Marker.Unit);

				var s = new StringBuilder();
				if (i.Marker.LowerNorm is not null)
					s.Append(i.Marker.LowerNorm);
				s.Append(" - ");
				if (i.Marker.HigherNorm is not null)
					s.Append(i.Marker.HigherNorm);
				table.Cell().AlignRight().Text(s.ToString());
			}
		});
	}
}