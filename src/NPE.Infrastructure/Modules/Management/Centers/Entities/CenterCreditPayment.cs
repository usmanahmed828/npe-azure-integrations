using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management.Centers;

[PrimaryKey("Id", "CenterId")]
[Table("CenterCreditPayment")]
public partial class CenterCreditPayment
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Key]
    [Column("CenterID")]
    public int CenterId { get; set; }

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    public byte Method { get; set; }

    [Column("CNo")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Cno { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ReceivedFrom { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ReceivedBy { get; set; } = null!;

    [Column(TypeName = "smalldatetime")]
    public DateTime ReceivedDate { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? Description { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string CreatedBy { get; set; } = null!;

    [Column(TypeName = "smalldatetime")]
    public DateTime CreatedDate { get; set; }
    public Center? Center { get; set; }
}
