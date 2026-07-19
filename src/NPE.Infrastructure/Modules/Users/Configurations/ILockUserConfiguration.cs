using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Users.Entities;

namespace NPE.Infrastructure.Modules.Users.Configurations
{
    public class ILockUserConfiguration : IEntityTypeConfiguration<ILockUser>
    {
        public void Configure(EntityTypeBuilder<ILockUser> builder)
        {
            builder.ToTable("iLock_User", tb => tb.HasTrigger("ti_UserWebSettings"));

            builder.HasKey(x => new { x.UserId, x.CompanyId })
                   .HasName("PK_Ilock_User");

            builder.Property(x => x.UserName)
                   .IsRequired()
                   .HasMaxLength(30)
                   .IsUnicode(false);

            builder.Property(x => x.FirstName)
                   .IsRequired()
                   .HasMaxLength(30)
                   .IsUnicode(false);

            builder.Property(x => x.LastName)
                   .HasMaxLength(30)
                   .IsUnicode(false);

            builder.Property(x => x.Password)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(x => x.Email)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(x => x.Phone)
                   .HasMaxLength(20)
                   .IsUnicode(false);

            builder.Property(x => x.Mobile)
                   .HasMaxLength(20)
                   .IsUnicode(false);

            builder.Property(x => x.Address)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(x => x.City)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(x => x.State)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(x => x.Country)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(x => x.Disabled)
                   .HasDefaultValue(false);

            builder.Property(x => x.CreatedBy)
                   .HasMaxLength(30)
                   .IsUnicode(false);

            builder.Property(x => x.ModifiedBy)
                   .HasMaxLength(30)
                   .IsUnicode(false);
        }
    }
}
