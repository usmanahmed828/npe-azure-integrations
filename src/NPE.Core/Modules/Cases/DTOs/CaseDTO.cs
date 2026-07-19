namespace NPE.Core.Modules.Cases.Models
{
    public class CaseDTO
    {
        public long Id { get; set; }
        public string CaseNumber { get; set; } = string.Empty;

        public long PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;

        public int RegistrationLocation { get; set; }
        public int DestinationLocation { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime ReportingDate { get; set; }

        public int? ReferenceId { get; set; }
        public string? ReferenceName { get; set; }

        public int? ConsultantId { get; set; }
        public string? ConsultantName { get; set; }
        public decimal TotalAmount { get; set; }
        public byte Discount { get; set; }
        public decimal Less { get; set; }
        public decimal NetAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BankPaid { get; set; }
        public decimal Due { get; set; }
        public decimal? DueReceived { get; set; }
        public decimal? BankDueReceived { get; set; }
        public string? Comments { get; set; }
        public bool Completed { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }
        public bool Status { get; set; }
        public bool AlertSent { get; set; }
        public bool? WithoutHistory { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public PaymentType? PaymentType { get; set; }
        public string? PaymentCno { get; set; }
        public string? PaymentRemarks { get; set; }
        public List<CaseDetailDTO> Details { get; set; } = new();
        public List<CasePaymentDTO> Payments { get; set; } = new();
        public CaseAdditionalSettingDTO? AdditionalSetting { get; set; }
        public List<CaseClinicalDetailDTO?> ClinicalFindings { get; set; } = new();
        public CaseInfoDTO? CaseInfo { get; set; }
        public CasePaymentOnlineDTO? PaymentOnline { get; set; }
        public List<CaseRemarkDTO?> Remarks { get; set; } = new();
        public CaseSettingDTO? Setting { get; set; }
        public CorporatePaymentFinancialDTO? CorporatePaymentFinancial { get; set; }
    }
    public sealed class CaseDetailDTO
    {
        public long Id { get; set; }

        public long CaseId { get; set; }

        public int TestId { get; set; }

        public string TestName { get; set; } = string.Empty;

        public decimal Rate { get; set; }

        public short TestStatus { get; set; }

        public int ConductedAt { get; set; }

        public DateTime ReportingDate { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; } = string.Empty;

        public DateTime ModifiedDate { get; set; }

        public bool Status { get; set; }

        public short TemplateId { get; set; }

        public string? Comments { get; set; }

        public bool IsDelayed { get; set; }

        public string? ConductedBy { get; set; }

        public string? ApprovedBy { get; set; }

        public string? ExternalSystemDetailId { get; set; }

        public DateTime? SyncDateTime { get; set; }

        #region Nested DTOs

        public CaseDetailInstrumentDTO? Instrument { get; set; }

        public OutsourceCaseDetailDTO? Outsource { get; set; }

        #endregion
    }
    public sealed class CasePaymentDTO
    {
        public long Id { get; set; }

        public long CaseId { get; set; }

        public DateTime Dated { get; set; }

        /// <summary>
        /// 0 Cash, 1 Card, 2 Cheque, 3 Transfer, 4 Waived Off
        /// </summary>
        public byte Method { get; set; }

        /// <summary>
        /// 0 Advance, 1 Due Received, 2 Adjustment
        /// </summary>
        public byte Type { get; set; }

        public decimal Amount { get; set; }

        public string? Cno { get; set; }

        public string? Description { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? CenterId { get; set; }
    }
    public sealed class CaseAdditionalSettingDTO
    {
        public long CaseId { get; set; }

        public int? SecondReferenceId { get; set; }
        public string? SecondReferenceName { get; set; }

        public int? SecondConsultantId { get; set; }
        public string? SecondConsultantName { get; set; }

        public string? MedicalRecordNo { get; set; }

        public string? SampleReceivedFrom { get; set; }
        public string? SampleReceivedBy { get; set; }

        public string? Ponumber { get; set; }

        public string? CaseSettings { get; set; }
    }
    public sealed class CaseClinicalDetailDTO
    {
        public long Id { get; set; }

        public long CaseId { get; set; }

        public string ClinicalDetailCode { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }
    }
    public sealed class CaseDetailInstrumentDTO
    {
        public long Id { get; set; }

        public long CaseDetailId { get; set; }

        public int InstrumentId { get; set; }

        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
    public sealed class CaseInfoDTO
    {
        public long CaseId { get; set; }

        public string Server { get; set; } = null!;

        public string ClientIp { get; set; } = null!;

        public string ClientName { get; set; } = null!;

        public long UserId { get; set; }
    }
    public sealed class CasePaymentOnlineDTO
    {
        public long Id { get; set; }

        public long CaseId { get; set; }

        public byte Method { get; set; }

        public decimal Amount { get; set; }

        public string ModifiedBy { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }

        public string? PaymentUrl { get; set; }

        public byte? IsReceived { get; set; }

        public bool? IsAlertSent { get; set; }

        public DateTime? AlertSentDate { get; set; }

        public int? PaymentType { get; set; }
    }
    public sealed class CaseRemarkDTO
    {
        public long Id { get; set; }

        public long CaseId { get; set; }

        public short RemarkId { get; set; }

        public string? Description { get; set; }

        public bool Type { get; set; }

        public decimal? Rate { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public bool? Status { get; set; }
    }
    public sealed class OutsourceCaseDetailDTO
    {
        public long Id { get; set; }

        public long CaseDetailId { get; set; }

        public long DispatchId { get; set; }

        public DateTime DispatchDate { get; set; }

        public short DispatchStatus { get; set; }

        public string DispatchClient { get; set; } = null!;
    }
    public sealed class CaseSettingDTO
    {
        public long CaseId { get; set; }

        public bool? IsCompleted { get; set; }

        public bool? IsAlertSent { get; set; }

        public bool? IsEmailSent { get; set; }

        public string? BborderNumber { get; set; }

        public string? Bbtype { get; set; }

        public bool? Status { get; set; }

        public string? Bbdin { get; set; }

        public string? Qrstring { get; set; }

        public byte[]? Qrimage { get; set; }
    }
    public sealed class CorporatePaymentFinancialDTO
    {
        public long CaseId { get; set; }
        public decimal CaseNetAmount { get; set; }
        public decimal CompanyAmount { get; set; }
        public decimal CompanyPaidAmount { get; set; }
        public decimal CompanyBalance { get; set; }
        public decimal PatientAmount { get; set; }
        public decimal PatientPaidAmount { get; set; }
        public decimal PatientBalance { get; set; }
    }

    public enum PaymentMethod : byte
    {
        Cash = 0,
        CreditCard = 1,
        Cheque = 2,
        BankDeposit = 3,
        OnlineTransfer = 4,
        JazzCash = 5,
        EasyPaisa = 6
    }

    public enum PaymentType : byte
    {
        RegistrationPayment = 0,
        DueReceive = 1,
        Adjustment = 2,
        DueReceiveOtherCenter = 3,
        CorporatePayment = 5
    }
}
