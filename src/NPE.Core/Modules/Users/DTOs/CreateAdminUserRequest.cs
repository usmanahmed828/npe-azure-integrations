using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Users.DTOs
{
    public class CreateAdminUserRequest
    {
        public int CompanyId { get; set; }

        public int RootCenterId { get; set; }

        public int DefaultReferenceId { get; set; }

        public string Username { get; set; } = "";

        public string Password { get; set; } = "";

        public string FullName { get; set; } = "";

        public string Email { get; set; } = "";

        public string Phone { get; set; } = "";
    }
}
