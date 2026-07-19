using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management;

[Table("City")]
public partial class City
{
    [Key]
    public int CityCode { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string CityName { get; set; } = null!;

    public int CountryCode { get; set; }

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
}
