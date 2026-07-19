using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Common.Extensions;
using NPE.Infrastructure.Modules.Cases.Entities;

namespace NPE.Infrastructure.Modules.Cases.Configurations;

public sealed class CaseAdditionalSettingConfiguration
    : IEntityTypeConfiguration<CaseAdditionalSetting>
{
    public void Configure(
        EntityTypeBuilder<CaseAdditionalSetting> builder)
    {
        builder.ToTable("CaseAdditionalSetting");

        #region Key

        builder.HasKey(x => x.CaseId);

        builder.Property(x => x.CaseId)
            .ValueGeneratedNever();

        #endregion

        #region Properties

        builder.Property(x => x.SecondReferenceName)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder.Property(x => x.SecondConsultantName)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder.Property(x => x.MedicalRecordNo)
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.Property(x => x.SampleReceivedFrom)
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.Property(x => x.SampleReceivedBy)
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.Property(x => x.Ponumber)
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.Property(x => x.CaseSettings)
            .IsUnicode(false);

        #endregion

        #region Relationship

        builder.HasOne(x => x.Case)
            .WithOne(x => x.AdditionalSetting)
            .HasForeignKey<CaseAdditionalSetting>(
                x => x.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}

public sealed class CaseClinicalDetailConfiguration
    : IEntityTypeConfiguration<CaseClinicalDetail>
{
    public void Configure(
        EntityTypeBuilder<CaseClinicalDetail> builder)
    {
        builder.ToTable("CaseClinicalDetail", tb =>
        {
            tb.HasTrigger(
                "biSyncDeleteCaseClinicalDetailTrackingTrigger");

            tb.HasTrigger(
                "biSyncInsertCaseClinicalDetailTrackingTrigger");

            tb.HasTrigger(
                "biSyncUpdateCaseClinicalDetailTrackingTrigger");
        });

        #region Key / Index

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.CaseId);

        #endregion

        #region Properties

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.ClinicalDetailCode)
            .IsUnicode(false)
            .HasMaxLength(5);

        builder.Property(x => x.Name)
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .IsUnicode(false)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedBy)
            .IsUnicode(false)
            .HasMaxLength(30);

        builder.Property(x => x.ModifiedBy)
            .IsUnicode(false)
            .HasMaxLength(30);

        builder.Property(x => x.CreatedDate)
            .HasColumnType("smalldatetime");

        builder.Property(x => x.ModifiedDate)
            .HasColumnType("smalldatetime");

        #endregion

        #region Relationship

        builder.HasOne(x => x.Case)
            .WithMany(x => x.CaseClinicalDetails)
            .HasForeignKey(x => x.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}

public sealed class CaseDetailInstrumentConfiguration
    : IEntityTypeConfiguration<CaseDetailInstrument>
{
    public void Configure(
        EntityTypeBuilder<CaseDetailInstrument> builder)
    {
        builder.ToTable("CaseDetailInstrument");

        #region Key / Index

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.CaseDetailId);

        #endregion

        #region Properties

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.CreatedBy)
            .IsUnicode(false)
            .HasMaxLength(30);

        builder.Property(x => x.ModifiedBy)
            .IsUnicode(false)
            .HasMaxLength(30);

        builder.Property(x => x.CreatedDate)
            .HasColumnType("smalldatetime");

        builder.Property(x => x.ModifiedDate)
            .HasColumnType("smalldatetime");

        #endregion

        #region Relationship

        builder.HasOne(x => x.CaseDetail)
            .WithOne(x => x.CaseDetailInstrument)
            .HasForeignKey<CaseDetailInstrument>(
                x => x.CaseDetailId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}

public sealed class CaseInfoConfiguration
    : IEntityTypeConfiguration<CaseInfo>
{
    public void Configure(
        EntityTypeBuilder<CaseInfo> builder)
    {
        builder.ToTable("CaseInfo");

        #region Key

        builder.HasKey(x => x.CaseId);

        builder.Property(x => x.CaseId)
            .ValueGeneratedNever();

        #endregion

        #region Properties

        builder.Property(x => x.Server)
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.Property(x => x.ClientIp)
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.Property(x => x.ClientName)
            .IsUnicode(false)
            .HasMaxLength(50);

        #endregion

        #region Relationship

        builder.HasOne(x => x.Case)
            .WithOne(x => x.CaseInfo)
            .HasForeignKey<CaseInfo>(
                x => x.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}


public sealed class CasePaymentOnlineConfiguration
    : IEntityTypeConfiguration<CasePaymentOnline>
{
    public void Configure(
        EntityTypeBuilder<CasePaymentOnline> builder)
    {
        builder.ToTable("CasePaymentOnline");

        #region Key / Index

        builder.HasKey(x => x.Id);

        builder.HasIndex(
            x => new
            {
                x.CaseId,
                x.PaymentType,
                x.ModifiedBy
            },
            "Index_7720233");

        #endregion

        #region Properties

        builder.Property(x => x.Amount)
            .HasColumnType("decimal(10,0)");

        builder.Property(x => x.ModifiedBy)
            .IsUnicode(false)
            .HasMaxLength(30);

        builder.Property(x => x.ModifiedDate)
            .HasColumnType("smalldatetime");

        builder.Property(x => x.AlertSentDate)
            .HasColumnType("smalldatetime");

        builder.Property(x => x.IsAlertSent)
            .HasDefaultValue(false);

        builder.Property(x => x.IsReceived)
            .HasDefaultValue((byte)0)
            .HasComment(
                "0 = No, 1 = Received, 2 = Error");

        builder.Property(x => x.PaymentType)
            .HasDefaultValue(0);

        #endregion

        #region Relationship

        builder.HasOne(x => x.Case)
            .WithOne(x => x.PaymentOnline)
            .HasForeignKey<CasePaymentOnline>(
                x => x.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}

public sealed class CaseRemarkConfiguration
    : IEntityTypeConfiguration<CaseRemark>
{
    public void Configure(
        EntityTypeBuilder<CaseRemark> builder)
    {
        builder.ToTable("CaseRemark", tb =>
        {
            tb.HasComment(
                "Stores remarks, notes, and charges.");

            tb.HasTrigger(
                "biSyncDeleteCaseRemarkTrackingTrigger");

            tb.HasTrigger(
                "biSyncInsertCaseRemarkTrackingTrigger");

            tb.HasTrigger(
                "biSyncUpdateCaseRemarkTrackingTrigger");
        });

        #region Key / Index

        builder.HasKey(x => x.Id)
            .IsClustered(false);

        builder.HasIndex(
            x => x.CaseId,
            "IX_CaseRemark")
            .IsClustered()
            .HasFillFactor(95);

        #endregion

        #region Properties

        builder.Property(x => x.Status).HasNexusStatusConversion();

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Description)
            .IsUnicode(false)
            .HasMaxLength(500);

        builder.Property(x => x.Rate)
            .HasColumnType("decimal(15,2)");

        builder.Property(x => x.CreatedBy)
            .IsUnicode(false)
            .HasMaxLength(30)
            .HasDefaultValue("Admin");

        builder.Property(x => x.ModifiedBy)
            .IsUnicode(false)
            .HasMaxLength(30)
            .HasDefaultValue("Admin");

        builder.Property(x => x.CreatedDate)
            .HasColumnType("smalldatetime")
            .HasDefaultValueSql("(getdate())");

        builder.Property(x => x.ModifiedDate)
            .HasColumnType("smalldatetime")
            .HasDefaultValueSql("(getdate())");

        builder.Property(x => x.Status)
            .HasDefaultValue(false);

        #endregion

        #region Relationship

        builder.HasOne(x => x.Case)
            .WithMany(x => x.CaseRemarks)
            .HasForeignKey(x => x.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}

public sealed class OutsourceCaseDetailConfiguration
    : IEntityTypeConfiguration<OutsourceCaseDetail>
{
    public void Configure(
        EntityTypeBuilder<OutsourceCaseDetail> builder)
    {
        builder.ToTable("OutsourceCaseDetail");

        #region Key / Index

        builder.HasKey(x => x.Id)
            .IsClustered(false);

        builder.HasIndex(x => x.CaseDetailId);

        #endregion

        #region Properties

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.DispatchDate)
            .HasColumnType("smalldatetime");

        builder.Property(x => x.DispatchClient)
            .IsUnicode(false)
            .HasMaxLength(30);

        #endregion

        #region Relationship

        builder.HasOne(x => x.CaseDetail)
            .WithOne(x => x.OutsourceCaseDetail)
            .HasForeignKey<OutsourceCaseDetail>(
                x => x.CaseDetailId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}