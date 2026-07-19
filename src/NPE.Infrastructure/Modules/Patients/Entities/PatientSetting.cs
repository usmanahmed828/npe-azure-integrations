using System;
using System.Collections.Generic;

namespace NPE.Infrastructure.Modules.Patients.Entities;

/// <summary>
/// It will be used to store the information related patient like defaults which will be used further in case registration like reference or rate type. 
/// </summary>
public partial class PatientSetting
{
    public long Id { get; set; }

    /// <summary>
    /// Which 
    /// </summary>
    public int? ReferenceId { get; set; }

    public short? RateTypeId { get; set; }

    /// <summary>
    /// Default % of Discount, When Case will be registered
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Max % of discount which can be given to patient 
    /// </summary>
    public decimal MaxDiscount { get; set; }

    /// <summary>
    /// Indicator that will be used to send alert patient that your reports are ready or if some thing goes wrong
    /// </summary>
    public bool AlertByEmail { get; set; }

    public bool AlertByMobile { get; set; }

    public bool AlertByFax { get; set; }

    /// <summary>
    /// Either he is allowed to credit 
    /// </summary>
    public bool AllowCredit { get; set; }

    /// <summary>
    /// Max Amount allowed to patient for credit
    /// </summary>
    public decimal CreditLimit { get; set; }

    /// <summary>
    /// Current Balance of Patient. Calculated Field and will be controled using Triggers
    /// </summary>
    public decimal CreditAmount { get; set; }

    public string? CreditReference { get; set; }

    public string? Comments { get; set; }

    public string? Tmxid { get; set; }

    public string? ExternalSystemId { get; set; }

    // Navigation property to Patient
    public virtual Patient Patient { get; set; } = null!;
}
