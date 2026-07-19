using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Common.Extensions;

namespace NPE.Infrastructure.Modules.Management.Centers.Configurations
{
    public class CenterConfiguration : IEntityTypeConfiguration<Center>
    {
        public void Configure(EntityTypeBuilder<Center> builder)
        {
            //builder.ToTable("Center");
            builder.ToTable("Center", tb => tb.UseSqlOutputClause(false));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(x => x.Status)
    .HasDefaultValue(true)
    .HasNexusStatusConversion();

            // Configure other properties as needed
            builder
    .HasOne(
        x => x.CenterSetting)
    .WithOne(
        x => x.Center)
    .HasForeignKey<CenterSetting>(
        x => x.CenterId)
    .OnDelete(
        DeleteBehavior.Cascade);

            builder
                .HasMany(
                    x => x.CenterAdditionalDatas)
                .WithOne(
                    x => x.Center)
                .HasForeignKey(
                    x => x.CenterID);

            builder
    .HasMany(
        x => x.CenterAdditionalInfos)
    .WithOne(
        x => x.Center)
    .HasForeignKey(
        x => x.CenterId)
    .IsRequired(false);

            builder
                .HasMany(
                    x => x.CenterConsultants)
                .WithOne(
                    x => x.Center)
                .HasForeignKey(
                    x => x.CenterId);

            builder
                .HasMany(
                    x => x.CenterReferences)
                .WithOne(
                    x => x.Center)
                .HasForeignKey(
                    x => x.CenterId);

            builder
                .HasMany(
                    x => x.CenterCreditDetails)
                .WithOne(
                    x => x.Center)
                .HasForeignKey(
                    x => x.CenterId);

            builder
                .HasOne(
                    x => x.CenterCreditSummary)
                .WithOne(
                    x => x.Center)
                .HasForeignKey<CenterCreditSummary>(
                    x => x.CenterId);
        }
    }
}
