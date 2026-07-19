using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Tests.Entities;

namespace NPE.Infrastructure.Modules.Tests.Configurations
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.ToTable("Test", tb =>
            {
                tb.HasTrigger("InserTesttoRateType");
                tb.HasTrigger("UpdateTesttoRateType");
                tb.HasTrigger("biSyncDeleteTestTrackingTrigger");
                tb.HasTrigger("biSyncInsertTestTrackingTrigger");
                tb.HasTrigger("biSyncUpdateTestTrackingTrigger");
                tb.HasTrigger("td_Test");
                tb.HasTrigger("ti_Test");
                tb.HasTrigger("tu_Test");
            });

            builder.HasIndex(e => new { e.Code, e.Name }, "IX_Test").HasFillFactor(100);

            // Primary Key
            builder.Property(e => e.ID).ValueGeneratedNever();

            // Properties
            builder.Property(e => e.Code).HasMaxLength(10).IsUnicode(false);
            builder.Property(e => e.Comments).HasMaxLength(5000).IsUnicode(false);
            builder.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())").HasColumnType("smalldatetime");
            builder.Property(e => e.Createdby).HasMaxLength(30).IsUnicode(false).HasDefaultValue(" ");
            builder.Property(e => e.CriticalValueLowerBound).HasColumnType("decimal(15, 5)");
            builder.Property(e => e.CriticalValueUpperBound).HasColumnType("decimal(15, 5)");
            builder.Property(e => e.Format).HasMaxLength(50).IsUnicode(false).HasDefaultValueSql("(0)");
            builder.Property(e => e.ModifiedBy).HasMaxLength(30).IsUnicode(false).HasDefaultValue(" ");
            builder.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())").HasColumnType("smalldatetime");
            builder.Property(e => e.Name).HasMaxLength(200).IsUnicode(false);
            builder.Property(e => e.Rate).HasColumnType("decimal(15, 5)");
            builder.Property(e => e.ReportDays).HasDefaultValue((byte)0);
            builder.Property(e => e.ReportGroup).HasMaxLength(150).IsUnicode(false);
            builder.Property(e => e.ReportName).HasMaxLength(200).IsUnicode(false);
            builder.Property(e => e.SpecimenReqQuantity).HasMaxLength(100).IsUnicode(false);
            builder.Property(e => e.StabilityFrozen).HasMaxLength(50).IsUnicode(false);
            builder.Property(e => e.StabilityRefrigerated).HasMaxLength(50).IsUnicode(false);
            builder.Property(e => e.StabilityRoom).HasMaxLength(50).IsUnicode(false);
            builder.Property(e => e.Synonyms).HasMaxLength(500).IsUnicode(false);
            builder.Property(e => e.TemplateID).HasDefaultValue((short)0);
            builder.Property(e => e.TestHeading).HasMaxLength(150).IsUnicode(false);
            builder.Property(e => e.TestType).HasDefaultValue((short)1)
                .HasComment("use to save testtype (0 THEN 'Routine' WHEN 1 THEN 'Special' WHEN 2 THEN 'PCR' WHEN 3 THEN 'BIOPSY' )");
            builder.Property(e => e.Type).HasComment("0 For Normal , 1 for Profile and 2 for Package");
            builder.Property(e => e.Unit).HasMaxLength(100).IsUnicode(false);

            // THE ONLY RELATIONSHIP CONFIGURED HERE (Because Test holds GroupID)
            builder.HasOne(d => d.Group)
                   .WithMany(p => p.Tests)
                   .HasForeignKey(d => d.GroupID)
                   .HasConstraintName("FK_Test_Group");
        }
    }
}