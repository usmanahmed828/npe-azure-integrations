using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Centers;

namespace NPE.Infrastructure.Modules.Management.Centers.Configurations
{
    public class CenterLabNoConfiguration
        : IEntityTypeConfiguration<CenterLabNo>
    {
        public void Configure(
            EntityTypeBuilder<CenterLabNo> builder)
        {
            builder.ToTable(
                "CenterLabNos");

            builder
                .HasKey(
                    x => x.CenterCode);

            builder.Property(
                x => x.CenterCode)
                .ValueGeneratedNever();

            builder.Property(
                x => x.MaxLabNo)
                .IsRequired();

            builder
                .HasOne(
                    x => x.Center)

                .WithOne(
                    x => x.CenterLabNo)

                .HasForeignKey<CenterLabNo>(
                    x => x.CenterCode)

                .OnDelete(
                    DeleteBehavior.Restrict);
        }
    }
}