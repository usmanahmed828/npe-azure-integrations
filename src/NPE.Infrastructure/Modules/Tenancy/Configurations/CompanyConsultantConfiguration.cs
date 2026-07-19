using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NPE.Infrastructure.Modules.Tenancy;

namespace NPE.Infrastructure.Modules.Tenancy.Configurations
{
    public class CompanyConsultantConfiguration
        : IEntityTypeConfiguration<CompanyConsultant>
    {
        public void Configure(
            EntityTypeBuilder<CompanyConsultant> builder)
        {
            builder.ToTable(
                "CompanyConsultants",
                "tenant");

            builder.HasKey(x =>
                new
                {
                    x.CompanyId,
                    x.ConsultantId
                });

            builder.Property(x =>
                x.CompanyId)
                .IsRequired();

            builder.Property(x =>
                x.ConsultantId)
                .IsRequired();

            builder.HasIndex(x =>
                x.CompanyId)

                .HasDatabaseName(
                    "IX_CompanyConsultants_CompanyId");

            builder.HasIndex(x =>
                x.ConsultantId)

                .HasDatabaseName(
                    "IX_CompanyConsultants_ConsultantId");

            builder.HasOne(x =>
                x.Company)

                .WithMany(x =>
                    x.CompanyConsultants)

                .HasForeignKey(x =>
                    x.CompanyId)

                .OnDelete(
                    DeleteBehavior.Restrict)

                .HasConstraintName(
                    "FK_CompanyConsultants_Company");

            builder.HasOne(x =>
                x.Consultant)

                .WithMany()

                .HasForeignKey(x =>
                    x.ConsultantId)

                .OnDelete(
                    DeleteBehavior.Restrict)

                .HasConstraintName(
                    "FK_CompanyConsultants_Consultant");
        }
    }
}