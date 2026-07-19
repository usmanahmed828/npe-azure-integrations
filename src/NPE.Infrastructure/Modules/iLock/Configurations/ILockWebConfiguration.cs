using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.iLock.Entities;

namespace NPE.Infrastructure.Modules.iLock.Configurations
{
    public class ILockWebConfiguration : IEntityTypeConfiguration<ILockWeb>
    {
        public void Configure(EntityTypeBuilder<ILockWeb> builder)
        {
            builder.ToTable("iLock_Web");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UIContainer)
                   .HasMaxLength(100);

            builder.Property(x => x.UIObject)
                   .HasMaxLength(100);

            builder.Property(x => x.Description)
                   .HasMaxLength(200)
                   .IsUnicode(false);

            builder.Property(x => x.Value)
                   .HasMaxLength(50)
                   .IsUnicode(false);
        }
    }
}
