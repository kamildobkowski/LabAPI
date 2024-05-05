using LabAPI.Domain.Common;
using LabAPI.Domain.DomainEvents.Order;
using LabAPI.Domain.Enums;
using LabAPI.Domain.ValueObjects;

namespace LabAPI.Domain.Entities;

public sealed class Order : AggregateRoot
{
	public string OrderNumber { get; set; } = string.Empty;
	public PatientData PatientData { get; set; } = null!;
	public Dictionary<string, Dictionary<string, string>?> Results { get; set; } = null!;
	public OrderStatus Status { get; set; } = OrderStatus.Registered;
	public string? AcceptedById { get; set; }
	
	public void AddResults(Dictionary<string, Dictionary<string, string>?> results)
	{
		Results = results;
		CheckIfResultsAreReadyAndChangeStatus();
	}
	
	public void AcceptResults(string userId)
	{
		if (Status != OrderStatus.ResultsReady)
			throw new InvalidOperationException("Order is not ready for results acceptance.");
		Status = OrderStatus.ResultsAccepted;
		AcceptedById = userId;
		RaiseDomainEvent(new OrderResultsConfirmed(this));
	}

	private bool CheckIfResultsAreReadyAndChangeStatus()
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