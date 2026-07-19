using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Infrastructure.Modules.iLock.Models
{
    public class MenuFlatItem
    {
        public int Id { get; set; }
        public string Key { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public string Description { get; set; } = "";
    }
}
