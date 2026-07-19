using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Common.Security
{
    public interface ICurrentUser
    {
        int UserId { get; }

        int CompanyId { get; }

        string Username { get; }

        bool IsAuthenticated { get; }
    }
}
