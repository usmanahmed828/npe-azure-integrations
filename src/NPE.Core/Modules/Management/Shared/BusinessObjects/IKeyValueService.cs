using NPE.Core.Common.Crud;
using NPE.Core.Modules.Auth.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPE.Core.Modules.Management.KeyValue.DTOs;


namespace NPE.Core.Modules.Management.KeyValue.BusinessObjects
{
    public interface IKeyValueService : ICrudService<KeyValueDTO, int>
    {
        Task<List<KeyValueDTO>> GetByKeyName();
    }
}