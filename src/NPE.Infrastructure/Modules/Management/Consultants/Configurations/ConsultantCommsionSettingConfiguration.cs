using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Consultant.Entities;

namespace NPE.Infrastructure.Modules.Management.Consultant.Configurations
{
    public class ConsultantCommsionSettingConfiguration :
        IEntityTypeConfiguration<ConsultantCommsionSetting>
    {
        public void Configure(
            EntityTypeBuilder<ConsultantCommsionSetting> builder)
        {
            builder.Property(
                x => x.ConsultantId)
                .ValueGeneratedNever();
        }
    }
}