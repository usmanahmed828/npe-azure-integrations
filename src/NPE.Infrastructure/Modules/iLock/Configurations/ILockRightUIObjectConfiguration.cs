using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.iLock.Entities;

namespace NPE.Infrastructure.Modules.iLock.Configurations
{
    public class ILockRightUIObjectConfiguration : IEntityTypeConfiguration<ILockRightUIObject>
    {
        public void Configure(EntityTypeBuilder<ILockRightUIObject> builder)
        {
            builder.ToTable("iLock_RightUIObject");

            builder.HasKey(x => new { x.CompanyId, x.RightId, x.UIObjectId });

            builder.Property(x => x.CreatedBy)
                   .HasMaxLength(30)
                   .IsUnicode(false);
        }
    }
}
