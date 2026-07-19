using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.iLock.Entities;

namespace NPE.Infrastructure.Modules.iLock.Configurations
{
    public class ILockGlobalDefConfiguration : IEntityTypeConfiguration<ILockGlobalDef>
    {
        public void Configure(EntityTypeBuilder<ILockGlobalDef> builder)
        {
            builder.ToTable("iLock_GlobalDef");

            builder.HasKey(x => x.GlobalId)
                   .HasName("PK_iLock_GlobalDef");

            builder.Property(x => x.GlobalId)
                   .HasColumnName("globalID");

            builder.Property(x => x.EnableLocation);

            builder.Property(x => x.EnableCompany);
        }
    }
}
