using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Centers;
using NPE.Infrastructure.Modules.Management.Reference.Entities;

namespace NPE.Infrastructure.Modules.Management.Center.Configurations
{
    public class CenterReferenceConfiguration :
        IEntityTypeConfiguration<CenterReference>
    {
        public void Configure(
            EntityTypeBuilder<CenterReference> builder)
        {
            builder
                .HasOne(
                    x => x.Center)
                .WithMany(
                    x => x.CenterReferences)
                .HasForeignKey(
                    x => x.CenterId)
                .OnDelete(
                    DeleteBehavior.Restrict);

            builder
                .HasOne(
                    x => x.Reference)
                .WithMany(
                    x => x.CenterReferences)
                .HasForeignKey(
                    x => x.ReferenceId)
                .OnDelete(
                    DeleteBehavior.Restrict);
        }
    }
}