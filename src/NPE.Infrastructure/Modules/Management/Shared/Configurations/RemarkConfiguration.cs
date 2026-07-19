using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Shared.Entities;

namespace NPE.Infrastructure.Modules.Management.Shared.Configurations
{
    public class RemarkConfiguration :
        IEntityTypeConfiguration<Remark>
    {
        public void Configure(
            EntityTypeBuilder<Remark> builder)
        {
            builder.ToTable(
                "Remark",
                tb =>
                {
                    tb.HasComment(
                        "Remarks related to case and Case Detail");

                    tb.HasTrigger(
                        "biSyncDeleteRemarkTrackingTrigger");

                    tb.HasTrigger(
                        "biSyncInsertRemarkTrackingTrigger");

                    tb.HasTrigger(
                        "biSyncUpdateRemarkTrackingTrigger");
                });

            builder.Property(
                x => x.Id)
                .ValueGeneratedNever();

            builder.Property(
                x => x.Rate)
                .HasDefaultValue(
                    0m);
        }
    }
}