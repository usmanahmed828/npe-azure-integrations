using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NPE.Infrastructure.Modules.Management.Shared.Entities
{
    [Table("PatientTitle")]
    public partial class PatientTitle
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string? Title { get; set; }

        public short? Sex { get; set; }

        public bool? Status { get; set; }

        [StringLength(50)]
        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
