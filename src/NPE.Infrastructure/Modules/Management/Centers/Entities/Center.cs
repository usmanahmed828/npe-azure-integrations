using Microsoft.EntityFrameworkCore;
using NPE.Infrastructure.Modules.Tenancy.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NPE.Infrastructure.Modules.Management.Centers;

[Table("Center")]
public partial class Center
{
    /// <summary>
    /// User will asign ID to the Center
    /// </summary>
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(1000)]
    [Unicode(false)]
    public string? Description { get; set; }

    /// <summary>
    /// 0 For Company Owned,1 ForFranchise 
    /// </summary>
    public byte Type { get; set; }

    public bool IsLab { get; set; }

    public bool IsCreditEnabled { get; set; }

    [Column(TypeName = "decimal(10, 0)")]
    public decimal CreditLimit { get; set; }

    public short CreditDays { get; set; }

    /// <summary>
    /// will be updated using trigger on Centerpayment
    /// </summary>
    [Column(TypeName = "decimal(10, 0)")]
    public decimal? Balance { get; set; }

    [Column("RateTypeID")]
    public int RateTypeId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Address { get; set; }

    public int? City { get; set; }

    public int? Country { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Fax { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ContactPerson { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ContactPhone { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? ContactMobile { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ContactEmail { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string CreatedBy { get; set; } = null!;

    [Column(TypeName = "smalldatetime")]
    public DateTime CreatedDate { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string ModifiedBy { get; set; } = null!;

    [Column(TypeName = "smalldatetime")]
    public DateTime ModifiedDate { get; set; }

    public bool Status { get; set; }

    [Column(TypeName = "money")]
    public decimal? Rebate { get; set; }

    [Column(TypeName = "money")]
    public decimal? SpecialDiscount { get; set; }

    [Column(TypeName = "money")]
    public decimal? CourierCharges { get; set; }
    public int CompanyId { get; set; }

    #region Navigation Properties
    public CenterSetting? CenterSetting { get; set; }
    public ICollection<CenterAdditionalData> CenterAdditionalDatas { get; set; } = new List<CenterAdditionalData>();
    public ICollection<CenterAdditionalInfo> CenterAdditionalInfos { get; set; } = new List<CenterAdditionalInfo>();
    public ICollection<CenterConsultant> CenterConsultants { get; set; } = new List<CenterConsultant>();
    public ICollection<CenterReference> CenterReferences { get; set; } = new List<CenterReference>();
    public ICollection<CenterCreditDetail> CenterCreditDetails { get; set; } = new List<CenterCreditDetail>();
    public ICollection<CenterCreditHistory> CenterCreditHistories { get; set; } = new List<CenterCreditHistory>();
    public ICollection<CenterCreditPayment> CenterCreditPayments { get; set; } = new List<CenterCreditPayment>();
    public CenterCreditSummary? CenterCreditSummary { get; set; }
    public CenterLabNo? CenterLabNo { get; set; }
    public ICollection<CenterOwnerCenter> CenterOwnerCenters { get; set; } = new List<CenterOwnerCenter>();
    public ICollection<CenterPayment> CenterPayments { get; set; } = new List<CenterPayment>();
    public ICollection<CenterReportLayoutMapping> CenterReportLayoutMappings { get; set; } = new List<CenterReportLayoutMapping>();
    public ICollection<CompanyCenter> CompanyCenters { get; set; } = new List<CompanyCenter>();
    public CentersToCheckDueAmountFor? DueAmountSetting { get; set; }
    #endregion
}
