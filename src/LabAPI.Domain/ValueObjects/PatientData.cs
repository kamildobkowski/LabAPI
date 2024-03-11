using LabAPI.Domain.Enums;

namespace LabAPI.Domain.ValueObjects;

public sealed record PatientData
{
	public string? Pesel { get; init; }
	public DateOnly DateOfBirth { get; init; }
	public EnSex? Sex { get; init; }
	public Address? Address { get; init; }

	public PatientData()
	{
		
	}
	public PatientData(string pesel, EnSex sex, Address? address)
	{
		Pesel = pesel;
		DateOfBirth = GetDateOnlyFromPesel(pesel);
		Sex = sex;
		Address = address;
	}

	public PatientData(DateOnly dateOfBirth, EnSex sex, Address? address)
	{
		DateOfBirth = dateOfBirth;
		Sex = sex;
		Address = address;
	}

	private static DateOnly GetDateOnlyFromPesel(string pesel)
	{
		var year = int.Parse(pesel[..1]);
		if (pesel[3] == '2' || pesel[3] == '3')
			year += 2000;
		else
			year += 1900;
		var month = int.Parse(pesel[2..3]);
		if (year >= 2000)
			month -= 20;
		var day = int.Parse(pesel[4..5]);
		return new DateOnly(year, month, day);
	}
}