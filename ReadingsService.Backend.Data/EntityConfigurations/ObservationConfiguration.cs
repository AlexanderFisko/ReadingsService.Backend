using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingsService.Backend.Core.Entities;

namespace ReadingsService.Backend.Data.EntityConfigurations;

internal class ObservationConfiguration : IEntityTypeConfiguration<ObservationEntity>
{
    public void Configure(EntityTypeBuilder<ObservationEntity> builder)
    {
        // NOP
    }
}