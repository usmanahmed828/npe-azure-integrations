using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Common.Identity.Entities;

public partial class IlockIdentity
{
    public string? Type { get; set; }
    public int NextId { get; set; }
}
