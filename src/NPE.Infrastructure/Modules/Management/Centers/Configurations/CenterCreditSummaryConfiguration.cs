using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Centers;

namespace NPE.Infrastructure.Modules.Management.Center.Configurations
{
    public class CenterCreditSummaryConfiguration :
        IEntityTypeConfiguration<CenterCreditSummary>
    {
        public void Configure(
            EntityTypeBuilder<CenterCreditSummary> builder)
        {
            builder.Property(
                x => x.CenterId)
                .ValueGeneratedNever();

            builder
    .HasOne(
        x => x.Center)
    .WithOne(
        x => x.CenterCreditSummary)
    .HasForeignKey<CenterCreditSummary>(
        x => x.CenterId)
    .OnDelete(
        DeleteBehavior.Restrict);
        }
    }
}