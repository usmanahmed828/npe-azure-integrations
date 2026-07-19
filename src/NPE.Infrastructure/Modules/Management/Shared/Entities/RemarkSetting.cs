using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management;

[Table("RemarkSetting")]
public partial class RemarkSetting
{
    [Key]
    [Column("RemarkID")]
    public int RemarkId { get; set; }

    public bool? AdditionalInfo { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? AdditionalInfoValidationFields { get; set; }
}
