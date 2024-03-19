using LabAPI.Domain.Common;


namespace LabAPI.Domain.ValueObjects;

public sealed class PatientData
{
	public string? Pesel { get; init; }
	public DateOnly DateOfBirth { get; init; }
	public string? Sex { get; init; }
	public Address? Address { get; init; }

	public PatientData()
	{
		
	}
	public PatientData(string? pesel, DateOnly? dateOfBirth, string? sex, Address? address)
	{
		if (pesel is not null)
			Pesel = pesel;
		DateOfBirth = dateOfBirth ?? GetDateOnlyFromPesel(pesel!);
		Sex = sex;
		Address = address;
	}

	private static DateOnly GetDateOnlyFromPesel(string pesel)
	{
		var year = int.Parse(pesel[..2]);
		if (pesel[2] == '2' || pesel[2] == '3')
			year += 2000;
		else
			year += 1900;
		var month = int.Parse(pesel[2..4]);
		if (year >= 2000)
			month -= 20;
		var day = int.Parse(pesel[4..6]);
		return new DateOnly(year, month, day);
	}
}