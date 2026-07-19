using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.iLock.Entities;

namespace NPE.Infrastructure.Modules.iLock.Configurations
{
    public class ILockLocationConfiguration : IEntityTypeConfiguration<ILockLocation>
    {
        public void Configure(EntityTypeBuilder<ILockLocation> builder)
        {
            builder.ToTable("iLock_Location");

            builder.HasKey(x => new { x.LocationId, x.CompanyId });

            builder.Property(x => x.LocationId)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(x => x.Address)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(x => x.City)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(x => x.Country)
                   .HasMaxLength(50)
                   .IsUnicode(false);
        }
    }
}
