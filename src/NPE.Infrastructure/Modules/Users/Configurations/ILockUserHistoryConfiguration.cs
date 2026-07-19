using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Users.Entities;

namespace NPE.Infrastructure.Modules.Users.Configurations
{
    public class ILockUserHistoryConfiguration : IEntityTypeConfiguration<ILockUserHistory>
    {
        public void Configure(EntityTypeBuilder<ILockUserHistory> builder)
        {
            builder.ToTable("iLock_UserHistory");

            builder.HasKey(x => new { x.HistroyId, x.CompanyId });

            builder.Property(x => x.SiteDescription)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            // FK → User
            builder.HasOne<ILockUser>()
                   .WithMany()
                   .HasForeignKey(x => new { x.UserId, x.CompanyId })
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
