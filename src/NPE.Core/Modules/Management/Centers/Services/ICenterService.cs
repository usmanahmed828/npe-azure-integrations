using NPE.Core.Common.Crud;
using NPE.Core.Modules.Management.Center.Models;
using NPE.Core.Modules.Signup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Management.Centers.Services
{
    public interface ICenterService : ICrudService<CenterDTO, int>
    {
        Task<CenterLookupBundleDTO> GetLookupAsync();

        //Task<List<CenterDTO>> GetAllAsync();
        //Task<CenterDTO?> GetByIdAsync(int id);
        //Task<int> CreateAsync(CenterDTO dto);
        //Task<int> UpdateAsync(CenterDTO dto);
        //Task DeleteAsync(int id);

        Task AssignCenterToCompanyAsync(int companyId, int centerId, bool isRootCenter);
        Task SaveCenterReference(int centerId, int referenceId);
        Task SaveCenterConsultant(int centerId, int consultantId);
        Task SaveCenterAdditionalData(int centerId, int consultantId, int referenceId);
    }
}
