using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NPE.Infrastructure.Modules.Management.Centers;

namespace NPE.Infrastructure.Modules.Management.Extra;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CenterCreditDetail>(entity =>
        {
            entity.ToTable("CenterCreditDetail", tb => tb.HasComment("Payment taken from patient for test(s) will be stored. "));

            entity.Property(e => e.CenterId).HasDefaultValue(0);
            entity.Property(e => e.Cno).HasComment("Cheque No or Credit Card No");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValue("Admin")
                .HasComment("");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Dated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Method).HasComment("0 Cash,1 Credit Card, 2 Cheque, 3 Transfer to Patient Account, 4 For Wavied Off <BR>3 and 4  will not be available on screen but it will used to transfer Case balance to Patient Account if Credit is allowed there. 4 will be used to wavie off by managment");
            entity.Property(e => e.ModifiedBy).HasDefaultValue("Admin");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Type).HasComment("0 For Advance, 1 for Due Received,2 for Adjustment");
        });

        modelBuilder.Entity<CenterCreditHistory>(entity =>
        {
            entity.HasKey(e => e.CaseId).HasName("PK_CenterCreditUsageHistory");

            entity.Property(e => e.CaseId).ValueGeneratedNever();
        });

        modelBuilder.Entity<CenterCreditPayment>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<CenterCreditSummary>(entity =>
        {
            entity.Property(e => e.CenterId).ValueGeneratedNever();
        });

        modelBuilder.Entity<CenterOwner>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<CenterPayment>(entity =>
        {
            entity.ToTable("CenterPayment", tb =>
                {
                    tb.HasTrigger("UpdateCenterBalance");
                    tb.HasTrigger("biSyncDeleteCenterPaymentTrackingTrigger");
                    tb.HasTrigger("biSyncInsertCenterPaymentTrackingTrigger");
                    tb.HasTrigger("biSyncUpdateCenterPaymentTrackingTrigger");
                });

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Cno).HasComment("Cheque No or Credit Card No");
            entity.Property(e => e.CreatedBy).HasDefaultValue("Admin");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Method).HasComment("0 For Cash, 1 for Cheque, 2 for Draft");
            entity.Property(e => e.ReceivedBy)
                .HasDefaultValue("Admin")
                .HasComment("");
            entity.Property(e => e.ReceivedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<CentersToCheckDueAmountFor>(entity =>
        {
            entity.HasKey(e => e.CenterId).HasName("PK_CentersToCheckAmountFor");

            entity.Property(e => e.CenterId).ValueGeneratedNever();
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City", tb =>
                {
                    tb.HasTrigger("biSyncDeleteCityTrackingTrigger");
                    tb.HasTrigger("biSyncInsertCityTrackingTrigger");
                    tb.HasTrigger("biSyncUpdateCityTrackingTrigger");
                });

            entity.Property(e => e.CityCode).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy).HasDefaultValue("Admin");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedBy).HasDefaultValue("Admin");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<CityLocation>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK_Location");

            entity.Property(e => e.Code).ValueGeneratedNever();
        });

        modelBuilder.Entity<ClinicalDetail>(entity =>
        {
            entity.ToTable("ClinicalDetail", tb =>
                {
                    tb.HasTrigger("biSyncDeleteClinicalDetailTrackingTrigger");
                    tb.HasTrigger("biSyncInsertClinicalDetailTrackingTrigger");
                    tb.HasTrigger("biSyncUpdateClinicalDetailTrackingTrigger");
                });

            entity.Property(e => e.CreatedBy).HasDefaultValue("Admin");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedBy).HasDefaultValue("Admin");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(false);
        });

        modelBuilder.Entity<ConsultantCommsionSetting>(entity =>
        {
            entity.Property(e => e.ConsultantId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country", tb =>
                {
                    tb.HasTrigger("biSyncDeleteCountryTrackingTrigger");
                    tb.HasTrigger("biSyncInsertCountryTrackingTrigger");
                    tb.HasTrigger("biSyncUpdateCountryTrackingTrigger");
                });

            entity.Property(e => e.CountryCode).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
        });

        //modelBuilder.Entity<KeyValue>(entity =>
        //{
        //    entity.Property(e => e.Id).ValueGeneratedNever();
        //});

        modelBuilder.Entity<ReferenceAgrement>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ReferencePayment>(entity =>
        {
            entity.ToTable("ReferencePayment", tb =>
                {
                    tb.HasComment("Payments made by Reference will be stored in it. A trigger will be written on it to update the balance of the reference.");
                    tb.HasTrigger("UpdateReferenceBalance");
                });

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Cno).HasComment("Cheque No or Credit Card No");
            entity.Property(e => e.CreatedBy).HasDefaultValue("Admin");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Discount).HasDefaultValue(0m);
            entity.Property(e => e.Method).HasComment("0 For Cash, 1 for Cheque, 2 for Draft");
            entity.Property(e => e.ReceivedBy)
                .HasDefaultValue("Admin")
                .HasComment("");
            entity.Property(e => e.ReceivedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Remark>(entity =>
        {
            entity.ToTable("Remark", tb =>
                {
                    tb.HasComment("Remarks related to case and Case Detail it also it will be used to configure other charges.");
                    tb.HasTrigger("biSyncDeleteRemarkTrackingTrigger");
                    tb.HasTrigger("biSyncInsertRemarkTrackingTrigger");
                    tb.HasTrigger("biSyncUpdateRemarkTrackingTrigger");
                });

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Internal).HasComment("1 will be used to indicate that this remark will be used internally for Lab ");
            entity.Property(e => e.Rate)
                .HasDefaultValue(0m)
                .HasComment("Charges if any otherwise 0");
            entity.Property(e => e.Type).HasComment("0 for cmments and 1 for other charges");
        });

        modelBuilder.Entity<RemarkSetting>(entity =>
        {
            entity.Property(e => e.RemarkId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
