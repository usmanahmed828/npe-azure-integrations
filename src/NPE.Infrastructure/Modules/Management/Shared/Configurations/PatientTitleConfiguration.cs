using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Management.Shared.Entities;

namespace NPE.Infrastructure.Modules.Management.Shared.Configurations
{
    public class PatientTitleConfiguration : IEntityTypeConfiguration<PatientTitle>
    {
        public void Configure(EntityTypeBuilder<PatientTitle> builder)
        {
            builder.ToTable("PatientTitle");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Title)
                .HasColumnName("Title")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(x => x.Sex)
                .HasColumnName("Sex");

            builder.Property(x => x.Status)
                .HasColumnName("Status");

            builder.Property(x => x.CreatedBy)
                .HasColumnName("CreatedBy")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(x => x.CreatedDate)
                .HasColumnName("CreatedDate");

            builder.Property(x => x.ModifiedBy)
                .HasColumnName("ModifiedBy")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(x => x.ModifiedDate)
                .HasColumnName("ModifiedDate");
        }
    }
}
