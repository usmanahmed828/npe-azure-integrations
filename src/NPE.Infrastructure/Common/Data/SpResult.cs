using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Infrastructure.Common.Data
{
    public class SpResult<T>
    {
        public List<T> Data { get; set; } = new();

        public Dictionary<string, object?> OutputParameters { get; set; } = new();
    }
}
