using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management;

[Table("ReferenceAgrement")]
public partial class ReferenceAgrement
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("ReferenceID")]
    public long ReferenceId { get; set; }

    [StringLength(250)]
    public string? Remarks { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FilePath { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FileName { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? FileExtension { get; set; }

    [Column("isPDF")]
    public bool? IsPdf { get; set; }
}
