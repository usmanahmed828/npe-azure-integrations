using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Cases.Entities;

namespace NPE.Infrastructure.Modules.Cases.Entities.Configurations;

public sealed class CorporatePaymentFinancialConfiguration
    : IEntityTypeConfiguration<CorporatePaymentFinancial>
{
    public void Configure(
        EntityTypeBuilder<CorporatePaymentFinancial> builder)
    {
        builder.ToTable("CorporatePaymentFinancials");

        #region Key

        builder.HasKey(x => x.CaseId);

        builder.Property(x => x.CaseId)
            .HasColumnName("CaseID")
            .ValueGeneratedNever();

        #endregion

        #region Properties

        builder.Property(x => x.CaseNetAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.CompanyAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.CompanyPaidAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.CompanyBalance)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.PatientAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.PatientPaidAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.PatientBalance)
            .HasColumnType("decimal(18,2)");

        #endregion

        #region Relationship

        builder.HasOne(x => x.Case)
            .WithOne(x => x.CorporatePaymentFinancial)
            .HasForeignKey<CorporatePaymentFinancial>(
                x => x.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}