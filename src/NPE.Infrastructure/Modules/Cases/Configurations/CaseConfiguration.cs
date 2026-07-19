using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Common.Extensions;

namespace NPE.Infrastructure.Modules.Cases.Entities.Configurations;

public sealed class CaseConfiguration
    : IEntityTypeConfiguration<Case>
{
    public void Configure(
        EntityTypeBuilder<Case> builder)
    {
        builder.ToTable("Case", tb =>
        {
            tb.HasComment(
                "Stores patient case header info.");

            tb.HasTrigger("UpdateCaseInfo");
            tb.HasTrigger("biSyncDeleteCaseTrackingTrigger");
            tb.HasTrigger("biSyncInsertCaseTrackingTrigger");
            tb.HasTrigger("biSyncUpdateCaseTrackingTrigger");
            tb.HasTrigger("td_Case");
            tb.HasTrigger("ti_Case");
            tb.HasTrigger("trgInsertCaseAlert");
            tb.HasTrigger("tu_Case");
        });

        #region Keys / Indexes

        builder.HasKey(x => x.Id);

        builder.HasIndex(
            x => new
            {
                x.CaseNumber,
                x.RegistrationDate,
                x.RegistrationLocation
            },
            "IX_Case")
            .HasFillFactor(100);

        builder.HasIndex(
            x => x.DestinationLocation,
            "IX_Case_2")
            .HasFillFactor(100);

        builder.HasIndex(
            x => x.PatientId,
            "IX_NC_PatientID")
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

        builder.Property(x => x.PaidAmount)
            .HasDefaultValue(0m);

        builder.Property(x => x.BankPaid)
            .HasDefaultValue(0m);

        builder.Property(x => x.BankDueReceived)
            .HasDefaultValue(0m);

        builder.Property(x => x.DueReceived)
            .HasDefaultValue(0m);

        builder.Property(x => x.Completed)
            .HasComment(
                "Set true when all tests approved.");

        builder.Property(x => x.TotalAmount)
            .HasComment(
                "Sum of tests + charges.");

        builder.Property(x => x.NetAmount)
            .HasComment(
                "Total - discount - less.");

        builder.Property(x => x.Due)
            .HasComment(
                "NetAmount - PaidAmount.");

        #endregion

        #region Relationships

        builder.HasOne(x => x.Patient)
            .WithMany(x => x.Cases)
            .HasForeignKey(x => x.PatientId)
            .HasConstraintName("FK_Case_Patient")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.CaseDetails)
            .WithOne(x => x.Case)
            .HasForeignKey(x => x.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.CasePayments)
            .WithOne(x => x.Case)
            .HasForeignKey(x => x.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.CaseClinicalDetails)
            .WithOne(x => x.Case)
            .HasForeignKey(x => x.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.CaseRemarks)
            .WithOne(x => x.Case)
            .HasForeignKey(x => x.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.AdditionalSetting)
            .WithOne(x => x.Case)
            .HasForeignKey<CaseAdditionalSetting>(
                x => x.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CaseInfo)
            .WithOne(x => x.Case)
            .HasForeignKey<CaseInfo>(
                x => x.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.PaymentOnline)
            .WithOne(x => x.Case)
            .HasForeignKey<CasePaymentOnline>(
                x => x.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CaseSetting)
            .WithOne(x => x.Case)
            .HasForeignKey<CaseSetting>(
                x => x.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CorporatePaymentFinancial)
            .WithOne(x => x.Case)
            .HasForeignKey<CorporatePaymentFinancial>(
                x => x.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}