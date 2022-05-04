using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingsService.Backend.Core.Entities;

namespace ReadingsService.Backend.Data.EntityConfigurations;

internal class SequenceConfiguration : IEntityTypeConfiguration<SequenceEntity>
{
    public void Configure(EntityTypeBuilder<SequenceEntity> builder)
    {
        // NOP
    }
}