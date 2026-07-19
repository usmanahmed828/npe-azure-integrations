using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Cases.Entities;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CaseAdditionalSetting> CaseAdditionalSettings { get; set; }

    public virtual DbSet<CaseClinicalDetail> CaseClinicalDetails { get; set; }

    public virtual DbSet<CaseDetailInstrument> CaseDetailInstruments { get; set; }

    public virtual DbSet<CaseInfo> CaseInfos { get; set; }

    public virtual DbSet<CasePaymentOnline> CasePaymentOnlines { get; set; }

    public virtual DbSet<CaseRemark> CaseRemarks { get; set; }

    public virtual DbSet<OutsourceCaseDetail> OutsourceCaseDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CaseAdditionalSetting>(entity =>
        {
            entity.Property(e => e.CaseId).ValueGeneratedNever();
        });

        modelBuilder.Entity<CaseClinicalDetail>(entity =>
        {
            entity.ToTable("CaseClinicalDetail", tb =>
                {
                    tb.HasTrigger("biSyncDeleteCaseClinicalDetailTrackingTrigger");
                    tb.HasTrigger("biSyncInsertCaseClinicalDetailTrackingTrigger");
                    tb.HasTrigger("biSyncUpdateCaseClinicalDetailTrackingTrigger");
                });

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<CaseInfo>(entity =>
        {
            entity.Property(e => e.CaseId).ValueGeneratedNever();
        });

        modelBuilder.Entity<CasePaymentOnline>(entity =>
        {
            entity.Property(e => e.IsAlertSent).HasDefaultValue(false);
            entity.Property(e => e.IsReceived)
                .HasDefaultValue((byte)0)
                .HasComment("0 = No, 1 = Received, 2 = Error");
            entity.Property(e => e.PaymentType).HasDefaultValue(0);
        });

        modelBuilder.Entity<CaseRemark>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("CaseRemark", tb =>
                {
                    tb.HasComment("Stores and remarks related to Case \r\nit will actually used to store other charges as well e.g. Home Test Devliery , or Special Note to Test Conductor which will be marked as Internal.\r\n\r\nDefault Values are same from remarks. Internal will be readonly on the screen. ");
                    tb.HasTrigger("biSyncDeleteCaseRemarkTrackingTrigger");
                    tb.HasTrigger("biSyncInsertCaseRemarkTrackingTrigger");
                    tb.HasTrigger("biSyncUpdateCaseRemarkTrackingTrigger");
                });

            entity.HasIndex(e => e.CaseId, "IX_CaseRemark")
                .IsClustered()
                .HasFillFactor(95);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy)
                .HasDefaultValue("Admin")
                .HasComment("");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedBy).HasDefaultValue("Admin");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(false);
        });

        modelBuilder.Entity<OutsourceCaseDetail>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
