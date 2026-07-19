using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Auth.Entities;

namespace NPE.Infrastructure.Modules.Auth.Configurations
{
    public class ExternalAppConfiguration : IEntityTypeConfiguration<ExternalApp>
    {
        public void Configure(EntityTypeBuilder<ExternalApp> builder)
        {
            builder.ToTable("ExternalApps");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AppId)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.SharedSecret)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true);

            builder.HasIndex(x => x.AppId).IsUnique();

            builder.HasMany(x => x.Permissions)
                   .WithOne(p => p.ExternalApp)
                   .HasForeignKey(p => p.ExternalAppID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}