using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Common.Identity.Entities;

namespace NPE.Infrastructure.Common.Identity.Configurations
{
    public class IdentityConfiguration : IEntityTypeConfiguration<Identity.Entities.Identity>
    {
        public void Configure(EntityTypeBuilder<Identity.Entities.Identity> builder)
        {
            builder.ToTable("Identity");

            builder.HasKey(x => new
            {
                x.CenterCode,
                x.Type,
                x.StartValue
            });

            builder.Property(x => x.Type)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.CurrentValue)
                .IsRequired();
        }
    }
}
