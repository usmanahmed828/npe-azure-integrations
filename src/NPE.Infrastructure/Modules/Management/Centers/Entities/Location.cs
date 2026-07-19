using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management.Centers;

public partial class Location
{
    [Key]
    [Column("LocationID")]
    public int LocationId { get; set; }

    [Column("CityID")]
    public int? CityId { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    public string? Name { get; set; }
}
