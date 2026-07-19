using System;

namespace NPE.Core.Modules.Management.Shared.DTOs
{
    public class PatientTitleDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public short? Sex { get; set; }
        public bool? Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
