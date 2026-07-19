using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.iLock.Entities;

namespace NPE.Infrastructure.Modules.iLock.Configurations
{
    public class ILockUserAuditConfiguration : IEntityTypeConfiguration<ILockUserAudit>
    {
        public void Configure(EntityTypeBuilder<ILockUserAudit> builder)
        {
            builder.ToTable("iLock_User_Audit");

            builder.HasKey(x => x.AuditId);

            builder.Property(x => x.HostName)
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(x => x.HostIpaddress)
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(x => x.AppName)
                   .HasMaxLength(100)
                   .IsUnicode(false);
        }
    }
}
