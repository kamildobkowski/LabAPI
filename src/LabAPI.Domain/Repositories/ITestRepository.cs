using LabAPI.Domain.Common;
using LabAPI.Domain.Entities;

namespace LabAPI.Domain.Repositories;

public interface ITestRepository : IRepository<Test>, IPagination<Test>
{
	
}