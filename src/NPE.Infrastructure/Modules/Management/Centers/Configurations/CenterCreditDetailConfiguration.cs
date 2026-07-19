using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Centers;

namespace NPE.Infrastructure.Modules.Management.Center.Configurations
{
    public class CenterCreditDetailConfiguration :
        IEntityTypeConfiguration<CenterCreditDetail>
    {
        public void Configure(
            EntityTypeBuilder<CenterCreditDetail> builder)
        {
            builder.ToTable(
                "CenterCreditDetail",
                tb =>
                {
                    tb.HasComment(
                        "Payment taken from patient for test(s) will be stored.");
                });

            builder.Property(x => x.CenterId)
                .HasDefaultValue(0);

            builder.Property(x => x.CreatedBy)
                .HasDefaultValue("Admin");

            builder.Property(x => x.CreatedDate)
                .HasDefaultValueSql("(getdate())");

            builder.Property(x => x.Dated)
                .HasDefaultValueSql("(getdate())");

            builder.Property(x => x.ModifiedBy)
                .HasDefaultValue("Admin");

            builder.Property(x => x.ModifiedDate)
                .HasDefaultValueSql("(getdate())");

            builder
    .HasOne(
        x => x.Center)
    .WithMany(
        x => x.CenterCreditDetails)
    .HasForeignKey(
        x => x.CenterId)
    .OnDelete(
        DeleteBehavior.Restrict);
        }
    }
}