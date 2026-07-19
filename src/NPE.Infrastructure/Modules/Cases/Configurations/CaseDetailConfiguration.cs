using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Common.Extensions;
using NPE.Infrastructure.Modules.Cases.Entities;

namespace NPE.Infrastructure.Modules.Cases.Entities.Configurations;

public sealed class CaseDetailConfiguration
    : IEntityTypeConfiguration<CaseDetail>
{
    public void Configure(
        EntityTypeBuilder<CaseDetail> builder)
    {
        builder.ToTable("CaseDetail", tb =>
        {
            tb.HasComment(
                "Stores tests registered against case.");

            tb.HasTrigger(
                "biSyncDeleteCaseDetailTrackingTrigger");

            tb.HasTrigger(
                "biSyncInsertCaseDetailTrackingTrigger");

            tb.HasTrigger(
                "biSyncUpdateCaseDetailTrackingTrigger");

            tb.HasTrigger("td_CaseDetail");
            tb.HasTrigger("ti_CaseDetail");
            tb.HasTrigger("trgAutoUpdateCaseStatusComments");
            tb.HasTrigger("trgInsertCaseDetailStatus");
            tb.HasTrigger("trgUpdateCaseStatusComments");
            tb.HasTrigger("tu_CaseDetail");
        });        

        #region Keys / Indexes

        builder.HasKey(x => x.Id)
            .IsClustered(false);

        builder.HasIndex(
            x => new
            {
                x.CaseId,
                x.TestId,
                x.TestStatus
            },
            "IX_CaseDetail")
            .IsClustered()
            .HasFillFactor(100);

        #endregion

        #region Properties

        builder.Property(x => x.Status).HasNexusStatusConversion();

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.CreatedBy)
            .HasDefaultValue("Admin");

        builder.Property(x => x.CreatedDate)
            .HasDefaultValueSql("(getdate())");

        builder.Property(x => x.ModifiedBy)
            .HasDefaultValue("Admin");

        builder.Property(x => x.ModifiedDate)
            .HasDefaultValueSql("(getdate())");

        builder.Property(x => x.TemplateId)
            .HasDefaultValue((short)1);

        builder.Property(x => x.SyncDateTime)
            .HasDefaultValueSql("(getdate())");

        builder.Property(x => x.TestStatus)
            .HasComment("Current test status");

        builder.Property(x => x.ReportingDate)
            .HasComment("Expected reporting date");

        #endregion

        #region Relationships

        builder.HasOne(x => x.Case)
            .WithMany(x => x.CaseDetails)
            .HasForeignKey(x => x.CaseId)
            .HasConstraintName("FK_CaseDetail_Case")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CaseDetailInstrument)
            .WithOne(x => x.CaseDetail)
            .HasForeignKey<CaseDetailInstrument>(
                x => x.CaseDetailId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.OutsourceCaseDetail)
            .WithOne(x => x.CaseDetail)
            .HasForeignKey<OutsourceCaseDetail>(
                x => x.CaseDetailId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}