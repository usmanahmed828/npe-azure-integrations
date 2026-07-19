using NPE.Core.Common.Crud;
using NPE.Core.Modules.Management.Center.Models;
using NPE.Core.Modules.Management.Consultant.DTOs;
using NPE.Core.Modules.Management.Reference.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Management.Consultant.BusinessObjects
{
    public interface IConsultantService : ICrudService<ConsultantDto, int>
    {
        //Task<List<ConsultantLookupDTO>> GetLookupAsync();
        //Task<List<ConsultantDto>> GetAllAsync();
        //Task<ConsultantDto?> GetByIdAsync(int id);
        //Task<int> CreateAsync(ConsultantDto dto);
        //Task<int> UpdateAsync(ConsultantDto dto);
        //Task DeleteAsync(int id);
        Task AssignConsultantToCompanyAsync(int companyId, int consultantId);
    }
}
