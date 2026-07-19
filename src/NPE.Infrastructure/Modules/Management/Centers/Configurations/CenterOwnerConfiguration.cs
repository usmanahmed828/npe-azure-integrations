using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Centers;

namespace NPE.Infrastructure.Modules.Management.Center.Configurations
{
    public class CenterOwnerConfiguration :
        IEntityTypeConfiguration<CenterOwner>
    {
        public void Configure(
            EntityTypeBuilder<CenterOwner> builder)
        {
            builder.Property(
                x => x.Id)
                .ValueGeneratedNever();
        }
    }
}