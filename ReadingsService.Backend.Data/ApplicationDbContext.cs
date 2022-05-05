using Microsoft.EntityFrameworkCore;
using ReadingsService.Backend.Core.Entities;
using System.Reflection;

namespace ReadingsService.Backend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<Observation> Observations { get; set; } = null!;

    public DbSet<Sequence> Sequences { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}