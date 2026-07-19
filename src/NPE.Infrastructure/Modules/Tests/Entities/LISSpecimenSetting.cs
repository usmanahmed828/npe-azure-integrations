using System;
using System.Collections.Generic;

namespace NPE.Infrastructure.Modules.Tests.Entities;

public partial class LISSpecimenSetting
{
    public int ID { get; set; }

    public int? SpecimenTypeID { get; set; }

    public int? TestID { get; set; }

    /// <summary>
    /// 1 = Yellow , 2 = Pupule , 3 = Green , 4 = SkyBlue , 5 = Gray , 7 = Urine
    /// </summary>
    public int? TubeType { get; set; }

    public int? TubeCount { get; set; }

    public bool? Status { get; set; }

    public virtual Test? Test { get; set; }
    public virtual LISSpecimenType? SpecimenType { get; set; }
}
