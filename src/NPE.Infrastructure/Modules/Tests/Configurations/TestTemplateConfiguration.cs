using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Tests.Entities;

namespace NPE.Infrastructure.Modules.Tests.Configurations
{
    public class TestTemplateConfiguration : IEntityTypeConfiguration<TestTemplate>
    {
        public void Configure(EntityTypeBuilder<TestTemplate> builder)
        {
            builder.HasKey(e => e.ID).HasName("PK_Template");

            builder.ToTable("TestTemplate", tb =>
            {
                tb.HasTrigger("biSyncDeleteTestTemplateTrackingTrigger");
                tb.HasTrigger("biSyncInsertTestTemplateTrackingTrigger");
                tb.HasTrigger("biSyncUpdateTestTemplateTrackingTrigger");
            });

            builder.Property(e => e.ID).ValueGeneratedNever();
            builder.Property(e => e.CreatedBy).HasMaxLength(30).IsUnicode(false).HasDefaultValue("Admin");
            builder.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())").HasColumnType("smalldatetime");
            builder.Property(e => e.CutOffDate).HasColumnType("datetime");
            builder.Property(e => e.Department).HasMaxLength(100).IsUnicode(false);
            builder.Property(e => e.Description).HasMaxLength(1000).IsUnicode(false);
            builder.Property(e => e.DoctorStempId).HasDefaultValue(1);
            builder.Property(e => e.Image).HasMaxLength(3000);
            builder.Property(e => e.ModifiedBy).HasMaxLength(30).IsUnicode(false).HasDefaultValue("Admin");
            builder.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())").HasColumnType("smalldatetime");
            builder.Property(e => e.Name).HasMaxLength(100).IsUnicode(false);
            builder.Property(e => e.NewReportFormatInd).HasDefaultValue(false);
            builder.Property(e => e.ReportName).HasMaxLength(200).IsUnicode(false);
            builder.Property(e => e.TemplateGroupName).HasMaxLength(50).IsUnicode(false);
        }
    }
}