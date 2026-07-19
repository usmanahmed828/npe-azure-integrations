using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Shared.Entities;

namespace NPE.Infrastructure.Modules.Management.Shared.Configurations
{
    public class CityConfiguration :
        IEntityTypeConfiguration<City>
    {
        public void Configure(
            EntityTypeBuilder<City> builder)
        {
            builder.ToTable(
                "City",
                tb =>
                {
                    tb.HasTrigger(
                        "biSyncDeleteCityTrackingTrigger");

                    tb.HasTrigger(
                        "biSyncInsertCityTrackingTrigger");

                    tb.HasTrigger(
                        "biSyncUpdateCityTrackingTrigger");
                });

            builder.Property(
                x => x.CityCode)
                .ValueGeneratedNever();

            builder.Property(
                x => x.CreatedBy)
                .HasDefaultValue(
                    "Admin");

            builder.Property(
                x => x.CreatedDate)
                .HasDefaultValueSql(
                    "(getdate())");

            builder.Property(
                x => x.ModifiedBy)
                .HasDefaultValue(
                    "Admin");

            builder.Property(
                x => x.ModifiedDate)
                .HasDefaultValueSql(
                    "(getdate())");
        }
    }
}