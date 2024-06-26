using LabAPI.Domain.Common;
using LabAPI.Domain.Entities;

namespace LabAPI.Domain.Repositories;

public interface IWorkerRepository : IRepository<Worker>, IPagination<Worker>
{
	
}