using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.iLock.DTOs
{
    public class MenuNodeDTO
    {
        public int Id { get; set; }
        public string Key { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public string Route { get; set; } = "";
        public string Icon { get; set; } = "";
        public string? Description { get; set; }
        public int Level { get; set; }
        public string? ParentKey { get; set; }
        public bool Visible { get; set; }
        public bool Expanded { get; set; }
        public bool IsPage { get; set; }
        public bool IsGroup { get; set; }
        //public int SortOrder { get; set; }
        public int Level1SortOrder { get; set; }
        public int ChildSortOrder { get; set; }

        public List<MenuNodeDTO> Children { get; set; } = new();
    }

    public class SidebarMenuDTO
    {
        public string Name { get; set; } = "";

        public string Icon { get; set; } = "";

        public List<SidebarSubItemDTO> SubItems { get; set; } = new();
    }

    public class SidebarSubItemDTO
    {
        public string Name { get; set; } = "";

        public string Path { get; set; } = "";

        public bool Pro { get; set; }

        public List<SidebarSubItemDTO> SubItems { get; set; } = new();
    }
}
