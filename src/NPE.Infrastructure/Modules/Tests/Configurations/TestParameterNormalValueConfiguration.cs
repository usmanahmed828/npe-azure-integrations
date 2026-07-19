using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Tests.Entities;

namespace NPE.Infrastructure.Modules.Tests.Configurations
{
    public class TestParameterNormalValueConfiguration : IEntityTypeConfiguration<TestParameterNormalValue>
    {
        public void Configure(EntityTypeBuilder<TestParameterNormalValue> builder)
        {
            builder.ToTable("TestParameterNormalValue", tb =>
            {
                tb.HasTrigger("biSyncDeleteTestParameterNormalValueTrackingTrigger");
                tb.HasTrigger("biSyncInsertTestParameterNormalValueTrackingTrigger");
                tb.HasTrigger("biSyncUpdateTestParameterNormalValueTrackingTrigger");
                tb.HasTrigger("td_TestParameterNormalValue");
                tb.HasTrigger("ti_TestParameterNormalValue");
                tb.HasTrigger("tu_TestParameterNormalValue");
            });

            // 1. Explicitly declare the Primary Key
            builder.HasKey(e => e.ID);
            builder.Property(e => e.ID).ValueGeneratedNever();

            builder.Property(e => e.AgeType).HasMaxLength(50).IsUnicode(false);
            builder.Property(e => e.CreatedBy).HasMaxLength(30).IsUnicode(false).HasDefaultValue("Admin");
            builder.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())").HasColumnType("smalldatetime");
            builder.Property(e => e.FromValue).HasColumnType("decimal(15, 5)");
            builder.Property(e => e.Gender).HasComment("0 for Male , 1 for Female and 2 for Both");
            builder.Property(e => e.ModifiedBy).HasMaxLength(30).IsUnicode(false).HasDefaultValue("Admin");
            builder.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())").HasColumnType("smalldatetime");

            // Note: If you decided to use MAX types earlier, you can change these to .HasColumnType("nvarchar(max)")
            builder.Property(e => e.Remarks).HasMaxLength(4000);
            builder.Property(e => e.TextValue).HasMaxLength(2000).IsUnicode(false);

            builder.Property(e => e.ToValue).HasColumnType("decimal(15, 5)");

            // 2. Physical DB Relationship back to TestParameter
            builder.HasOne(d => d.TestParameter)
                   .WithMany(p => p.TestParameterNormalValues)
                   .HasForeignKey(d => d.TestParameterID)
                   .HasConstraintName("FK_TestParameterNormalValue_TestParameter");

            // 3. THE GRAPH RELATIONSHIP BINDING 🚀 (Now with Constraint Name)
            builder.HasMany(d => d.TestNormalValueGraph)
                   .WithOne()
                   .HasForeignKey(g => g.TestParameterNormalValueID)
                   .HasPrincipalKey(d => d.ID)
                   .HasConstraintName("FK_TestNormalValueGraph_TestParameterNormalValue"); // <-- ADDED HERE
        }
    }
}