using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Shared.Entities;

namespace NPE.Infrastructure.Modules.Management.Shared.Configurations
{
    public class ClinicalDetailConfiguration :
        IEntityTypeConfiguration<ClinicalDetail>
    {
        public void Configure(
            EntityTypeBuilder<ClinicalDetail> builder)
        {
            builder.ToTable(
                "ClinicalDetail",
                tb =>
                {
                    tb.HasTrigger(
                        "biSyncDeleteClinicalDetailTrackingTrigger");

                    tb.HasTrigger(
                        "biSyncInsertClinicalDetailTrackingTrigger");

                    tb.HasTrigger(
                        "biSyncUpdateClinicalDetailTrackingTrigger");
                });

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

            builder.Property(
                x => x.Status)
                .HasDefaultValue(
                    false);
        }
    }
}