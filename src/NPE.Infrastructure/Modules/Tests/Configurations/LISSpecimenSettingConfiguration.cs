using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Tests.Entities;
using System.Reflection.Emit;

namespace NPE.Infrastructure.Modules.Tests.Configurations
{
    public class LISSpecimenSettingConfiguration : IEntityTypeConfiguration<LISSpecimenSetting>
    {
        public void Configure(EntityTypeBuilder<LISSpecimenSetting> builder)
        {
            builder.ToTable("LISSpecimenSetting", tb =>
            {
                tb.HasTrigger("biSyncDeleteLISSpecimenSettingTrackingTrigger");
                tb.HasTrigger("biSyncInsertLISSpecimenSettingTrackingTrigger");
                tb.HasTrigger("biSyncUpdateLISSpecimenSettingTrackingTrigger");
            });

            // 1. Explicit Primary Key
            builder.HasKey(e => e.ID);

            // 🚀 THE MISSING LOCK: Tells EF Core we are generating IDs manually via IdentityService!
            builder.Property(e => e.ID).ValueGeneratedNever();

            // 2. Properties & Defaults
            builder.Property(e => e.TubeCount).HasDefaultValue(1);

            builder.Property(e => e.TubeType)
                   .HasDefaultValue(1)
                   .HasComment("1 = Yellow , 2 = Pupule , 3 = Green , 4 = SkyBlue , 5 = Gray , 7 = Urine");

            // 3. THE RESTORED RELATIONSHIPS 🚀
            // This tells EF Core: "One Test has Many LISSpecimenSettings"
            builder.HasOne(d => d.Test)
                   .WithMany(p => p.LISSpecimenSettings) // Ensure your Test.cs has this collection!
                   .HasForeignKey(d => d.TestID)
                   .HasConstraintName("FK_LISSpecimenSetting_Test_Manual");

            // This links the setting to the Specimen Type (Urine container, EDTA, etc.)
            builder.HasOne(d => d.SpecimenType)
                   .WithMany()
                   .HasForeignKey(d => d.SpecimenTypeID)
                   .HasConstraintName("FK_LISSpecimenSetting_LISSpecimenType_Manual");
        }
    }
}