using LabAPI.Domain.Entities;
using LabAPI.Domain.ValueObjects;

namespace LabAPI.Application.Common.Interfaces;

public interface IPdfService
{
	Task CreateOrderPdf(Order order, OrderResultDocumentModel model);
}