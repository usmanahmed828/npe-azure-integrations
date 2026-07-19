using NPE.Core.Common.Crud;
using NPE.Core.Modules.Management.Center.Models;
using NPE.Core.Modules.Management.Reference.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Management.Reference.BusinessObjects
{
    public interface IReferenceService : ICrudService<ReferenceDTO, int>
    {
        //Task<List<ReferenceLookupDTO>> GetLookupAsync();

        //Task<List<ReferenceDTO>> GetAllAsync();
        //Task<ReferenceDTO?> GetByIdAsync(int id);
        //Task<int> CreateAsync(ReferenceDTO dto);
        //Task<int> UpdateAsync(ReferenceDTO dto);
        //Task DeleteAsync(int id);

        Task AssignReferenceToCompanyAsync(int companyId, int referenceId);
    }
}
