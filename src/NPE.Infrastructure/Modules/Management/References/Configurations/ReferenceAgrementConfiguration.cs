using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Reference.Entities;

namespace NPE.Infrastructure.Modules.Management.Reference.Configurations
{
    public class ReferenceAgrementConfiguration :
        IEntityTypeConfiguration<ReferenceAgrement>
    {
        public void Configure(
            EntityTypeBuilder<ReferenceAgrement> builder)
        {
            builder.Property(
                x => x.Id)
                .ValueGeneratedNever();
        }
    }
}