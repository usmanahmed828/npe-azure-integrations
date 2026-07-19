using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Centers;

namespace NPE.Infrastructure.Modules.Management.Center.Configurations
{
    public class CenterCreditPaymentConfiguration :
        IEntityTypeConfiguration<CenterCreditPayment>
    {
        public void Configure(
            EntityTypeBuilder<CenterCreditPayment> builder)
        {
            builder.Property(
                x => x.Id)
                .ValueGeneratedOnAdd();

            builder
    .HasOne(
        x => x.Center)
    .WithMany(
        x => x.CenterCreditPayments)
    .HasForeignKey(
        x => x.CenterId)
    .OnDelete(
        DeleteBehavior.Restrict);
        }
    }
}