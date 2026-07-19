using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Tests.Entities;

namespace NPE.Infrastructure.Modules.Tests.Configurations
{
    public class TestGroupConfiguration : IEntityTypeConfiguration<TestGroup>
    {
        public void Configure(EntityTypeBuilder<TestGroup> builder)
        {
            builder.ToTable("TestGroup", tb =>
            {
                tb.HasTrigger("biSyncDeleteTestGroupTrackingTrigger");
                tb.HasTrigger("biSyncInsertTestGroupTrackingTrigger");
                tb.HasTrigger("biSyncUpdateTestGroupTrackingTrigger");
            });

            // Explicit PK name from your DB
            builder.HasKey(e => e.ID).HasName("PK_Group");
            builder.Property(e => e.ID).ValueGeneratedNever();

            builder.Property(e => e.CreatedBy).HasMaxLength(30).IsUnicode(false).HasDefaultValue("Admin").HasComment("");
            builder.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())").HasColumnType("smalldatetime");
            builder.Property(e => e.ModifiedBy).HasMaxLength(30).IsUnicode(false).HasDefaultValue("Admin");
            builder.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())").HasColumnType("smalldatetime");
            builder.Property(e => e.Name).HasMaxLength(100).IsUnicode(false);
            builder.Property(e => e.ReportName).HasMaxLength(100).IsUnicode(false);
            builder.Property(e => e.Status).HasDefaultValue(false);

            // Relationship back to TestDepartment
            builder.HasOne(d => d.Department)
                   .WithMany(p => p.TestGroups)
                   .HasForeignKey(d => d.DepartmentID)
                   .HasConstraintName("FK_Group_Department");
        }
    }
}