using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.iLock.Entities;

namespace NPE.Infrastructure.Modules.iLock.Configurations
{
    public class ILockGroupConfiguration
        : IEntityTypeConfiguration<ILockGroup>
    {
        public void Configure(
            EntityTypeBuilder<ILockGroup> builder)
        {
            builder.ToTable(
                "iLock_Group");

            builder.HasKey(x =>
                new
                {
                    x.CompanyId,
                    x.GroupId
                })
                .HasName(
                    "PK_iLock_Group");

            builder.Property(x =>
                x.Name)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(x =>
                x.Description)
                .HasColumnType(
                    "varchar(max)");

            builder.Property(x =>
                x.CreatedBy)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(x =>
                x.ModifiedBy)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.HasOne(x =>
                x.Company)

                .WithMany(x =>
                    x.ILockGroups)

                .HasForeignKey(x =>
                    x.CompanyId)

                .HasConstraintName(
                    "FK_iLock_Group_iLock_Company")

                .OnDelete(
                    DeleteBehavior.Cascade);

            builder.HasIndex(x =>
                x.CompanyId)

                .HasDatabaseName(
                    "IX_iLock_Group_CompanyId");
        }
    }
}