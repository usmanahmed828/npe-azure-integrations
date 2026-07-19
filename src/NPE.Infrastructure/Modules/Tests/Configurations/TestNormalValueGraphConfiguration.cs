using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Tests.Entities;

namespace NPE.Infrastructure.Modules.Tests.Configurations
{
    public class TestNormalValueGraphConfiguration : IEntityTypeConfiguration<TestNormalValueGraph>
    {
        public void Configure(EntityTypeBuilder<TestNormalValueGraph> builder)
        {
            builder.ToTable("TestNormalValueGraph", tb =>
            {
                tb.HasTrigger("biSyncDeleteTestNormalValueGraphTrackingTrigger");
                tb.HasTrigger("biSyncInsertTestNormalValueGraphTrackingTrigger");
                tb.HasTrigger("biSyncUpdateTestNormalValueGraphTrackingTrigger");
            });

            // 1. Explicitly declare the Primary Key
            builder.HasKey(e => e.ID);
            builder.Property(e => e.ID).ValueGeneratedNever();

            // 2. Map all properties explicitly
            builder.Property(e => e.Color).HasMaxLength(15).HasDefaultValue("#ff0000");
            builder.Property(e => e.FromValue).HasMaxLength(50);
            builder.Property(e => e.Status).HasDefaultValue(true);
            builder.Property(e => e.StatusName).HasMaxLength(50).IsUnicode(false);
            builder.Property(e => e.StatusNameLabel).HasMaxLength(50);
            builder.Property(e => e.ToValue).HasMaxLength(50);

            // 3. Explicitly map SortOrder (from your entity definition)
            builder.Property(e => e.SortOrder);

            // Note: No FK configurations are needed here because we mapped 
            // the manual relationships from the parent (TestNormalValue) side!
        }
    }
}