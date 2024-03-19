using LabAPI.Application.Common.Interfaces;
using LabAPI.Domain.Entities;

namespace LabAPI.Application.Features.OrderResults.Repository;

public interface IOrderResultRepository : IRepository<OrderResult>, IPagination<OrderResult>
{
	
}