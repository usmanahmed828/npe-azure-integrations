using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Centers;

namespace NPE.Infrastructure.Modules.Management.Center.Configurations
{
    public class CenterCreditHistoryConfiguration :
        IEntityTypeConfiguration<CenterCreditHistory>
    {
        public void Configure(
            EntityTypeBuilder<CenterCreditHistory> builder)
        {
            builder
                .HasKey(
                    x => x.CaseId)
                .HasName(
                    "PK_CenterCreditUsageHistory");

            builder.Property(
                x => x.CaseId)
                .ValueGeneratedNever();

            builder
    .HasOne(
        x => x.Center)
    .WithMany(
        x => x.CenterCreditHistories)
    .HasForeignKey(
        x => x.CenterId)
    .OnDelete(
        DeleteBehavior.Restrict);
        }
    }
}