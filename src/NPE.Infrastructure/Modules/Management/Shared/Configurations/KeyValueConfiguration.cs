using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Shared.Entities;

namespace NPE.Infrastructure.Modules.Management.Shared.Configurations
{
    public class KeyValueConfiguration :
        IEntityTypeConfiguration<KeyValue>
    {
        public void Configure(
            EntityTypeBuilder<KeyValue> builder)
        {
            builder.Property(
                x => x.Id)
                .ValueGeneratedNever();
        }
    }
}