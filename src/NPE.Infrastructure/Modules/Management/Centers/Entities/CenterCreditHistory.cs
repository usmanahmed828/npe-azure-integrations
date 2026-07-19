using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management.Centers;

[Table("CenterCreditHistory")]
public partial class CenterCreditHistory
{
    [Key]
    [Column("CaseID")]
    public long CaseId { get; set; }

    [Column("CenterID")]
    public int CenterId { get; set; }

    [Column(TypeName = "money")]
    public decimal CaseTotalAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal CaseNetAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalCreditAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal CurrentCreditAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal PreviousTotalUsedAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal CurrentTotalUsedAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal PreviousUsedAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal CurrentUsedAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal PreviousBalance { get; set; }

    [Column(TypeName = "money")]
    public decimal CurrentBalance { get; set; }
    public Center? Center { get; set; }
}
