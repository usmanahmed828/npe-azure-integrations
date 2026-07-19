using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Cases.Entities;

namespace NPE.Infrastructure.Modules.Cases.Entities.Configurations;

public sealed class CasePaymentConfiguration
    : IEntityTypeConfiguration<CasePayment>
{
    public void Configure(
        EntityTypeBuilder<CasePayment> builder)
    {
        builder.ToTable("CasePayment", tb =>
        {
            tb.HasComment(
                "Stores payments received for case.");

            tb.HasTrigger("InsertCashLog");
            tb.HasTrigger("UpdateCaseDueAmount");
            tb.HasTrigger("UpdateCasepaymentInfo");
            tb.HasTrigger(
                "biSyncDeleteCasePaymentTrackingTrigger");
            tb.HasTrigger(
                "biSyncInsertCasePaymentTrackingTrigger");
            tb.HasTrigger(
                "biSyncUpdateCasePaymentTrackingTrigger");
        });

        #region Key / Index

        builder.HasKey(x => x.Id);

        builder.HasIndex(
            x => x.CaseId,
            "_dta_index_CasePayment_7_2139206721__K2_4_7");

        #endregion

        #region Properties

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Amount)
            .HasColumnType("decimal(10,0)");

        builder.Property(x => x.CenterId)
            .HasDefaultValue(0);

        builder.Property(x => x.CreatedBy)
            .HasDefaultValue("Admin");

        builder.Property(x => x.CreatedDate)
            .HasDefaultValueSql("(getdate())");

        builder.Property(x => x.Dated)
            .HasDefaultValueSql("(getdate())");

        builder.Property(x => x.ModifiedBy)
            .HasDefaultValue("Admin");

        builder.Property(x => x.ModifiedDate)
            .HasDefaultValueSql("(getdate())");

        builder.Property(x => x.Method)
            .HasComment(
                "0=Cash,1=Card,2=Cheque,3=Transfer,4=Waived");

        builder.Property(x => x.Type)
            .HasComment(
                "0=Advance,1=DueReceived,2=Adjustment");

        builder.Property(x => x.Cno)
            .HasComment(
                "Cheque No / Card No");

        #endregion

        #region Relationship

        builder.HasOne(x => x.Case)
            .WithMany(x => x.CasePayments)
            .HasForeignKey(x => x.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}