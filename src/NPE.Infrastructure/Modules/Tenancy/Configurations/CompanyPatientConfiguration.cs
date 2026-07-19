using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Tenancy.Entities;

namespace NPE.Infrastructure.Modules.Tenancy.Configurations
{
    public class CompanyPatientConfiguration : IEntityTypeConfiguration<CompanyPatient>
    {
        public void Configure(EntityTypeBuilder<CompanyPatient> builder)
        {
            builder.ToTable("CompanyPatients", "tenant");

            builder.HasKey(x => new
            {
                x.CompanyId,
                x.PatientId
            });

            builder.Property(x => x.CompanyId);

            builder.Property(x => x.PatientId);

            builder.HasOne<Patients.Entities.Patient>()
                .WithMany()
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
