using LabAPI.Domain.Entities;

namespace LabAPI.Application.Interfaces;

public interface ITestRepository : IRepository<Test>, IPagination<Test>
{
	
}