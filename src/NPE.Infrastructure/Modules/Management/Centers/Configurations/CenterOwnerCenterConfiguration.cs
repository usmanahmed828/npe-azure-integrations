using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Centers;

namespace NPE.Infrastructure.Modules.Management.Center.Configurations
{
    public class CenterOwnerCenterConfiguration :
        IEntityTypeConfiguration<CenterOwnerCenter>
    {
        public void Configure(
            EntityTypeBuilder<CenterOwnerCenter> builder)
        {
            builder
                .HasOne(
                    x => x.Center)
                .WithMany(
                    x => x.CenterOwnerCenters)
                .HasForeignKey(
                    x => x.CenterId)
                .OnDelete(
                    DeleteBehavior.Restrict);

            builder
                .HasOne(
                    x => x.CenterOwner)
                .WithMany(
                    x => x.CenterOwnerCenters)
                .HasForeignKey(
                    x => x.CenterOwnerId)
                .OnDelete(
                    DeleteBehavior.Restrict);
        }
    }
}