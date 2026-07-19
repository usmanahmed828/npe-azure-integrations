using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Infrastructure.Modules.Management.Shared.Entities
{
    [Table("KeyValue")]
    public class KeyValue
    {
        public int Id { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
    }
}