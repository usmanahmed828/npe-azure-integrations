using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Tests.Entities;

namespace NPE.Infrastructure.Modules.Tests.Configurations
{
    public class TestSettingConfiguration : IEntityTypeConfiguration<TestSetting>
    {
        public void Configure(EntityTypeBuilder<TestSetting> builder)
        {
            // 1. Primary Key
            builder.HasKey(e => e.TestID);

            builder.ToTable("TestSetting", tb =>
            {
                tb.HasTrigger("biSyncDeleteTestSettingTrackingTrigger");
                tb.HasTrigger("biSyncInsertTestsettingTrackingTrigger");
                tb.HasTrigger("biSyncUpdateTestSettingTrackingTrigger");
            });

            builder.Property(e => e.TestID).ValueGeneratedNever();

            // 2. Map Properties
            builder.Property(e => e.AdditionalInfo).HasDefaultValue(false);
            builder.Property(e => e.AdditionalInfoValidationFields).HasMaxLength(200).IsUnicode(false);
            builder.Property(e => e.DefultValue).HasMaxLength(150).IsUnicode(false);
            builder.Property(e => e.Gender).HasDefaultValue((byte)0);
            builder.Property(e => e.IsHide).HasDefaultValue(false);
            builder.Property(e => e.RemarksCode).HasMaxLength(200).IsUnicode(false).HasDefaultValue("");
            builder.Property(e => e.SelectionList).HasMaxLength(500).IsUnicode(false);

            // Note: I highly recommend explicitly mapping the unbounded 'Settings' column if it's varchar(max)
            // builder.Property(e => e.Settings).IsUnicode(false).HasColumnType("varchar(max)");

            // 3. THE MISSING LINK: 1-to-1 Relationship Mapping
            // This mirrors exactly what we did for TestDetail
            builder.HasOne(d => d.Test)
                   .WithOne(p => p.TestSetting)
                   .HasForeignKey<TestSetting>(d => d.TestID)
                   .HasConstraintName("FK_TestSetting_Test");
        }
    }
}