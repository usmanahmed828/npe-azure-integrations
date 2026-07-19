using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Tenancy.Entities;

namespace NPE.Infrastructure.Modules.Tenancy.Configurations
{
    public class CompanyTestConfiguration : IEntityTypeConfiguration<CompanyTest>
    {
        public void Configure(EntityTypeBuilder<CompanyTest> builder)
        {
            builder.ToTable("CompanyTests", "tenant");

            builder.HasKey(x => new
            {
                x.CompanyId,
                x.TestId
            });

            builder.Property(x => x.CompanyId);

            builder.Property(x => x.TestId);

            builder.HasOne<Tests.Entities.Test>()
                .WithMany()
                .HasForeignKey(x => x.TestId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
