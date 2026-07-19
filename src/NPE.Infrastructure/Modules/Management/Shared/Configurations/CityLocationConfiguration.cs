using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Shared.Entities;

namespace NPE.Infrastructure.Modules.Management.Shared.Configurations
{
    public class CityLocationConfiguration :
        IEntityTypeConfiguration<CityLocation>
    {
        public void Configure(
            EntityTypeBuilder<CityLocation> builder)
        {
            builder.HasKey(
                x => x.Code)
                .HasName(
                    "PK_Location");

            builder.Property(
                x => x.Code)
                .ValueGeneratedNever();
        }
    }
}