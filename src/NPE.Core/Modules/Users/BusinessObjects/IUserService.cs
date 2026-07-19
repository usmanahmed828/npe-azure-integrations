using NPE.Core.Modules.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Users.BusinessObjects
{
    public interface IUserService
    {
        Task<int> CreateAdminUserAsync(
            CreateAdminUserRequest request);
    }
}
