using NPE.Core.Modules.iLock.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.iLock.BusinessObjects
{
    public interface IEncryptDecryptService
    {
        Task<string> EncryptString(string input);
        Task<string> DecryptString(string input);
    }
}
