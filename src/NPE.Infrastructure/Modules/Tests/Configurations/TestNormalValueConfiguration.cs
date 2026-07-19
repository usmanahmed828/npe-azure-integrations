using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Tests.Entities;

namespace NPE.Infrastructure.Modules.Tests.Configurations
{
    public class TestNormalValueConfiguration : IEntityTypeConfiguration<TestNormalValue>
    {
        public void Configure(EntityTypeBuilder<TestNormalValue> builder)
        {
            // 1. Explicitly defined the table name
            builder.ToTable("TestNormalValues", tb =>
            {
                tb.HasTrigger("biSyncDeleteTestNormalValuesTrackingTrigger");
                tb.HasTrigger("biSyncInsertTestNormalValuesTrackingTrigger");
                tb.HasTrigger("biSyncUpdateTestNormalValuesTrackingTrigger");
                tb.HasTrigger("td_TestNormalValues");
                tb.HasTrigger("ti_TestNormalValues");
                tb.HasTrigger("tu_TestNormalValues");
            });

            // 2. Explicitly declared the Primary Key
            builder.HasKey(e => e.ID);
            builder.Property(e => e.ID).ValueGeneratedNever();

            builder.Property(e => e.AgeType).HasMaxLength(50).IsUnicode(false);
            builder.Property(e => e.CreatedBy).HasMaxLength(30).IsUnicode(false).HasDefaultValue("Admin");
            builder.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())").HasColumnType("smalldatetime");
            builder.Property(e => e.FromValue).HasColumnType("decimal(15, 5)");
            builder.Property(e => e.Gender).HasComment("0 for Male , 1 for Female and 2 for Both");
            builder.Property(e => e.ModifiedBy).HasMaxLength(30).IsUnicode(false).HasDefaultValue("Admin");
            builder.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())").HasColumnType("smalldatetime");
            builder.Property(e => e.Remarks).HasMaxLength(4000);
            builder.Property(e => e.TextValue).HasMaxLength(100);
            builder.Property(e => e.ToValue).HasColumnType("decimal(15, 5)");

            builder.HasOne(d => d.Test)
                   .WithMany(p => p.TestNormalValues)
                   .HasForeignKey(d => d.TestID)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_TestNormalValues_Test");

            builder.HasMany(d => d.TestNormalValueGraph)
                   .WithOne()
                   .HasForeignKey(g => g.TestNormalValueID)
                   .HasPrincipalKey(d => d.ID)
                   .HasConstraintName("FK_TestNormalValueGraph_TestNormalValues"); // <-- ADDED HERE
        }
    }
}