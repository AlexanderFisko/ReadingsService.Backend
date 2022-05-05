using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingsService.Backend.Core.Entities;

namespace ReadingsService.Backend.Data.EntityConfigurations;

internal class ObservationConfiguration : IEntityTypeConfiguration<Observation>
{
    public void Configure(EntityTypeBuilder<Observation> builder)
    {
        // NOP
    }
}