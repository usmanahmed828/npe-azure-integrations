using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Users.Entities;

namespace NPE.Infrastructure.Modules.Users.Configurations
{
    public class ILockUserLocationConfiguration : IEntityTypeConfiguration<ILockUserLocation>
    {
        public void Configure(EntityTypeBuilder<ILockUserLocation> builder)
        {
            builder.ToTable("iLock_UserLocation");

            builder.HasKey(x => new { x.CompanyId, x.UserId, x.LocationId });

            builder.Property(x => x.LocationId)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(x => x.CreatedBy)
                   .HasMaxLength(30)
                   .IsUnicode(false);
        }
    }
}
