using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management;

[Table("ReferenceBillSetting")]
[Index("Date", "ReferenceId", Name = "IX_ReferenceBillSetting", IsUnique = true)]
public partial class ReferenceBillSetting
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ReferenceID")]
    [StringLength(64)]
    [Unicode(false)]
    public string? ReferenceId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? InvoiceDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? BillDate { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? InvoiceNo { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? Date { get; set; }

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
}
