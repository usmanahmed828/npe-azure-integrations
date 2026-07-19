using Microsoft.EntityFrameworkCore;
using NPE.Infrastructure.Modules.Management.Centers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NPE.Infrastructure.Modules.Management.Consultant.Entities;

/// <summary>
/// Consultant is  doctor who have asked the patient for the Test.
/// </summary>
[Table("Consultant")]
[Index("Code", Name = "unique_Code", IsUnique = true)]
public partial class Consultant
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Company { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? Address { get; set; }

    public int? City { get; set; }

    public int? Country { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? Fax { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Description { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string CreatedBy { get; set; } = null!;

    [Column(TypeName = "smalldatetime")]
    public DateTime CreatedDate { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string ModifiedBy { get; set; } = null!;

    [Column(TypeName = "smalldatetime")]
    public DateTime ModifiedDate { get; set; }

    public bool Status { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? NegotiatedAndFinalayzedBy { get; set; }

    [Column("RegionID")]
    public int? RegionId { get; set; }

    public ConsultantSetting? ConsultantSetting { get; set; }
    public ICollection<CenterConsultant> CenterConsultants { get; set; } = new List<CenterConsultant>();
}
