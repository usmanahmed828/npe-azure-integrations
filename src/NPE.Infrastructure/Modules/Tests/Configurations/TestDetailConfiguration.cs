using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Tests.Entities;

namespace NPE.Infrastructure.Modules.Tests.Configurations
{
    public class TestDetailConfiguration : IEntityTypeConfiguration<TestDetail>
    {
        public void Configure(EntityTypeBuilder<TestDetail> builder)
        {
            builder.ToTable("TestDetail", tb =>
            {
                tb.HasTrigger("biSyncDeleteTestDetailTrackingTrigger");
                tb.HasTrigger("biSyncInsertTestDetailTrackingTrigger");
                tb.HasTrigger("biSyncUpdateTestDetailTrackingTrigger");
            });

            // 1. Explicitly define the Primary Key (Best Practice)
            builder.HasKey(e => e.ID);

            builder.Property(e => e.ID).ValueGeneratedNever();

            // 2. Explicitly map BOMID (from your Entity structure)
            builder.Property(e => e.BOMID);

            builder.Property(e => e.Description).HasMaxLength(6000).IsUnicode(false);

            // 3. Your flawless 1-to-1 relationship mapping
            builder.HasOne(d => d.IDNavigation)
                   .WithOne(p => p.TestDetail)
                   .HasForeignKey<TestDetail>(d => d.ID)
                   .HasConstraintName("FK_TestDetail_Test");
        }
    }
}