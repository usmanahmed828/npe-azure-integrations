using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management.Centers;

[Table("CenterCreditSummary")]
public partial class CenterCreditSummary
{
    [Key]
    [Column("CenterID")]
    public int CenterId { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalUsed { get; set; }

    [Column(TypeName = "money")]
    public decimal CreditAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal CreditUsed { get; set; }

    [Column(TypeName = "money")]
    public decimal CurrentBalance { get; set; }
    public Center? Center { get; set; }
}
