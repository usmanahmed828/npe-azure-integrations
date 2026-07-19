using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Shared.Entities;

namespace NPE.Infrastructure.Modules.Management.Shared.Configurations
{
    public class RemarkSettingConfiguration :
        IEntityTypeConfiguration<RemarkSetting>
    {
        public void Configure(
            EntityTypeBuilder<RemarkSetting> builder)
        {
            builder.Property(
                x => x.RemarkId)
                .ValueGeneratedNever();
        }
    }
}