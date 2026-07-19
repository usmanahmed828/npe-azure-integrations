using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Common
{
    public interface IPatientNumberService
    {
        Task<string> GenerateAsync(int centerCode, Int64 patientId);
    }
}
