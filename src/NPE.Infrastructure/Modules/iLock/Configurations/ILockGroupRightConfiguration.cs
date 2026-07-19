using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.iLock.Entities;

namespace NPE.Infrastructure.Modules.iLock.Configurations
{
    public class ILockGroupRightConfiguration : IEntityTypeConfiguration<ILockGroupRight>
    {
        public void Configure(EntityTypeBuilder<ILockGroupRight> builder)
        {
            builder.ToTable("iLock_GroupRight");

            builder.HasKey(x => new { x.CompanyId, x.RightId, x.GroupId })
                   .HasName("PK_iLock_GroupRight");

            // FK → Group
            builder.HasOne<ILockGroup>()
                   .WithMany()
                   .HasForeignKey(x => new { x.GroupId, x.CompanyId })
                   .OnDelete(DeleteBehavior.Cascade);

            // FK → Right
            builder.HasOne<ILockRight>()
                   .WithMany()
                   .HasForeignKey(x => new { x.RightId, x.CompanyId });

            builder.Property(x => x.CreatedBy)
                   .HasMaxLength(30)
                   .IsUnicode(false);
        }
    }
}
