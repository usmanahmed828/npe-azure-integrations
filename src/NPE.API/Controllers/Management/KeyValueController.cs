using Microsoft.AspNetCore.Mvc;
using NPE.Core.Common.UnitOfWork;
using NPE.Core.Modules.Management.KeyValue.BusinessObjects;
using NPE.Core.Modules.Management.KeyValue.DTOs;

namespace NPE.API.Controllers.Management
{
    [ApiController]
    [Route("api/keyvalue")]
    public class KeyValueController : BaseCrudController<KeyValueDTO, int>
    {
        private readonly IKeyValueService _service;

        public KeyValueController(IKeyValueService service, IUnitOfWork unitOfWork) : base(service, unitOfWork)
        {
            _service = service;
        }

      
    }
}