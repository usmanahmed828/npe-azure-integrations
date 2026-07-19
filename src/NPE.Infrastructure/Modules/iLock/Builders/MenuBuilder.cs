using NPE.Core.Modules.iLock.DTOs;
using NPE.Infrastructure.Modules.iLock.Models;

namespace NPE.Infrastructure.Modules.iLock.Builders
{
    public class MenuBuilder
    {
        public List<MenuNodeDTO> Build(List<MenuFlatItem> dbItems)
        {
            if (dbItems == null || !dbItems.Any())
                return new();

            var nodes = BuildNodes(dbItems);
            return BuildTree(nodes, "");
        }

        private List<MenuNodeDTO> BuildNodes(List<MenuFlatItem> dbItems)
        {
            var result = new List<MenuNodeDTO>();

            foreach (var item in dbItems)
            {
                var level = ResolveLevel(item.Key);

                if (level > 0)
                {
                    var parent = ResolveParentKey(item.Key, level);

                    result.Add(new MenuNodeDTO
                    {
                        Id = item.Id,
                        Key = item.Key,
                        DisplayName = item.DisplayName,
                        Description = item.Description,
                        ParentKey = parent,
                        Level = level,
                        Visible = true,
                        Expanded = false,
                        IsPage = level == 3,
                        IsGroup = level != 3,
                        //SortOrder = ResolveSortOrder(item.Key),
                        Level1SortOrder = ResolveLevel1SortOrder(item.Key),
                        ChildSortOrder = ResolveChildSortOrder(item.Key),
                        Route = ResolveRoute(item.Key),
                        Icon = ResolveIcon(item.Key)
                    });
                }
            }

            return result;
        }

        //private List<MenuNodeDTO> BuildTree(List<MenuNodeDTO> allNodes, string parentKey)
        //{
        //    return allNodes.Where(x => string.Equals(x.ParentKey, parentKey, StringComparison.OrdinalIgnoreCase))
        //        .OrderBy(x => x.SortOrder)
        //        .ThenBy(x => x.DisplayName)
        //        .Select(x =>
        //        {
        //            x.Children = BuildTree(allNodes, x.Key);
        //            return x;
        //        }).ToList();
        //}
        private List<MenuNodeDTO> BuildTree(List<MenuNodeDTO> allNodes, string parentKey)
        {
            var items = allNodes.Where(x => string.Equals(x.ParentKey, parentKey, StringComparison.OrdinalIgnoreCase));

            if (string.IsNullOrWhiteSpace(parentKey))
            {
                // Level 1 menus
                items = items.OrderBy(x => x.Level1SortOrder).ThenBy(x => x.DisplayName);
            }
            else
            {
                // Level 2 + Level 3 menus
                items = items.OrderBy(x => x.ChildSortOrder).ThenBy(x => x.DisplayName);
            }

            return items
                .Select(x =>
                {
                    x.Children = BuildTree(allNodes, x.Key);
                    return x;
                }).ToList();
        }

        private int ResolveLevel(string key)
        {
            if (MenuStructure.Level1.Contains(key))
            {
                return 1;
            }

            if (MenuStructure.Level2Parent.ContainsKey(key))
            {
                return 2;
            }

            if (MenuStructure.Level3Parent.ContainsKey(key))
            {
                return 3;
            }

            return 0;
        }

        private string ResolveParentKey(string key, int level)
        {
            if (level == 1)
                return "";

            if (level == 2)
            {
                return MenuStructure.Level2Parent.TryGetValue(key, out var level1) ? level1 : "";
            }

            if (level == 3)
            {
                return MenuStructure.Level3Parent.TryGetValue(key, out var parent) ? parent : "";
            }

            return "";
        }

        private string ResolveIcon(string key)
        {
            return key.ToLower() switch
            {
                "reception" => "users",
                "management" => "settings",
                "reports" => "chart",
                "laboratory" => "flask",
                "samplecollection" => "package",
                "radiology" => "scan",
                "cash" => "cash",
                "expenses" => "wallet",

                _ => "circle"
            };
        }

        //private int ResolveSortOrder(string key)
        //{
        //    return key.ToLower() switch
        //    {
        //        "reception" => 1,
        //        "samplecollection" => 2,
        //        "laboratory" => 3,
        //        "management" => 4,
        //        "reports" => 5,
        //        "radiology" => 6,

        //        _ => 999
        //    };
        //}
        private string ResolveRoute(string key)
        {
            return "/" + key.Replace(" ", "").ToLower();
        }

        private int ResolveLevel1SortOrder(string key)
        {
            return MenuStructure.Level1SortOrder.TryGetValue(key, out var order) ? order : 999;
        }

        private int ResolveChildSortOrder(string key)
        {
            return MenuStructure.ChildSortOrder.TryGetValue(key, out var order) ? order : 999;
        }
    }
}