using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.iLock.Entities;

namespace NPE.Infrastructure.Modules.iLock.Configurations
{
    public class ILockGroupUIObjectConfiguration
        : IEntityTypeConfiguration<ILockGroupUIObject>
    {
        public void Configure(
            EntityTypeBuilder<ILockGroupUIObject> builder)
        {
            builder.ToTable(
                "iLock_GroupUIObject");

            builder.HasKey(x =>
                new
                {
                    x.CompanyId,
                    x.GroupId,
                    x.UIObjectId
                })
                .HasName(
                    "PK_iLock_GroupUIObject");

            builder.Property(x =>
                x.CreatedBy)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(x =>
                x.CreatedDate)
                .HasColumnType(
                    "smalldatetime");

            builder.HasOne(x =>
                x.Group)

                .WithMany(x =>
                    x.Permissions)

                .HasForeignKey(x =>
                    new
                    {
                        x.GroupId,
                        x.CompanyId
                    })

                .HasPrincipalKey(x =>
                    new
                    {
                        x.GroupId,
                        x.CompanyId
                    })

                .HasConstraintName(
                    "FK_iLock_GroupUIObject_iLock_Group")

                .OnDelete(
                    DeleteBehavior.Cascade);

            builder.HasOne(x =>
                x.UIObject)

                .WithMany()

                .HasForeignKey(x =>
                    x.UIObjectId)

                .HasConstraintName(
                    "FK_iLock_GroupUIObject_iLock_UIObject")

                .OnDelete(
                    DeleteBehavior.Restrict);

            builder.HasIndex(x =>
                new
                {
                    x.CompanyId,
                    x.GroupId
                })
                .HasDatabaseName(
                    "IX_iLock_GroupUIObject_Group");

            builder.HasIndex(x =>
                x.UIObjectId)
                .HasDatabaseName(
                    "IX_iLock_GroupUIObject_UIObject");
        }
    }
}