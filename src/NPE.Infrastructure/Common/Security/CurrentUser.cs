using NPE.Core.Common.Security;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace NPE.Infrastructure.Common.Security
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _http;

        public CurrentUser(
            IHttpContextAccessor http)
        {
            _http = http;
        }

        public bool IsAuthenticated =>
            _http.HttpContext?.User?.Identity
                ?.IsAuthenticated ?? false;

        public int UserId =>
            int.TryParse(
                _http.HttpContext?
                    .User
                    .FindFirst("user_id")
                    ?.Value,
                out var id)
                ? id
                : 0;

        public int CompanyId =>
            int.TryParse(
                _http.HttpContext?
                    .User
                    .FindFirst("company_id")
                    ?.Value,
                out var id)
                ? id
                : 0;

        public string Username =>
            _http.HttpContext?
                .User
                .FindFirst(ClaimTypes.Name)
                ?.Value ?? "";
    }
}