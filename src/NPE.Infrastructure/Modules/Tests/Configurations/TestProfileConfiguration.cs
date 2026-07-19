using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Tests.Entities;

namespace NPE.Infrastructure.Modules.Tests.Configurations
{
    public class TestProfileConfiguration : IEntityTypeConfiguration<TestProfile>
    {
        public void Configure(EntityTypeBuilder<TestProfile> builder)
        {
            // 1. Explicit Composite Primary Key
            builder.HasKey(e => new { e.ProfileID, e.TestID });

            builder.ToTable("TestProfile", tb =>
            {
                tb.HasTrigger("biSyncDeleteTestProfileTrackingTrigger");
                tb.HasTrigger("biSyncInsertTestProfileTrackingTrigger");
                tb.HasTrigger("biSyncUpdateTestProfileTrackingTrigger");
            });

            builder.Property(e => e.SortOrder).HasDefaultValue((short)0);

            // 2. THE PARENT LINK (The Profile Hub)
            // This maps ProfileID back to the Test entity, using your TestProfile collection
            builder.HasOne(d => d.Profile)
                   .WithMany(p => p.TestProfile)
                   .HasForeignKey(d => d.ProfileID)
                   .OnDelete(DeleteBehavior.ClientSetNull) // CRITICAL for self-referencing tables
                   .HasConstraintName("FK_TestProfile_Test_Profile");

            // 3. THE CHILD LINK (The specific Test inside the Profile)
            // This maps TestID back to the Test entity (without needing a second collection in Test)
            builder.HasOne(d => d.Test)
                   .WithMany() // Empty WithMany() means "I don't need a reverse list of all profiles this child belongs to"
                   .HasForeignKey(d => d.TestID)
                   .OnDelete(DeleteBehavior.ClientSetNull) // CRITICAL to prevent SQL Server multiple cascade path errors
                   .HasConstraintName("FK_TestProfile_Test_Child");
        }
    }
}