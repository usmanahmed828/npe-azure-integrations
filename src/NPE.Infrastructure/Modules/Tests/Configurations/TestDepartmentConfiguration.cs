using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Tests.Entities;

namespace NPE.Infrastructure.Modules.Tests.Configurations
{
    public class TestDepartmentConfiguration : IEntityTypeConfiguration<TestDepartment>
    {
        public void Configure(EntityTypeBuilder<TestDepartment> builder)
        {
            builder.ToTable("TestDepartment", tb =>
            {
                tb.HasTrigger("biSyncDeleteTestDepartmentTrackingTrigger");
                tb.HasTrigger("biSyncInsertTestDepartmentTrackingTrigger");
                tb.HasTrigger("biSyncUpdateTestDepartmentTrackingTrigger");
            });

            // Note: Your DB explicitly named this PK constraint "PK_Department"
            builder.HasKey(e => e.ID).HasName("PK_Department");
            builder.Property(e => e.ID).ValueGeneratedNever();

            builder.Property(e => e.CreatedBy).HasMaxLength(30).IsUnicode(false);
            builder.Property(e => e.CreatedDate).HasColumnType("smalldatetime");
            builder.Property(e => e.Description).HasMaxLength(1000).IsUnicode(false);
            builder.Property(e => e.ModifiedBy).HasMaxLength(30).IsUnicode(false);
            builder.Property(e => e.ModifiedDate).HasColumnType("smalldatetime");
            builder.Property(e => e.Name).HasMaxLength(100).IsUnicode(false);
        }
    }
}