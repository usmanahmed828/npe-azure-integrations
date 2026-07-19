using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Patients.Entities;

/// <summary>
/// If patient belongs to an organization which is referring him/her to Lab then employee’s some job related detail should be recorded. This entity will used to store cooperate info. Note corporate info may be needed only if the Reference for the patient has been mentioned. 
/// </summary>
[Table("PatientCorporateInfo")]
public partial class PatientCorporateInfo
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    /// <summary>
    /// Employee Id of the Patient or the person who is referred to lab (if any)
    /// </summary>
    [Column("EmployeeID")]
    [StringLength(25)]
    [Unicode(false)]
    public string? EmployeeId { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? NameofEmployee { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Relation { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Region { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Division { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Description { get; set; } = null;
    public virtual Patient Patient { get; set; } = null!;
}
