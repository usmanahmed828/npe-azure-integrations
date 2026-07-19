using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPE.Infrastructure.Modules.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Infrastructure.Modules.Users.Configurations
{
    public class WebSettingsConfiguration :
        IEntityTypeConfiguration<WebSettings>
    {
        public void Configure(
            EntityTypeBuilder<WebSettings> builder)
        {
            builder.ToTable("WebSettings");

            builder.HasKey(x =>
                x.SettingId);

            builder.Property(x =>
                x.SettingId)
                .HasColumnName("SettingID");

            // DB typo preserved intentionally
            builder.Property(x =>
                x.SettingName)
                .HasColumnName("SetttingName")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x =>
                x.Status)
                .HasColumnName("Status")
                .IsRequired();
        }
    }
}
