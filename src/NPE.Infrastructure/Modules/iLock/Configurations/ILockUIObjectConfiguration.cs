using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.iLock.Entities;

namespace NPE.Infrastructure.Modules.iLock.Configurations
{
    public class ILockUIObjectConfiguration : IEntityTypeConfiguration<ILockUIObject>
    {
        public void Configure(EntityTypeBuilder<ILockUIObject> builder)
        {
            builder.ToTable("iLock_UIObject");

            builder.HasKey(x => x.UIObjectId);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(x => x.DisplayName)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(x => x.Description)
                   .HasMaxLength(300)
                   .IsUnicode(false);

            // FK → Container
            builder.HasOne<ILockUIContainer>()
                   .WithMany()
                   .HasForeignKey(x => x.UIContainerId);

            builder.HasOne(x => x.UIContainer)
       .WithMany(c => c.UIObjects)
       .HasForeignKey(x => x.UIContainerId);
        }
    }
}
