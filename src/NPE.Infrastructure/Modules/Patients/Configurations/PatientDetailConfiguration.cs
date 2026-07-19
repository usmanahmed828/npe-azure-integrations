// PatientDetailConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Patients.Entities;

namespace NPE.Infrastructure.Modules.Patients.Configurations
{
    public class PatientDetailConfiguration : IEntityTypeConfiguration<PatientDetail>
    {
        public void Configure(EntityTypeBuilder<PatientDetail> builder)
        {
            builder.ToTable("PatientDetails", tb => tb.UseSqlOutputClause(false));

            builder.HasKey(d => d.PatientId);
        }
    }
}