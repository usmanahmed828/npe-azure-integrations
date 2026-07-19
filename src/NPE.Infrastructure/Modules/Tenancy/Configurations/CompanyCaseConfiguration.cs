using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Tenancy.Entities;

namespace NPE.Infrastructure.Modules.Tenancy.Configurations
{
    public class CompanyCaseConfiguration : IEntityTypeConfiguration<CompanyCase>
    {
        public void Configure(EntityTypeBuilder<CompanyCase> builder)
        {
            builder.ToTable("CompanyCases", "tenant");

            builder.HasKey(x => new
            {
                x.CompanyId,
                x.CaseId
            });

            builder.Property(x => x.CompanyId);

            builder.Property(x => x.CaseId);

            builder.HasOne<Cases.Entities.Case>()
                .WithMany()
                .HasForeignKey(x => x.CaseId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
