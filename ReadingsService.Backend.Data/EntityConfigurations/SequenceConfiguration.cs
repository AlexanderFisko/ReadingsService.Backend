using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingsService.Backend.Core.Entities;

namespace ReadingsService.Backend.Data.EntityConfigurations;

internal class SequenceConfiguration : IEntityTypeConfiguration<Sequence>
{
    public void Configure(EntityTypeBuilder<Sequence> builder) => builder.HasMany(s => s.Observations).WithOne(o => o.Sequence).OnDelete(DeleteBehavior.Cascade);
}