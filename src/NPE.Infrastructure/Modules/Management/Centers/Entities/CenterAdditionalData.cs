using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management.Centers;

[PrimaryKey("CenterID", "TagName")]
[Table("CenterAdditionalData")]
public partial class CenterAdditionalData
{
    [Key]
    public int CenterID { get; set; }
    [Key]
    public string TagName { get; set; }
    public string? TagValue { get; set; }

    public Center? Center { get; set; }
}
