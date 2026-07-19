using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Centers;

namespace NPE.Infrastructure.Modules.Management.Center.Configurations
{
    public class CenterPaymentConfiguration :
        IEntityTypeConfiguration<CenterPayment>
    {
        public void Configure(
            EntityTypeBuilder<CenterPayment> builder)
        {
            builder.ToTable(
                "CenterPayment",
                tb =>
                {
                    tb.HasTrigger(
                        "UpdateCenterBalance");

                    tb.HasTrigger(
                        "biSyncDeleteCenterPaymentTrackingTrigger");

                    tb.HasTrigger(
                        "biSyncInsertCenterPaymentTrackingTrigger");

                    tb.HasTrigger(
                        "biSyncUpdateCenterPaymentTrackingTrigger");
                });

            builder.Property(
                x => x.Id)
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
                x => x.ReceivedBy)
                .HasDefaultValue(
                    "Admin");

            builder.Property(
                x => x.ReceivedDate)
                .HasDefaultValueSql(
                    "(getdate())");

            builder
    .HasOne(
        x => x.Center)
    .WithMany(
        x => x.CenterPayments)
    .HasForeignKey(
        x => x.CenterId)
    .OnDelete(
        DeleteBehavior.Restrict);
        }
    }
}