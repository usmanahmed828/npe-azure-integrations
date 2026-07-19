using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.iLock.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Infrastructure.Modules.iLock.Configurations
{
    public class ILockUIContainerConfiguration : IEntityTypeConfiguration<ILockUIContainer>
    {
        public void Configure(EntityTypeBuilder<ILockUIContainer> builder)
        {
            builder.ToTable("iLock_UIContainer");

            builder.HasKey(x => x.UIContainerId)
                   .HasName("PK_iLock_ParentObject");

            builder.Property(x => x.DisplayName)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(x => x.FullName)
                   .IsRequired()
                   .HasMaxLength(300)
                   .IsUnicode(false);

            builder.Property(x => x.Description)
                   .HasMaxLength(300)
                   .IsUnicode(false);

            // FK → Application
            builder.HasOne<ILockApplication>()
                   .WithMany()
                   .HasForeignKey(x => x.ApplicationId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
