using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Shared.Entities;

namespace NPE.Infrastructure.Modules.Management.Shared.Configurations
{
    public class RegionConfiguration :
        IEntityTypeConfiguration<Region>
    {
        public void Configure(
            EntityTypeBuilder<Region> builder)
        {
            builder.Property(
                x => x.Id)
                .ValueGeneratedNever();
        }
    }
}