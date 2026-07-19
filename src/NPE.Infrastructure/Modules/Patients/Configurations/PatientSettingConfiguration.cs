// PatientSettingConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Patients.Entities;

namespace NPE.Infrastructure.Modules.Patients.Configurations
{
    public class PatientSettingConfiguration : IEntityTypeConfiguration<PatientSetting>
    {
        public void Configure(EntityTypeBuilder<PatientSetting> builder)
        {
            builder.ToTable("PatientSettings", tb => tb.UseSqlOutputClause(false));

            builder.HasKey(s => s.Id);
        }
    }
}