using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Consultant.Entities;

namespace NPE.Infrastructure.Modules.Management.Consultant.Configurations
{
    public class ConsultantSettingConfiguration : IEntityTypeConfiguration<ConsultantSetting>
    {
        public void Configure(EntityTypeBuilder<ConsultantSetting> builder)
        {
            builder.ToTable("ConsultantSetting");

            // PK
            builder.HasKey(x => x.ConsultantId);

            builder.Property(x => x.ConsultantId)
                .HasColumnName("ConsultantID")
                .ValueGeneratedNever();

            // Money fields
            builder.Property(x => x.Commission)
                .HasColumnType("money")
                .HasDefaultValue(0);

            builder.Property(x => x.MaxDiscount)
                .HasColumnType("money")
                .IsRequired(false);

            // RateType
            builder.Property(x => x.RateTypeId)
                .HasColumnName("RateTypeID")
                .IsRequired(false);

            // Enum-like byte
            builder.Property(x => x.CommissionCalculationMethod)
                .IsRequired(false);

            // Flags
            builder.Property(x => x.IsTestCountByFlightNumber)
                .IsRequired(false);

            builder.Property(x => x.SecondaryConsultant)
                .IsRequired(false);

            // Text
            builder.Property(x => x.Speciality)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired(false);

            // ❌ NO relationships enforced here
            // Legacy-safe: Consultant may exist without settings
        }
    }
}
