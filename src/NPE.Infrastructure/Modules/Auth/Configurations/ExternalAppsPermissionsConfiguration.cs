using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Auth.Entities;

namespace NPE.Infrastructure.Modules.Auth.Configurations
{
    public class ExternalAppPermissionConfiguration : IEntityTypeConfiguration<ExternalAppPermission>
    {
        public void Configure(EntityTypeBuilder<ExternalAppPermission> builder)
        {
            builder.ToTable("ExternalAppsPermissions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Permission)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}