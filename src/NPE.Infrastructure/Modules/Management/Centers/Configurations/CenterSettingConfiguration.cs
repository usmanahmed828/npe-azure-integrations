using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Centers;

namespace NPE.Infrastructure.Modules.Management.Centers.Configurations
{
    public class CenterSettingConfiguration : IEntityTypeConfiguration<CenterSetting>
    {
        public void Configure(EntityTypeBuilder<CenterSetting> builder)
        {
            builder.ToTable("CenterSetting");

            builder.HasKey(x => x.CenterId);

            builder.Property(x => x.CenterId)
                .ValueGeneratedNever();

            builder.Property(x => x.DestinationLocation)
                .IsRequired();

            builder.Property(x => x.DefaultStatus)
                .HasDefaultValue(1);

            builder.Property(x => x.RegionId)
                .IsRequired(false);

            builder.Property(x => x.TransportTime)
                .IsRequired(false);

            builder.Property(x => x.IsCreditFeatureEnabled)
                .IsRequired(false);

            builder.Property(x => x.CreditWarningLimit)
                .HasColumnType("decimal(18,2)")
                .IsRequired(false);
        }
    }
}
