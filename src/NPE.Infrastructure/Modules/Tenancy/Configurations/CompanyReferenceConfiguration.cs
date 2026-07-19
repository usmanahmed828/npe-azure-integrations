using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NPE.Infrastructure.Modules.Tenancy;

namespace NPE.Infrastructure.Modules.Tenancy.Configurations
{
    public class CompanyReferenceConfiguration
        : IEntityTypeConfiguration<CompanyReference>
    {
        public void Configure(
            EntityTypeBuilder<CompanyReference> builder)
        {
            builder.ToTable(
                "CompanyReferences",
                "tenant");

            builder.HasKey(x =>
                new
                {
                    x.CompanyId,
                    x.ReferenceId
                });

            builder.Property(x =>
                x.CompanyId)
                .IsRequired();

            builder.Property(x =>
                x.ReferenceId)
                .IsRequired();

            builder.HasIndex(x =>
                x.CompanyId)

                .HasDatabaseName(
                    "IX_CompanyReferences_CompanyId");

            builder.HasIndex(x =>
                x.ReferenceId)

                .HasDatabaseName(
                    "IX_CompanyReferences_ReferenceId");

            builder.HasOne(x =>
                x.Company)

                .WithMany(x =>
                    x.CompanyReferences)

                .HasForeignKey(x =>
                    x.CompanyId)

                .OnDelete(
                    DeleteBehavior.Restrict)

                .HasConstraintName(
                    "FK_CompanyReferences_Company");

            builder.HasOne(x =>
                x.Reference)

                .WithMany()

                .HasForeignKey(x =>
                    x.ReferenceId)

                .OnDelete(
                    DeleteBehavior.Restrict)

                .HasConstraintName(
                    "FK_CompanyReferences_Reference");
        }
    }
}