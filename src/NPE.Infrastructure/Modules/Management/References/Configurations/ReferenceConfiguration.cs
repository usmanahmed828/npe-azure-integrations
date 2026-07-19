using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Common.Extensions;
using NPE.Infrastructure.Modules.Management.Reference.Entities;

namespace NPE.Infrastructure.Modules.Management.Reference.Configurations
{
    public class ReferenceConfiguration : IEntityTypeConfiguration<Reference.Entities.Reference>
    {
        public void Configure(EntityTypeBuilder<Reference.Entities.Reference> builder)
        {
            //builder.ToTable("Reference");
            builder.ToTable("Reference", tb => tb.UseSqlOutputClause(false));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Code)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.Address)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(x => x.Phone)
                .HasMaxLength(64)
                .IsUnicode(false);

            builder.Property(x => x.Fax)
                .HasMaxLength(64)
                .IsUnicode(false);

            builder.Property(x => x.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(x => x.RateTypeId)
                .HasColumnName("RateTypeID")
                .IsRequired();

            builder.Property(x => x.PaymentMode)
                .HasDefaultValue((byte)0);

            builder.Property(x => x.CreditLimit)
                .HasColumnType("decimal(15,5)")
                .HasDefaultValue(0);

            builder.Property(x => x.CurrentBalance)
                .HasColumnType("decimal(15,5)")
                .HasDefaultValue(0);

            builder.Property(x => x.DefaultDiscount)
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(0);

            builder.Property(x => x.MaxDiscount)
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(0);

            builder.Property(x => x.CreatedBy)
                .HasMaxLength(30)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.ModifiedBy)
                .HasMaxLength(30)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.Status)
    .HasDefaultValue(true)
    .HasNexusStatusConversion();

            // 1–1 Reference → ReferenceSetting
            builder.HasOne(x => x.ReferenceSetting)
                .WithOne(x => x.Reference)
                .HasForeignKey<ReferenceSetting>(x => x.ReferenceId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
