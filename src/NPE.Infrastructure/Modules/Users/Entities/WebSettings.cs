using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Infrastructure.Modules.Users.Entities
{
    public class WebSettings
    {
        public int SettingId { get; set; }

        public string SettingName { get; set; } = "";

        public bool Status { get; set; }

        public ICollection<UserWebSettings> UserWebSettings { get; set; } = new List<UserWebSettings>();
    }
}
