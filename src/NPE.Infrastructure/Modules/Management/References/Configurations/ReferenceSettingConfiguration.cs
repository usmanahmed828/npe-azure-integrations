using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Common.Extensions;
using NPE.Infrastructure.Modules.Management.Reference.Entities;

namespace NPE.Infrastructure.Modules.Management.Reference.Configurations
{
    public class ReferenceSettingConfiguration : IEntityTypeConfiguration<ReferenceSetting>
    {
        public void Configure(EntityTypeBuilder<ReferenceSetting> builder)
        {
            builder.ToTable("ReferenceSetting");

            // PK
            builder.HasKey(x => x.ReferenceId);

            builder.Property(x => x.ReferenceId)
                .HasColumnName("ReferenceID")
                .ValueGeneratedNever();

            // Flags
            builder.Property(x => x.IsPrescriptionEnabled);
            //.HasDefaultValue(false);

            builder.Property(x => x.IsCouponEnabled);
                //.HasDefaultValue(false);

            //builder.Property(x => x.Status)
            //    .HasDefaultValue(true).HasNexusStatusConversion();
            builder.Property(x => x.Status);

            builder.Property(x => x.IsExtendedSearchEnabled)
                .IsRequired(false);

            builder.Property(x => x.IsLoyaltyCardEnabled)
                .IsRequired(false);

            builder.Property(x => x.IsOutsourceRequestEnabled);
                //.HasDefaultValue(false);

            builder.Property(x => x.IsAllowReportAccess)
                .IsRequired(false);

            builder.Property(x => x.SecondaryReference)
                .IsRequired(false);

            builder.Property(x => x.AdditionalInfo)
                .IsRequired(false);

            // Text
            builder.Property(x => x.CourierName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(false);

            builder.Property(x => x.AdditionalInfoValidationFields)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired(false);

            builder.Property(x => x.Settings)
                .IsUnicode()
                .IsRequired(false);

            // 1–1 Reference ↔ ReferenceSetting
            builder.HasOne(x => x.Reference)
                .WithOne(x => x.ReferenceSetting)
                .HasForeignKey<ReferenceSetting>(x => x.ReferenceId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
