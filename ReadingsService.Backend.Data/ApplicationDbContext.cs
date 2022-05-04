using Microsoft.EntityFrameworkCore;
using ReadingsService.Backend.Core.Entities;
using System.Reflection;

namespace ReadingsService.Backend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<ObservationEntity> Observations { get; set; } = null!;

    public DbSet<SequenceEntity> Sequences { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}