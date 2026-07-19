using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Tests.Entities;

namespace NPE.Infrastructure.Modules.Tests.Configurations
{
    public class LISSpecimenTypeConfiguration : IEntityTypeConfiguration<LISSpecimenType>
    {
        public void Configure(EntityTypeBuilder<LISSpecimenType> builder)
        {
            builder.ToTable("LISSpecimenType");

            builder.HasKey(e => e.ID);

            builder.Property(e => e.AllowPrintName).HasDefaultValue(true);

            builder.Property(e => e.Code)
                   .HasMaxLength(5)
                   .IsUnicode(false);

            builder.Property(e => e.Name)
                   .HasMaxLength(50)
                   .IsUnicode(false);
        }
    }
}