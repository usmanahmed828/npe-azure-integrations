using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Infrastructure.Modules.Users.Entities
{
    [PrimaryKey(nameof(UserId), nameof(SettingId))]
    [Table("UserWebSettings")]
    public class UserWebSettings
    {
        [Column("UserID")]
        public int UserId { get; set; }

        [Column("SettingID")]
        public int SettingId { get; set; }

        [Column("Value")]
        public int Value { get; set; }

        public WebSettings WebSetting { get; set; } = null!;
    }
}
