using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management.Centers;

/// <summary>
/// Payment taken from patient for test(s) will be stored. 
/// </summary>
[Table("CenterCreditDetail")]
public partial class CenterCreditDetail
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("CaseID")]
    public long CaseId { get; set; }

    [Column("CenterID")]
    public int? CenterId { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime Dated { get; set; }

    /// <summary>
    /// 0 Cash,1 Credit Card, 2 Cheque, 3 Transfer to Patient Account, 4 For Wavied Off &lt;BR&gt;3 and 4  will not be available on screen but it will used to transfer Case balance to Patient Account if Credit is allowed there. 4 will be used to wavie off by managment
    /// </summary>
    public byte Method { get; set; }

    /// <summary>
    /// 0 For Advance, 1 for Due Received,2 for Adjustment
    /// </summary>
    public byte Type { get; set; }

    [Column(TypeName = "decimal(10, 0)")]
    public decimal Amount { get; set; }

    [Column(TypeName = "decimal(10, 0)")]
    public decimal CaseTotalAmount { get; set; }

    /// <summary>
    /// Cheque No or Credit Card No
    /// </summary>
    [Column("CNo")]
    [StringLength(64)]
    [Unicode(false)]
    public string? Cno { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Description { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? ModifiedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? ModifiedDate { get; set; }

    public Center? Center { get; set; }
}
