using NPE.Core.Modules.iLock.DTOs;
using NPE.Core.Modules.Users.DTOs;

namespace NPE.Core.Modules.Bootstrap.DTOs
{
    public class BootstrapSecurityDTO
    {
        public UserInfoDTO
            User
        { get; set; }
                    = new();

        public List<string>
            Permissions
        { get; set; }
                    = new();

        public List<string>
            Modules
        { get; set; }
                    = new();

        public UserSettingsDTO
            UserSettings
        { get; set; }
                    = new();

        public List<SidebarMenuDTO>
            Menus
        { get; set; }
                    = new();
    }

    public class UserInfoDTO
    {
        public int UserId
        { get; set; }

        public int CompanyId
        { get; set; }

        public int CenterId
        { get; set; }

        public string Username
        { get; set; } = "";

        public string FullName
        { get; set; } = "";
    }
}