using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Centers;

namespace NPE.Infrastructure.Modules.Management.Center.Configurations
{
    public class MaxLabNoConfiguration : IEntityTypeConfiguration<MaxLabNo>
    {
        public void Configure(EntityTypeBuilder<MaxLabNo> builder)
        {
            builder.ToTable("MaxLabNos");

            // Composite PK: CenterCode + Dated
            builder.HasKey(x => new
            {
                x.CenterCode,
                x.Dated
            });

            builder.Property(x => x.CenterCode)
                .ValueGeneratedNever();

            builder.Property(x => x.Dated)
                .HasColumnType("smalldatetime")
                .IsRequired();

            builder.Property(x => x.NextLabNo)
                .IsRequired();
        }
    }
}
