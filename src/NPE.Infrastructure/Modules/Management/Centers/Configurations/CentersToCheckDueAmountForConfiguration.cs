using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NPE.Infrastructure.Modules.Management.Centers;

namespace NPE.Infrastructure.Modules.Management.Center.Configurations
{
    public class CentersToCheckDueAmountForConfiguration
        : IEntityTypeConfiguration<CentersToCheckDueAmountFor>
    {
        public void Configure(
            EntityTypeBuilder<CentersToCheckDueAmountFor> builder)
        {
            builder.ToTable(
                "CentersToCheckDueAmountFor");

            builder.HasKey(x =>
                x.CenterId);

            builder.HasOne(x =>
                x.Center)

                .WithOne(x =>
                    x.DueAmountSetting)

                .HasForeignKey<
                    CentersToCheckDueAmountFor>(
                        x => x.CenterId)

                .OnDelete(
                    DeleteBehavior.Restrict);
        }
    }
}