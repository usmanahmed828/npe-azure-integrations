using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.iLock.Entities;

namespace NPE.Infrastructure.Modules.iLock.Configurations
{
    public class ILockRightConfiguration : IEntityTypeConfiguration<ILockRight>
    {
        public void Configure(EntityTypeBuilder<ILockRight> builder)
        {
            builder.ToTable("iLock_Right");

            builder.HasKey(x => new { x.RightId, x.CompanyId })
                   .HasName("PK_Ilock_Right");

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(30)
                   .IsUnicode(false);

            builder.Property(x => x.Description)
                   .HasColumnType("varchar(max)");

            builder.Property(x => x.CreatedBy)
                   .HasMaxLength(30)
                   .IsUnicode(false);

            builder.Property(x => x.ModifiedBy)
                   .HasMaxLength(30)
                   .IsUnicode(false);

            // FK → Company
            builder.HasOne<ILockCompany>()
                   .WithMany()
                   .HasForeignKey(x => x.CompanyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
