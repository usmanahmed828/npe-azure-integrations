using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Reference.Entities;

namespace NPE.Infrastructure.Modules.Management.Reference.Configurations
{
    public class ReferencePaymentConfiguration :
        IEntityTypeConfiguration<ReferencePayment>
    {
        public void Configure(
            EntityTypeBuilder<ReferencePayment> builder)
        {
            builder.ToTable(
                "ReferencePayment",
                tb =>
                {
                    tb.HasComment(
                        "Payments made by Reference will be stored in it.");

                    tb.HasTrigger(
                        "UpdateReferenceBalance");
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
                x => x.Discount)
                .HasDefaultValue(
                    0m);

            builder.Property(
                x => x.ReceivedBy)
                .HasDefaultValue(
                    "Admin");

            builder.Property(
                x => x.ReceivedDate)
                .HasDefaultValueSql(
                    "(getdate())");
        }
    }
}