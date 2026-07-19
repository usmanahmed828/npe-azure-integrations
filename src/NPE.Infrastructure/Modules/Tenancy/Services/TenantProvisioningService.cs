using Microsoft.EntityFrameworkCore;
using NPE.Core.Modules.Signup.BusinessObjects;
using NPE.Infrastructure.Modules.iLock.Entities;

namespace NPE.Infrastructure.Modules.Tenancy.Services
{
    public class TenantProvisioningService : ITenantProvisioningService
    {
        private readonly ApplicationDbContext _context;

        public TenantProvisioningService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ProvisionAsync(int companyId, CancellationToken cancellationToken = default)
        {
            await CloneGroupsAsync(companyId, cancellationToken);

            await CloneGroupPermissionsAsync(companyId, cancellationToken);
        }

        #region Groups

        private async Task CloneGroupsAsync(int companyId, CancellationToken cancellationToken)
        {
            var sourceGroups = await _context.ILockGroups
                    .AsNoTracking()
                    .Where(x => x.CompanyId == 1)
                    .ToListAsync(cancellationToken);

            foreach (var group in sourceGroups)
            {
                _context.ILockGroups.Add(new ILockGroup
                {
                    CompanyId = companyId,
                    GroupId = group.GroupId,
                    Name = group.Name,
                    Description = group.Description,
                    CreatedBy = "SYSTEM",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = null,
                    ModifiedDate = null
                });
            }
        }

        #endregion

        #region Permissions

        private async Task CloneGroupPermissionsAsync(int companyId, CancellationToken cancellationToken)
        {
            var permissions = await _context.ILockGroupUIObjects
                    .AsNoTracking()
                    .Where(x => x.CompanyId == 1)
                    .ToListAsync(cancellationToken);

            foreach (var permission in permissions)
            {
                _context.ILockGroupUIObjects.Add(new ILockGroupUIObject
                {
                    CompanyId = companyId,
                    GroupId = permission.GroupId,
                    UIObjectId = permission.UIObjectId,
                    CreatedBy = "SYSTEM",
                    CreatedDate = DateTime.Now
                });
            }
        }

        #endregion
    }
}