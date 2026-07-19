using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Patients.Entities;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PatientCorporateInfo> PatientCorporateInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PatientCorporateInfo>(entity =>
        {
            entity.ToTable("PatientCorporateInfo", tb =>
                {
                    tb.HasComment("If patient belongs to an organization which is referring him/her to Lab then employee’s some job related detail should be recorded. This entity will used to store cooperate info. Note corporate info may be needed only if the Reference for the patient has been mentioned. ");
                    tb.HasTrigger("biSyncDeletePatientCorporateInfoTrackingTrigger");
                    tb.HasTrigger("biSyncInsertPatientCorporateInfoTrackingTrigger");
                    tb.HasTrigger("biSyncUpdatePatientCorporateInfoTrackingTrigger");
                });

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EmployeeId).HasComment("Employee Id of the Patient or the person who is referred to lab (if any)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
