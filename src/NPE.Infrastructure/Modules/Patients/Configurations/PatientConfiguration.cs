// PatientConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Patients.Entities;

namespace NPE.Infrastructure.Modules.Patients.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patient", tb => tb.UseSqlOutputClause(false));

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).ValueGeneratedNever();

        builder.HasOne(p => p.PatientDetail)
               .WithOne(d => d.Patient)
               .HasForeignKey<PatientDetail>(d => d.PatientId);

        builder.HasOne(p => p.PatientSetting)
               .WithOne(s => s.Patient)
               .HasForeignKey<PatientSetting>(s => s.Id);

        builder.HasOne(p => p.PatientCorporateInfo)
               .WithOne(x => x.Patient)
               .HasForeignKey<PatientCorporateInfo>(s => s.Id)
               .OnDelete(DeleteBehavior.Cascade);
    }
}