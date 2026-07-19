using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.iLock.Entities;

namespace NPE.Infrastructure.Modules.iLock.Configurations
{
    public class ILockGroupUserConfiguration : IEntityTypeConfiguration<ILockGroupUser>
    {
        public void Configure(EntityTypeBuilder<ILockGroupUser> builder)
        {
            builder.ToTable("iLock_GroupUser", tb => tb.HasTrigger("InsertAdminEmployee"));

            builder.HasKey(x => new { x.CompanyId, x.UserId, x.GroupId })
                   .HasName("PK_iLock_GroupUser");

            // FK → Group
            builder.HasOne<ILockGroup>()
                   .WithMany()
                   .HasForeignKey(x => new { x.GroupId, x.CompanyId })
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ILockGroup)
       .WithMany(g => g.ILockGroupUsers)
       .HasForeignKey(x => new { x.CompanyId, x.GroupId })
       .HasPrincipalKey(g => new { g.CompanyId, g.GroupId });

            builder.Property(x => x.CreatedBy)
                   .HasMaxLength(30)
                   .IsUnicode(false);
        }
    }
}
