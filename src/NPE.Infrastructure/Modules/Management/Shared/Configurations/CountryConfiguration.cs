using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Shared.Entities;

namespace NPE.Infrastructure.Modules.Management.Shared.Configurations
{
    public class CountryConfiguration :
        IEntityTypeConfiguration<Country>
    {
        public void Configure(
            EntityTypeBuilder<Country> builder)
        {
            builder.ToTable(
                "Country",
                tb =>
                {
                    tb.HasTrigger(
                        "biSyncDeleteCountryTrackingTrigger");

                    tb.HasTrigger(
                        "biSyncInsertCountryTrackingTrigger");

                    tb.HasTrigger(
                        "biSyncUpdateCountryTrackingTrigger");
                });

            builder.Property(
                x => x.CountryCode)
                .ValueGeneratedNever();

            builder.Property(
                x => x.CreatedDate)
                .HasDefaultValueSql(
                    "(getdate())");

            builder.Property(
                x => x.ModifiedDate)
                .HasDefaultValueSql(
                    "(getdate())");
        }
    }
}