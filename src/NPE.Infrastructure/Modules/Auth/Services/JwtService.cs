using Microsoft.IdentityModel.Tokens;
using NPE.Core.Common.Security;
using NPE.Core.Modules.Auth.BusinessObjects;
using NPE.Core.Modules.Auth.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NPE.Infrastructure.Modules.Auth.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _settings;

        public JwtService(JwtSettings settings)
        {
            _settings = settings;
        }

        public string GenerateInternalToken(InternalUserModel user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypesCustom.UserId, user.UserId.ToString()),
                new(ClaimTypesCustom.CompanyId, user.CompanyId.ToString()),
                new(ClaimTypesCustom.CenterId, user.CenterId.ToString()),
                new(ClaimTypesCustom.FullName, user.FullName),
                new(ClaimTypesCustom.ClientType, "internal")
            };

            foreach (var perm in user.Permissions)
                claims.Add(new Claim(ClaimTypesCustom.Permission, perm));

            return CreateToken(claims);
        }

        public string GenerateExternalToken(ExternalAppModel app)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, app.AppId),
                new(ClaimTypesCustom.ClientType, "external")
            };

            foreach (var perm in app.Permissions)
                claims.Add(new Claim(ClaimTypesCustom.Permission, perm));

            return CreateToken(claims);
        }

        private string CreateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_settings.SecretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}