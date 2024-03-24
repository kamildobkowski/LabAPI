using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace LabAPI.Infrastructure.Services.Pdf.OrderResults;

public partial class OrderResultsDocument
{
	private void ComposeHeader(IContainer container)
	{
		var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);
		
		container.Row(row =>
		{
			row.RelativeItem().Column(col =>
			{
				col.Item().Text($"Laboratorium");
				col.Item().Text($"Numer zlecenia: {model.OrderNumber}").Bold();
				col.Item().Text($"Data: {model.Date}");
			});
			row.RelativeItem()
				.AlignRight()
				.Column(col =>
			{
				col.Item().AlignRight().Text("Pacjent:").Bold();
				col.Item().AlignRight().Text($"{model.PatientData.Name} {model.PatientData.Surname}").Bold();
				if (model.PatientData.Address is not null)
				{
					col.Item().AlignRight().Text($"{model.PatientData.Address.Street} {model.PatientData.Address.Number}");
					col.Item().AlignRight().Text($"{model.PatientData.Address.PostalCode} {model.PatientData.Address.City}");
				}
				col.Item().AlignRight().Text($"PESEL: {model.PatientData.Pesel ?? "brak"}").Bold();
				col.Item().AlignRight().Text($"Data urodzenia: {model.PatientData.DateOfBirth}").Bold();
				var sexText = model.PatientData.Sex == "f" ? "Kobieta" : "Mężczyzna";
				col.Item().AlignRight().Text($"Płeć: {sexText}").Bold();
				
			});
			
		});
	}
}