using System.Data;
using LabAPI.Domain.Entities;
using LabAPI.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LabAPI.Infrastructure.Persistence;

public sealed class CosmosDbContext(IConfiguration configuration) : DbContext
{
	public DbSet<Order> Orders { get; set; } = null!;
	public DbSet<Test> Tests { get; set; } = null!;
	
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder
			.UseCosmos(configuration.GetConnectionString("AzureCosmosDb") 
			           ?? throw new DataException("No Database Key"), 
				"LabApi");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Test>()
			.HasPartitionKey(r => r.Id)
			.ToContainer("Tests")
			.HasNoDiscriminator()
			.HasKey(r => r.Id);
		modelBuilder.Entity<Test>()
			.OwnsMany(r => r.Markers);
		modelBuilder.Entity<Order>()
			.HasPartitionKey(r => r.OrderNumber)
			.ToContainer("Orders")
			.HasNoDiscriminator()
			.HasKey(r => r.OrderNumber);
		modelBuilder.Entity<Order>()
			.OwnsOne(r => r.PatientData).OwnsOne(r => r.Address);
	}
}