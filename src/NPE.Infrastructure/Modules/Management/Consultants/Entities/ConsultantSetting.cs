using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management.Consultant.Entities;

[Table("ConsultantSetting")]
public partial class ConsultantSetting
{
    [Key]
    [Column("ConsultantID")]
    public int ConsultantId { get; set; }

    [Column(TypeName = "money")]
    public decimal Commission { get; set; }

    [Column("RateTypeID")]
    public int? RateTypeId { get; set; }

    [Column(TypeName = "money")]
    public decimal? MaxDiscount { get; set; }

    /// <summary>
    /// 1 = Commission on Net Amount, 
    /// 2 = Commission on Total Amount With Discount, 
    /// 3 = Commission on Total Amount With Rate Type
    /// </summary>
    public byte? CommissionCalculationMethod { get; set; }

    public bool? IsTestCountByFlightNumber { get; set; }

    public bool? SecondaryConsultant { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Speciality { get; set; }
}
