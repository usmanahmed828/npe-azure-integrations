using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.iLock.Entities;

[Table("iLock_User_Audit")]
public partial class ILockUserAudit
{
    [Key]
    [Column("audit_id")]
    public int AuditId { get; set; }

    [Column("audit_date", TypeName = "datetime")]
    public DateTime? AuditDate { get; set; }

    [Column("audit_type")]
    [StringLength(1)]
    [Unicode(false)]
    public string? AuditType { get; set; }

    [Column("audit_user")]
    [StringLength(50)]
    [Unicode(false)]
    public string? AuditUser { get; set; }

    [Column("CompanyId_a")]
    public int? CompanyIdA { get; set; }

    [Column("CompanyId_b")]
    public int? CompanyIdB { get; set; }

    [Column("UserId_a")]
    public int? UserIdA { get; set; }

    [Column("UserId_b")]
    public int? UserIdB { get; set; }

    [Column("UserName_a")]
    [StringLength(30)]
    [Unicode(false)]
    public string? UserNameA { get; set; }

    [Column("UserName_b")]
    [StringLength(30)]
    [Unicode(false)]
    public string? UserNameB { get; set; }

    [Column("FirstName_a")]
    [StringLength(30)]
    [Unicode(false)]
    public string? FirstNameA { get; set; }

    [Column("FirstName_b")]
    [StringLength(30)]
    [Unicode(false)]
    public string? FirstNameB { get; set; }

    [Column("LastName_a")]
    [StringLength(30)]
    [Unicode(false)]
    public string? LastNameA { get; set; }

    [Column("LastName_b")]
    [StringLength(30)]
    [Unicode(false)]
    public string? LastNameB { get; set; }

    [Column("Disabled_a")]
    public bool? DisabledA { get; set; }

    [Column("Disabled_b")]
    public bool? DisabledB { get; set; }

    [Column("Password_a")]
    [StringLength(50)]
    [Unicode(false)]
    public string? PasswordA { get; set; }

    [Column("Password_b")]
    [StringLength(50)]
    [Unicode(false)]
    public string? PasswordB { get; set; }

    [Column("Email_a")]
    [StringLength(50)]
    [Unicode(false)]
    public string? EmailA { get; set; }

    [Column("Email_b")]
    [StringLength(50)]
    [Unicode(false)]
    public string? EmailB { get; set; }

    [Column("Phone_a")]
    [StringLength(20)]
    [Unicode(false)]
    public string? PhoneA { get; set; }

    [Column("Phone_b")]
    [StringLength(20)]
    [Unicode(false)]
    public string? PhoneB { get; set; }

    [Column("Mobile_a")]
    [StringLength(20)]
    [Unicode(false)]
    public string? MobileA { get; set; }

    [Column("Mobile_b")]
    [StringLength(20)]
    [Unicode(false)]
    public string? MobileB { get; set; }

    [Column("Address_a")]
    [StringLength(50)]
    [Unicode(false)]
    public string? AddressA { get; set; }

    [Column("Address_b")]
    [StringLength(50)]
    [Unicode(false)]
    public string? AddressB { get; set; }

    [Column("City_a")]
    [StringLength(50)]
    [Unicode(false)]
    public string? CityA { get; set; }

    [Column("City_b")]
    [StringLength(50)]
    [Unicode(false)]
    public string? CityB { get; set; }

    [Column("Fax_a")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FaxA { get; set; }

    [Column("Fax_b")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FaxB { get; set; }

    [Column("State_a")]
    [StringLength(50)]
    [Unicode(false)]
    public string? StateA { get; set; }

    [Column("State_b")]
    [StringLength(50)]
    [Unicode(false)]
    public string? StateB { get; set; }

    [Column("Country_a")]
    [StringLength(50)]
    [Unicode(false)]
    public string? CountryA { get; set; }

    [Column("Country_b")]
    [StringLength(50)]
    [Unicode(false)]
    public string? CountryB { get; set; }

    [Column("CreatedBy_a")]
    [StringLength(30)]
    [Unicode(false)]
    public string? CreatedByA { get; set; }

    [Column("CreatedBy_b")]
    [StringLength(30)]
    [Unicode(false)]
    public string? CreatedByB { get; set; }

    [Column("CreatedDate_a", TypeName = "smalldatetime")]
    public DateTime? CreatedDateA { get; set; }

    [Column("CreatedDate_b", TypeName = "smalldatetime")]
    public DateTime? CreatedDateB { get; set; }

    [Column("ModifiedBy_a")]
    [StringLength(30)]
    [Unicode(false)]
    public string? ModifiedByA { get; set; }

    [Column("ModifiedBy_b")]
    [StringLength(30)]
    [Unicode(false)]
    public string? ModifiedByB { get; set; }

    [Column("modifiedDate_a", TypeName = "smalldatetime")]
    public DateTime? ModifiedDateA { get; set; }

    [Column("modifiedDate_b", TypeName = "smalldatetime")]
    public DateTime? ModifiedDateB { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? HostName { get; set; }

    [Column("HostIPAddress")]
    [StringLength(100)]
    [Unicode(false)]
    public string? HostIpaddress { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? AppName { get; set; }
}
