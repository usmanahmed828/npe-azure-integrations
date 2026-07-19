using NPE.Infrastructure.Modules.Cases.Entities;
using System;
using System.Collections.Generic;

namespace NPE.Infrastructure.Modules.Patients.Entities;

/// <summary>
/// This entity deals with the patient who is coming first time. It contains one record per patient. Each patient will be assigned Unique Number which is called as Patient Number which will be a barcode. Patient Number generation will be written separately so that it can be changed per requirements. 
/// 
/// </summary>
public partial class Patient
{
    /// <summary>
    /// For Internal Use
    /// </summary>
    public long Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Fhname { get; set; } = null!;

    public byte Sex { get; set; }

    public DateTime DateOfBirth { get; set; }

    public byte MaritalStatus { get; set; }

    public string BloodGroup { get; set; } = null!;

    public string? Nic { get; set; }

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public DateTime DateRegistered { get; set; }

    public string PatientNumber { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public bool Status { get; set; }

    public int Location { get; set; }

    public string? Cabgno { get; set; }

    public string? MedicalRecordNo { get; set; }

    // Navigation properties (one-to-one)
    public virtual PatientDetail? PatientDetail { get; set; }
    public virtual PatientSetting? PatientSetting { get; set; }
    public virtual PatientCorporateInfo? PatientCorporateInfo { get; set; }
    public ICollection<Case> Cases { get; set; } = new List<Case>();
}
