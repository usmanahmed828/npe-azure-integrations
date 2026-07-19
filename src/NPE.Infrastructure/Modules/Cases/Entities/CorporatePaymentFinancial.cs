using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NPE.Infrastructure.Modules.Cases.Entities;

/// <summary>
/// Stores company/patient financial split for insured/corporate cases.
/// </summary>
public partial class CorporatePaymentFinancial
{
    [Key]
    [Column("CaseID")]
    public long CaseId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal CaseNetAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal CompanyAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal CompanyPaidAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal CompanyBalance { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PatientAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PatientPaidAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PatientBalance { get; set; }

    #region Navigation Properties

    public virtual Case Case { get; set; } = null!;

    #endregion
}