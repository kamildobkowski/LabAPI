using System.Configuration;
using System.Data;
using LabAPI.Domain.Common;
using LabAPI.Domain.Entities;
using LabAPI.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LabAPI.Infrastructure.Persistence;

public class LabDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public List<IDomainEvent> DomainEvents { get; } = [];
    
    public LabDbContext(DbContextOptions<LabDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseCosmos(
            Environment.GetEnvironmentVariable("AZURECOSMOSDB_CONNECTIONSTRING") ??
            throw new ConfigurationErrorsException("No Database Key provided"), "LabApi");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Order>()
            .ToContainer("Orders")
            .HasNoDiscriminator()
            .HasPartitionKey(r => r.OrderNumber);
        modelBuilder.Entity<Order>().Property(r => r.Id).ToJsonProperty("id");
        modelBuilder
            .Entity<Test>()
            .ToContainer("Tests")
            .HasNoDiscriminator()
            .HasPartitionKey(r => r.ShortName);
        modelBuilder.Entity<Test>()
            .Property(r => r.Id)
            .ToJsonProperty("id");
        modelBuilder
            .Entity<Customer>()
            .ToContainer("Customers")
            .HasNoDiscriminator()
            .HasPartitionKey(r => r.Id);
        modelBuilder.Entity<Customer>()
            .Property(r => r.Id)
            .ToJsonProperty("id");
        modelBuilder
            .Entity<Worker>()
            .ToContainer("Workers")
            .HasNoDiscriminator()
            .HasPartitionKey(r => r.Id);
        modelBuilder.Entity<Worker>()
            .Property(r => r.Id)
            .ToJsonProperty("id");
    }
}