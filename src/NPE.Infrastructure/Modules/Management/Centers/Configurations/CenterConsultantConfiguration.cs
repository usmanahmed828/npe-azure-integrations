using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Centers;

namespace NPE.Infrastructure.Modules.Management.Center.Configurations
{
    public class CenterConsultantConfiguration :
        IEntityTypeConfiguration<CenterConsultant>
    {
        public void Configure(
            EntityTypeBuilder<CenterConsultant> builder)
        {
            builder
                .HasOne(
                    x => x.Center)
                .WithMany(
                    x => x.CenterConsultants)
                .HasForeignKey(
                    x => x.CenterId)
                .OnDelete(
                    DeleteBehavior.Restrict);

            builder
                .HasOne(
                    x => x.Consultant)
                .WithMany(
                    x => x.CenterConsultants)
                .HasForeignKey(
                    x => x.ConsultantId)
                .OnDelete(
                    DeleteBehavior.Restrict);
        }
    }
}