using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management.Centers;

public partial class CenterLabNo
{
    [Key]
    public int CenterCode { get; set; }

    public int MaxLabNo { get; set; }
    public Center? Center { get; set; }
}
