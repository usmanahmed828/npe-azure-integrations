using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Auth.DTOs
{
    public sealed class InternalLoginRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
    public sealed class InternalUserModel
    {
        public int UserId { get; set; }

        public int CompanyId { get; set; }

        public string Username { get; set; } = "";

        public string FullName { get; set; } = "";

        public int CenterId { get; set; }

        public List<string> Permissions { get; set; } = new();
    }
    public sealed class InternalLoginResponse
    {
        public string Token { get; set; } = "";

        public int UserId { get; set; }

        public int CompanyId { get; set; }

        public string Username { get; set; } = "";

        public string FullName { get; set; } = "";
    }
}
