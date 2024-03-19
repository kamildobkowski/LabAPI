using LabAPI.Application.Common.Interfaces;
using LabAPI.Domain.Entities;

namespace LabAPI.Application.Features.Tests.Repository;

public interface ITestRepository : IRepository<Test>, IPagination<Test>
{
	
}