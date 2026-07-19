using NPE.Core.Modules.iLock.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Infrastructure.Modules.iLock.Mapping
{
    public static class MenuMapper
    {
        public static List<SidebarMenuDTO> ToSidebar(List<MenuNodeDTO> menus)
        {
            return menus.Select(x =>
                new SidebarMenuDTO
                {
                    Name = x.DisplayName,
                    Icon = ResolveIcon(x.Key),
                    SubItems = MapChildren(x.Children)
                }).ToList();
        }

        private static List<SidebarSubItemDTO> MapChildren(List<MenuNodeDTO> children)
        {
            return children.Select(x =>
                new SidebarSubItemDTO
                {
                    Name = x.DisplayName,
                    Path = "/" + x.Key.ToLower(),
                    Pro = false,
                    SubItems = MapChildren(x.Children)
                }).ToList();
        }

        private static string ResolveIcon(string key)
        {
            return key.ToLower() switch
            {
                "reception" => "users",
                "management" => "settings",
                "laboratory" => "flask",
                "reports" => "chart",
                "samplecollection" => "package",
                "radiology" => "scan",
                _ => "circle"
            };
        }
    }
}
