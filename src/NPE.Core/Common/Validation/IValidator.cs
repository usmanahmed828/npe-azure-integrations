using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Common.Validation
{
    public interface IValidator<in T>
    {
        void ValidateForCreate(T dto);
        void ValidateForUpdate(T dto);
    }
}
