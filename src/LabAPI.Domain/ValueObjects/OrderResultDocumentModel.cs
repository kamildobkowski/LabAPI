namespace LabAPI.Domain.ValueObjects;

public sealed class OrderResultDocumentModel
{
	public string OrderNumber { get; set; } = string.Empty;
	public PatientData PatientData { get; set; } = null!;
	public List<TestResult> TestResults { get; set; } = null!;
	public DateTime Date { get; set; }
	
	public class TestResult
	{
		public string ShortName { get; set; } = null!;
		public string Name { get; set; } = null!;
		public List<MarkerResult> Markers { get; set; } = null!;
	}

	public class MarkerResult
	{
		public Marker Marker { get; set; }
		public string Result { get; set; }
		public int? Norm { get; set; }

		public MarkerResult(Marker marker, string result)
		{
			Marker = marker;
			Result = result;
			Norm = null;
			if (marker.HigherNorm is null
			    || marker.LowerNorm is null
			    || !double.TryParse(result, out var doubleResult)) return;
			if (doubleResult > (double)marker.HigherNorm)
			{
				Norm = 1;
			}
			else if (doubleResult < (double)marker.LowerNorm)
			{
				Norm = -1;
			}
		}
		
	}
}