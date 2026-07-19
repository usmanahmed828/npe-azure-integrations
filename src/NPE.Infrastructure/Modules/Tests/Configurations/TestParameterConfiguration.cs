using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Tests.Entities;

namespace NPE.Infrastructure.Modules.Tests.Configurations
{
    public class TestParameterConfiguration : IEntityTypeConfiguration<TestParameter>
    {
        public void Configure(EntityTypeBuilder<TestParameter> builder)
        {
            builder.ToTable("TestParameter", tb =>
            {
                tb.HasTrigger("biSyncDeleteTestParameterTrackingTrigger");
                tb.HasTrigger("biSyncInsertTestParameterTrackingTrigger");
                tb.HasTrigger("biSyncUpdateTestParameterTrackingTrigger");
                tb.HasTrigger("td_TestParameter");
                tb.HasTrigger("ti_TestParameter");
                tb.HasTrigger("tu_TestParameter");
            });

            builder.HasIndex(e => new { e.TestID, e.Name }, "IX_TestParameter").HasFillFactor(95);

            // 1. Explicitly declare the Primary Key
            builder.HasKey(e => e.ID);
            builder.Property(e => e.ID).ValueGeneratedNever();

            builder.Property(e => e.CalculatedValueFormula).HasMaxLength(500).IsUnicode(false);
            builder.Property(e => e.CreatedBy).HasMaxLength(30).IsUnicode(false).HasDefaultValue("Admin");
            builder.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())").HasColumnType("smalldatetime");
            builder.Property(e => e.CriticalValueLowerBound).HasColumnType("decimal(15, 5)");
            builder.Property(e => e.CriticalValueUpperBound).HasColumnType("decimal(15, 5)");
            builder.Property(e => e.Description).HasMaxLength(500).IsUnicode(false);
            builder.Property(e => e.Format).HasMaxLength(50).IsUnicode(false).HasDefaultValueSql("((0))");
            builder.Property(e => e.ModifiedBy).HasMaxLength(30).IsUnicode(false).HasDefaultValue("Admin");
            builder.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())").HasColumnType("smalldatetime");
            builder.Property(e => e.Name).HasMaxLength(100).IsUnicode(false);
            builder.Property(e => e.ParameterName).HasMaxLength(50).IsUnicode(false);
            builder.Property(e => e.ReportName).HasMaxLength(100).IsUnicode(false);
            builder.Property(e => e.Unit).HasMaxLength(100).IsUnicode(false);

            // 2. Physical Database Relationship
            builder.HasOne(d => d.Test)
                   .WithMany(p => p.TestParameters)
                   .HasForeignKey(d => d.TestID)
                   .HasConstraintName("FK_TestParameter_Test");
        }
    }
}