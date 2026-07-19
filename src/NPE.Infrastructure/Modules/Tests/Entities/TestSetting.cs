using System;
using System.Collections.Generic;

namespace NPE.Infrastructure.Modules.Tests.Entities;

public partial class TestSetting
{
    public int TestID { get; set; }

    public bool? AllowSelection { get; set; }

    public string? DefultValue { get; set; }

    public string? SelectionList { get; set; }

    public int? Duration { get; set; }

    public int? Fetus { get; set; }

    public byte? Gender { get; set; }

    public bool? AdditionalInfo { get; set; }

    public string? AdditionalInfoValidationFields { get; set; }

    public string? RemarksCode { get; set; }

    public bool? IsHide { get; set; }

    public bool? VaccinationInfo { get; set; }

    public bool? VeterinaryInfo { get; set; }

    public short? SpeciesID { get; set; }

    public bool? IsAllowedResultEntry { get; set; }

    public bool? IsAllowedCardRecharge { get; set; }

    public bool? IsFamilyCardTest { get; set; }

    public bool? IsSingleCardTest { get; set; }

    public bool? IsRenewalCardTest { get; set; }

    public bool? IsConvertCardTest { get; set; }

    public bool? IsAllowDiscount { get; set; }

    public string? Settings { get; set; }

    // Tells TestSetting it belongs to a Test
    public virtual Test Test { get; set; } = null!;
}
