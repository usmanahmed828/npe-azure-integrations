using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.iLock.Entities;

namespace NPE.Infrastructure.Modules.iLock.Configurations
{
    public class ILockIdentityConfiguration : IEntityTypeConfiguration<ILockIdentity>
    {
        public void Configure(EntityTypeBuilder<ILockIdentity> builder)
        {
            builder.ToTable("iLock_identity");

            builder.HasKey(x => x.Type);

            builder.Property(x => x.Type)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(x => x.NextId);
        }
    }
}
