using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.iLock.Entities;

namespace NPE.Infrastructure.Modules.iLock.Configurations
{
    public class ILockCompanyConfiguration : IEntityTypeConfiguration<ILockCompany>
    {
        public void Configure(EntityTypeBuilder<ILockCompany> builder)
        {
            builder.ToTable("iLock_Company");

            builder.HasKey(x => x.CompanyId)
                   .HasName("PK_iLock_Division");

            builder.Property(x => x.CompanyId)
                   .ValueGeneratedNever();

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(x => x.Description)
                   .HasMaxLength(50)
                   .IsUnicode(false);
        }
    }
}
