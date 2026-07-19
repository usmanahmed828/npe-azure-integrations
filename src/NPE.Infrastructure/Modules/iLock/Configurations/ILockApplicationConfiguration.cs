using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.iLock.Entities;

namespace NPE.Infrastructure.Modules.iLock.Configurations
{
    public class ILockApplicationConfiguration : IEntityTypeConfiguration<ILockApplication>
    {
        public void Configure(EntityTypeBuilder<ILockApplication> builder)
        {
            builder.ToTable("iLock_Application");

            builder.HasKey(x => x.ApplicationId)
                   .HasName("PK_Application");

            builder.Property(x => x.ApplicationId)
                   .ValueGeneratedNever();

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(x => x.Description)
                   .HasMaxLength(100)
                   .IsUnicode(false);
        }
    }
}
