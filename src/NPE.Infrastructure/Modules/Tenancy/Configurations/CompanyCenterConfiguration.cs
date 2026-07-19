using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NPE.Infrastructure.Modules.Tenancy.Entities;

namespace NPE.Infrastructure.Modules.Tenancy.Configurations
{
    public class CompanyCenterConfiguration
        : IEntityTypeConfiguration<CompanyCenter>
    {
        public void Configure(
            EntityTypeBuilder<CompanyCenter> builder)
        {
            builder.ToTable(
                "CompanyCenters",
                "tenant");

            builder.HasKey(x =>
                new
                {
                    x.CompanyId,
                    x.CenterId
                });

            builder.Property(x =>
                x.CompanyId)
                .IsRequired();

            builder.Property(x =>
                x.CenterId)
                .IsRequired();

            builder.Property(x =>
                x.IsRootCenter)
                .HasDefaultValue(false);

            builder.HasIndex(x =>
                x.CompanyId)

                .HasDatabaseName(
                    "IX_CompanyCenters_CompanyId");

            builder.HasIndex(x =>
                x.CenterId)

                .HasDatabaseName(
                    "IX_CompanyCenters_CenterId");

            builder.HasOne(x =>
                x.Center)

                .WithMany(x =>
                    x.CompanyCenters)

                .HasForeignKey(x =>
                    x.CenterId)

                .OnDelete(
                    DeleteBehavior.Restrict)

                .HasConstraintName(
                    "FK_CompanyCenters_Center");

            builder.HasOne(x =>
                x.Company)

                .WithMany(x =>
                    x.CompanyCenters)

                .HasForeignKey(x =>
                    x.CompanyId)

                .OnDelete(
                    DeleteBehavior.Restrict)

                .HasConstraintName(
                    "FK_CompanyCenters_Company");
        }
    }
}