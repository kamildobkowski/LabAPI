using LabAPI.Application.Interfaces;
using LabAPI.Domain.Entities;
using LabAPI.Infrastructure.Persistence;
using Microsoft.Azure.Cosmos;

namespace LabAPI.Infrastructure.Repositories;

internal sealed class TestRepository(CosmosDbContext dbContext) 
	: GenericRepository<Test>(dbContext), ITestRepository
{
	
}