using LabAPI.Domain.Common;

namespace LabAPI.Domain.ValueObjects;

public sealed class Address(string number, string street, string city, string postalCode)
{
	public string Street { get; init; } = street;
	public string Number { get; init; } = number;
	public string City { get; init; } = city;
	public string PostalCode { get; init; } = postalCode;
}