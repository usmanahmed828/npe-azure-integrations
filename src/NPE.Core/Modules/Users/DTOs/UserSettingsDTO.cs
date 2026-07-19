using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Users.DTOs
{
    public class UserSettingsDTO
    {
        public int DefaultLocation { get; set; } = 0;

        public int RowCount { get; set; } = 25;

        public int DateRange { get; set; } = 7;

        public int DefaultTestStatus { get; set; } = 0;

        public bool ShowHeaderFooter { get; set; } = true;

        public int DefaultReferenceID { get; set; } = 0;
    }
}
