namespace NPE.Core.Modules.Patients.Models
{
    public class PatientDTO
    {
        public long Id { get; set; }

        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = ".";
        public string Fhname { get; set; } = ".";

        public byte Sex { get; set; }
        public DateTime DateOfBirth { get; set; }

        public byte MaritalStatus { get; set; } = 0;
        public string BloodGroup { get; set; } = "Unknown";

        public string? Nic { get; set; } = "";
        public string? Phone { get; set; } = "";
        public string? Mobile { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public string City { get; set; } = "";
        public string Country { get; set; } = "";

        public DateTime DateRegistered { get; set; } = DateTime.Now;
        public string PatientNumber { get; set; } = "";

        public string CreatedBy { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ModifiedBy { get; set; } = "";
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        public bool Status { get; set; } = false;
        public int Location { get; set; }

        public string? Cabgno { get; set; } = "";
        public string? MedicalRecordNo { get; set; } = "";

        // Nested models
        public PatientDetailDTO? PatientDetail { get; set; }
        public PatientSettingDTO? PatientSetting { get; set; }
        public PatientCorporateInfoDTO? PatientCorporateInfo { get; set; }
    }

    public class PatientDetailDTO
    {
        public string? PassportId { get; set; }
        public string? JobType { get; set; }
        public string? District { get; set; }
        public string? Province { get; set; }
        public string? ReferenceNumber { get; set; }
        public string? AgencyNo { get; set; }
        public string? Field3 { get; set; }
        public string? Field4 { get; set; }
    }

    public class PatientSettingDTO
    {
        public int? ReferenceId { get; set; }
        public short? RateTypeId { get; set; }

        public decimal Discount { get; set; }
        public decimal MaxDiscount { get; set; }

        public bool AlertByEmail { get; set; }
        public bool AlertByMobile { get; set; }
        public bool AlertByFax { get; set; }

        public bool AllowCredit { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal CreditAmount { get; set; }

        public string? CreditReference { get; set; }
        public string? Comments { get; set; }
        public string? ExternalSystemId { get; set; }
    }

    public class PatientCorporateInfoDTO
    {
        //public long Id { get; set; }

        public string? EmployeeId { get; set; }

        public string? NameofEmployee { get; set; }

        public string? Relation { get; set; }

        public string? Region { get; set; }

        public string? Division { get; set; }

        public string? Description { get; set; }
    }
}
