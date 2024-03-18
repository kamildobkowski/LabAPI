namespace LabAPI.Domain.ValueObjects;

public sealed class Address(string Number, string Street, string City, string PostalCode)
{
	public string Street { get; set; } = string.Empty;
	public string Number { get; set; } = string.Empty;
	public string City { get; set; } = string.Empty;
	public string PostalCode { get; set; } = string.Empty;
}