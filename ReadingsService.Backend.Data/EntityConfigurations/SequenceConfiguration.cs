using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingsService.Backend.Core.Entities;

namespace ReadingsService.Backend.Data.EntityConfigurations;

internal class SequenceConfiguration : IEntityTypeConfiguration<SequenceEntity>
{
    public void Configure(EntityTypeBuilder<SequenceEntity> builder) => builder.HasMany(s => s.Observations).WithOne(o => o.Sequence).OnDelete(DeleteBehavior.Cascade);
}