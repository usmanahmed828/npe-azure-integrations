using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NPE.Infrastructure.Modules.Cases.Entities.Configurations;

public sealed class CaseSettingConfiguration
    : IEntityTypeConfiguration<CaseSetting>
{
    public void Configure(
        EntityTypeBuilder<CaseSetting> builder)
    {
        builder.ToTable("CaseSetting", tb =>
        {
            tb.HasTrigger(
                "biSyncDeleteCaseSettingTrackingTrigger");

            tb.HasTrigger(
                "biSyncInsertCaseSettingTrackingTrigger");

            tb.HasTrigger(
                "biSyncUpdateCaseSettingTrackingTrigger");
        });

        #region Key / Index

        builder.HasKey(x => x.CaseId)
            .HasName("PK_CaseSetting_1");

        builder.HasIndex(
            x => x.CaseId,
            "IX_CaseSetting")
            .IsUnique()
            .HasFillFactor(100);

        #endregion

        #region Properties

        builder.Property(x => x.CaseId)
            .ValueGeneratedNever();

        builder.Property(x => x.IsAlertSent)
            .HasDefaultValue(false);

        builder.Property(x => x.IsCompleted)
            .HasDefaultValue(false);

        builder.Property(x => x.IsEmailSent)
            .HasDefaultValue(false);

        #endregion

        #region Relationship

        builder.HasOne(x => x.Case)
            .WithOne(x => x.CaseSetting)
            .HasForeignKey<CaseSetting>(
                x => x.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}