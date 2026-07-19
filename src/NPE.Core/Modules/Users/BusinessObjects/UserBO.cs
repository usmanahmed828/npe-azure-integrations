using NPE.Core.Common.UnitOfWork;
using NPE.Core.Modules.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Users.BusinessObjects
{
    public interface IUserBO
    {
        Task<int>
            CreateAdminUserAsync(
                CreateAdminUserRequest request);
    }

    public class UserBO
        : IUserBO
    {
        private readonly IUserService
            _service;

        private readonly IUnitOfWork
            _uow;

        public UserBO(
            IUserService service,
            IUnitOfWork uow)
        {
            _service =
                service;

            _uow =
                uow;
        }

        public async Task<int>
            CreateAdminUserAsync(
                CreateAdminUserRequest request)
        {
            await _uow.BeginAsync();

            try
            {
                var userId =
                    await _service
                        .CreateAdminUserAsync(
                            request);

                await _uow.CommitAsync();

                return userId;
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }
        }
    }
}
