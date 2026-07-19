using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Patients.Entities;

namespace NPE.Infrastructure.Modules.Patients.Configurations
{
    public sealed class PatientCorporateInfoConfiguration
        : IEntityTypeConfiguration<PatientCorporateInfo>
    {
        public void Configure(
            EntityTypeBuilder<PatientCorporateInfo> builder)
        {
            #region Table

            builder.ToTable(
                "PatientCorporateInfo",
                table =>
                {
                    table.HasComment(
                        "If patient belongs to an organization which is referring him/her to Lab then employee related corporate detail is stored here.");

                    table.HasTrigger(
                        "biSyncDeletePatientCorporateInfoTrackingTrigger");

                    table.HasTrigger(
                        "biSyncInsertPatientCorporateInfoTrackingTrigger");

                    table.HasTrigger(
                        "biSyncUpdatePatientCorporateInfoTrackingTrigger");
                });

            #endregion

            #region Primary Key

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            #endregion

            #region Properties

            builder.Property(x => x.EmployeeId)
                .HasColumnName("EmployeeID")
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasComment(
                    "Employee Id of the patient or referred person.");

            builder.Property(x => x.NameofEmployee)
                .HasMaxLength(64)
                .IsUnicode(false);

            builder.Property(x => x.Relation)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(x => x.Region)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(x => x.Division)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(x => x.Description)
                .HasMaxLength(500)
                .IsUnicode(false);

            #endregion
        }
    }
}