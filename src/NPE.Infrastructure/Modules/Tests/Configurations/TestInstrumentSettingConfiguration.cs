using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Tests.Entities;

namespace NPE.Infrastructure.Modules.Tests.Configurations
{
    public class TestInstrumentSettingConfiguration : IEntityTypeConfiguration<TestInstrumentSetting>
    {
        public void Configure(EntityTypeBuilder<TestInstrumentSetting> builder)
        {
            builder.ToTable("TestInstrumentSetting");

            // 1. Explicitly declare the Primary Key
            builder.HasKey(e => e.ID);
            builder.Property(e => e.ID).ValueGeneratedNever();

            builder.Property(e => e.CreatedBy).HasMaxLength(30).IsUnicode(false).HasDefaultValue("Admin");
            builder.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())").HasColumnType("smalldatetime");

            // 2. Explicitly map as unbounded varchar (if it's MAX in DB)
            builder.Property(e => e.Description).IsUnicode(false).HasColumnType("varchar(max)");

            builder.Property(e => e.InstrumentName).HasMaxLength(30).IsUnicode(false);
            builder.Property(e => e.ModifiedBy).HasMaxLength(30).IsUnicode(false).HasDefaultValue("Admin");
            builder.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())").HasColumnType("smalldatetime");
            builder.Property(e => e.TestCode).HasMaxLength(30).IsUnicode(false);

            // 3. Perfect 1-to-Many Relationship Mapping
            builder.HasOne(d => d.Test)
                   .WithMany(p => p.TestInstrumentSettings)
                   .HasForeignKey(d => d.TestID)
                   .HasConstraintName("FK_TestInstrumentSetting_Test");
        }
    }
}