using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Common.Extensions;

namespace NPE.Infrastructure.Modules.Management.Consultant.Configurations
{
    public class ConsultantConfiguration : IEntityTypeConfiguration<Consultant.Entities.Consultant>
    {
        public void Configure(EntityTypeBuilder<Consultant.Entities.Consultant> builder)
        {
            //builder.ToTable("Consultant");
            builder.ToTable("Consultant", tb => tb.UseSqlOutputClause(false));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.Status)
    .HasDefaultValue(true)
    .HasNexusStatusConversion();
        }
    }
}
