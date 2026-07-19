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
    public class UserWebSettingsConfiguration :
        IEntityTypeConfiguration<UserWebSettings>
    {
        public void Configure(
            EntityTypeBuilder<UserWebSettings> builder)
        {
            builder.ToTable("UserWebSettings");

            builder.HasKey(x => new
            {
                x.UserId,
                x.SettingId
            });

            builder.Property(x =>
                x.UserId)
                .HasColumnName("UserID");

            builder.Property(x =>
                x.SettingId)
                .HasColumnName("SettingID");

            builder.Property(x =>
                x.Value)
                .HasColumnName("Value");

            builder.HasOne(
                    x => x.WebSetting)
                .WithMany(
                    x => x.UserWebSettings)
                .HasForeignKey(
                    x => x.SettingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
