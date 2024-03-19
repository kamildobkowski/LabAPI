using LabAPI.Domain.Common;
using LabAPI.Domain.Enums;
using LabAPI.Domain.ValueObjects;

namespace LabAPI.Domain.Entities;

public sealed class Order : BaseEntity
{
	public string OrderNumber { get; set; } = string.Empty;
	public PatientData PatientData { get; set; } = null!;
	public Dictionary<string, Dictionary<string, string>?> Results { get; set; } = null!;
	public OrderStatus Status { get; set; } = OrderStatus.Registered;

	public bool CheckIfResultsAreReadyAndChangeStatus()
	{
		foreach (var i in Results)
		{
			if (i.Value is null)
				return false;
			foreach (var q in i.Value)
			{
				if (i.Value is null)
					return false;
			}
		}

		Status = OrderStatus.ResultsReady;
		return true;
	}
}