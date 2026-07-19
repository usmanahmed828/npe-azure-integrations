using NPE.Core.Modules.Cases.Models;
using NPE.Infrastructure.Modules.Cases.Entities;
using NPE.Infrastructure.Modules.Tenancy.Entities;

namespace NPE.Infrastructure.Modules.Cases.Mapping;

public static class CaseMapper
{
    #region Main

    public static CaseDTO ToDTO(
        Case x)
    {
        return new CaseDTO
        {
            Id = x.Id,
            CaseNumber = x.CaseNumber,
            PatientId = x.PatientId,

            PatientName =
                x.Patient == null
                    ? string.Empty
                    : $"{x.Patient.FirstName} {x.Patient.LastName}"
                        .Trim(),

            RegistrationDate =
                x.RegistrationDate,

            ReportingDate =
                x.ReportingDate,

            ReferenceId =
                x.ReferenceId,

            ReferenceName =
                x.ReferenceName,

            ConsultantId =
                x.ConsultantId,

            ConsultantName =
                x.ConsultantName,

            RegistrationLocation =
                x.RegistrationLocation,

            DestinationLocation =
                x.DestinationLocation,

            TotalAmount =
                x.TotalAmount,

            Discount =
                x.Discount,

            Less =
                x.Less,

            NetAmount =
                x.NetAmount,

            PaidAmount =
                x.PaidAmount,

            Due =
                x.Due,

            BankPaid =
                x.BankPaid,

            DueReceived =
                x.DueReceived,

            BankDueReceived =
                x.BankDueReceived,

            Comments =
                x.Comments,

            Completed =
                x.Completed,

            CreatedBy =
                x.CreatedBy,

            CreatedDate =
                x.CreatedDate,

            ModifiedBy =
                x.ModifiedBy,

            ModifiedDate =
                x.ModifiedDate,

            Status =
                x.Status,

            AlertSent =
                x.AlertSent,

            WithoutHistory =
                x.WithoutHistory,

            Details =
                x.CaseDetails
                    .Select(ToDTO)
                    .ToList(),

            Payments =
                x.CasePayments
                    .Select(ToDTO)
                    .ToList(),

            Remarks =
                x.CaseRemarks
                    .Select(ToDTO)
                    .ToList(),

            ClinicalFindings =
                x.CaseClinicalDetails
                    .Select(ToDTO)
                    .ToList(),

            AdditionalSetting =
                x.AdditionalSetting == null
                    ? null
                    : ToDTO(
                        x.AdditionalSetting),

            CaseInfo =
                x.CaseInfo == null
                    ? null
                    : ToDTO(
                        x.CaseInfo),

            PaymentOnline =
                x.PaymentOnline == null
                    ? null
                    : ToDTO(
                        x.PaymentOnline),

            Setting =
                x.CaseSetting == null
                    ? null
                    : ToDTO(
                        x.CaseSetting),

            CorporatePaymentFinancial =
                x.CorporatePaymentFinancial == null
                    ? null
                    : ToDTO(
                        x.CorporatePaymentFinancial)
        };
    }

    public static Case ToEntity(
        CaseDTO x)
    {
        return new Case
        {
            Id = x.Id,
            CaseNumber = x.CaseNumber,
            PatientId = x.PatientId,

            RegistrationDate =
                x.RegistrationDate,

            ReportingDate =
                x.ReportingDate,

            ReferenceId =
                x.ReferenceId,

            ReferenceName =
                x.ReferenceName
                ?? string.Empty,

            ConsultantId =
                x.ConsultantId,

            ConsultantName =
                x.ConsultantName
                ?? string.Empty,

            RegistrationLocation =
                x.RegistrationLocation,

            DestinationLocation =
                x.DestinationLocation,

            TotalAmount =
                x.TotalAmount,

            Discount =
                x.Discount,

            Less =
                x.Less,

            NetAmount =
                x.NetAmount,

            PaidAmount =
                x.PaidAmount,

            Due =
                x.Due,

            Completed =
                x.Completed,

            Comments =
                x.Comments,

            CreatedBy =
                x.CreatedBy,

            CreatedDate =
                x.CreatedDate,

            ModifiedBy =
                x.ModifiedBy,

            ModifiedDate =
                x.ModifiedDate,

            Status =
                x.Status,

            BankPaid =
                x.BankPaid,

            DueReceived =
                x.DueReceived,

            BankDueReceived =
                x.BankDueReceived,

            AlertSent =
                x.AlertSent,

            WithoutHistory =
                x.WithoutHistory,

            //CaseDetails =
            //    x.Details?
            //        .Select(ToEntity)
            //        .ToList()
            //    ?? new List<CaseDetail>(),

            //CasePayments =
            //    x.Payments?
            //        .Select(ToEntity)
            //        .ToList()
            //    ?? new List<CasePayment>(),

            //CaseRemarks =
            //    x.Remarks?
            //        .Select(ToEntity)
            //        .ToList()
            //    ?? new List<CaseRemark>(),

            ////CaseClinicalDetails =
            ////    x.ClinicalFindings?
            ////        .Select(ToEntity)
            ////        .ToList()
            ////    ?? new List<CaseClinicalDetail>(),

            //AdditionalSetting =
            //    x.AdditionalSetting == null
            //        ? null
            //        : ToEntity(
            //            x.AdditionalSetting),

            //CaseInfo =
            //    x.CaseInfo == null
            //        ? null
            //        : ToEntity(
            //            x.CaseInfo),

            //PaymentOnline =
            //    x.PaymentOnline == null
            //        ? null
            //        : ToEntity(
            //            x.PaymentOnline),

            //CaseSetting =
            //    x.Setting == null
            //        ? null
            //        : ToEntity(
            //            x.Setting),

            //CorporatePaymentFinancial =
            //    x.CorporatePaymentFinancial == null
            //        ? null
            //        : ToEntity(
            //            x.CorporatePaymentFinancial)
        };
    }

    public static void UpdateEntity(
        Case target,
        CaseDTO source)
    {
        target.CaseNumber =
            source.CaseNumber;

        target.PatientId =
            source.PatientId;

        target.RegistrationDate =
            source.RegistrationDate;

        target.ReportingDate =
            source.ReportingDate;

        target.ReferenceId =
            source.ReferenceId;

        target.ReferenceName =
            source.ReferenceName
            ?? string.Empty;

        target.ConsultantId =
            source.ConsultantId;

        target.ConsultantName =
            source.ConsultantName
            ?? string.Empty;

        target.RegistrationLocation =
            source.RegistrationLocation;

        target.DestinationLocation =
            source.DestinationLocation;

        target.TotalAmount =
            source.TotalAmount;

        target.Discount =
            source.Discount;

        target.Less =
            source.Less;

        target.NetAmount =
            source.NetAmount;

        target.PaidAmount =
            source.PaidAmount;

        target.Due =
            source.Due;

        target.Completed =
            source.Completed;

        target.Comments =
            source.Comments;

        target.ModifiedBy =
            source.ModifiedBy;

        target.ModifiedDate =
            source.ModifiedDate;

        target.Status =
            source.Status;

        target.BankPaid =
            source.BankPaid;

        target.DueReceived =
            source.DueReceived;

        target.BankDueReceived =
            source.BankDueReceived;

        target.AlertSent =
            source.AlertSent;

        target.WithoutHistory =
            source.WithoutHistory;
    }

    #endregion

    #region CaseDetail

    private static CaseDetailDTO ToDTO(
        CaseDetail x)
    {
        return new CaseDetailDTO
        {
            Id = x.Id,
            CaseId = x.CaseId,
            TestId = x.TestId,
            TestName = x.TestName,
            Rate = x.Rate,
            TestStatus = x.TestStatus,
            ConductedAt = x.ConductedAt,
            ReportingDate = x.ReportingDate,
            CreatedBy = x.CreatedBy,
            CreatedDate = x.CreatedDate,
            ModifiedBy = x.ModifiedBy,
            ModifiedDate = x.ModifiedDate,
            Status = x.Status,
            TemplateId = x.TemplateId,
            Comments = x.Comments,
            IsDelayed = x.IsDelayed,
            ConductedBy = x.ConductedBy,
            ApprovedBy = x.ApprovedBy,
            ExternalSystemDetailId =
                x.ExternalSystemDetailId,
            SyncDateTime =
                x.SyncDateTime,

            //Instrument =
            //    x.CaseDetailInstrument == null
            //        ? null
            //        : ToDTO(
            //            x.CaseDetailInstrument),

            //Outsource =
            //    x.OutsourceCaseDetail == null
            //        ? null
            //        : ToDTO(
            //            x.OutsourceCaseDetail)
        };
    }

    private static CaseDetail ToEntity(
        CaseDetailDTO x)
    {
        return new CaseDetail
        {
            Id = x.Id,
            CaseId = x.CaseId,
            TestId = x.TestId,
            TestName = x.TestName,
            Rate = x.Rate,
            TestStatus = x.TestStatus,
            ConductedAt = x.ConductedAt,
            ReportingDate = x.ReportingDate,
            CreatedBy = x.CreatedBy,
            CreatedDate = x.CreatedDate,
            ModifiedBy = x.ModifiedBy,
            ModifiedDate = x.ModifiedDate,
            Status = x.Status,
            TemplateId = x.TemplateId,
            Comments = x.Comments,
            IsDelayed = x.IsDelayed,
            ConductedBy = x.ConductedBy,
            ApprovedBy = x.ApprovedBy,
            ExternalSystemDetailId =
                x.ExternalSystemDetailId,
            SyncDateTime =
                x.SyncDateTime,

            CaseDetailInstrument =
                x.Instrument == null
                    ? null
                    : ToEntity(
                        x.Instrument),

            OutsourceCaseDetail =
                x.Outsource == null
                    ? null
                    : ToEntity(
                        x.Outsource)
        };
    }

    #endregion

    #region CasePayment

    private static CasePaymentDTO ToDTO(
        CasePayment x)
    {
        return new CasePaymentDTO
        {
            Id = x.Id,
            CaseId = x.CaseId,
            Dated = x.Dated,
            Method = x.Method,
            Type = x.Type,
            Amount = x.Amount,
            Cno = x.Cno,
            Description = x.Description,
            CreatedBy = x.CreatedBy,
            CreatedDate = x.CreatedDate,
            ModifiedBy = x.ModifiedBy,
            ModifiedDate = x.ModifiedDate,
            CenterId = x.CenterId
        };
    }

    private static CasePayment ToEntity(
        CasePaymentDTO x)
    {
        return new CasePayment
        {
            Id = x.Id,
            CaseId = x.CaseId,
            Dated = x.Dated,
            Method = x.Method,
            Type = x.Type,
            Amount = x.Amount,
            Cno = x.Cno,
            Description = x.Description,
            CreatedBy = x.CreatedBy,
            CreatedDate = x.CreatedDate,
            ModifiedBy = x.ModifiedBy,
            ModifiedDate = x.ModifiedDate,
            CenterId = x.CenterId
        };
    }

    #endregion

    #region CaseRemark

    private static CaseRemarkDTO ToDTO(
        CaseRemark x)
    {
        return new CaseRemarkDTO
        {
            Id = x.Id,
            CaseId = x.CaseId,
            RemarkId = x.RemarkId,
            Description = x.Description,
            Type = x.Type,
            Rate = x.Rate,
            CreatedBy = x.CreatedBy,
            CreatedDate = x.CreatedDate,
            ModifiedBy = x.ModifiedBy,
            ModifiedDate = x.ModifiedDate,
            Status = x.Status
        };
    }

    private static CaseRemark ToEntity(
        CaseRemarkDTO x)
    {
        return new CaseRemark
        {
            Id = x.Id,
            CaseId = x.CaseId,
            RemarkId = x.RemarkId,
            Description = x.Description,
            Type = x.Type,
            Rate = x.Rate,
            CreatedBy = x.CreatedBy,
            CreatedDate = x.CreatedDate,
            ModifiedBy = x.ModifiedBy,
            ModifiedDate = x.ModifiedDate,
            Status = x.Status
        };
    }

    #endregion

    #region Clinical

    private static CaseClinicalDetailDTO ToDTO(
        CaseClinicalDetail x)
    {
        return new CaseClinicalDetailDTO
        {
            Id = x.Id,
            CaseId = x.CaseId,
            ClinicalDetailCode =
                x.ClinicalDetailCode,
            Name = x.Name,
            Description = x.Description,
            CreatedBy = x.CreatedBy,
            CreatedDate = x.CreatedDate,
            ModifiedBy = x.ModifiedBy,
            ModifiedDate = x.ModifiedDate
        };
    }

    private static CaseClinicalDetail ToEntity(
        CaseClinicalDetail x)
    {
        return new CaseClinicalDetail
        {
            Id = x.Id,
            CaseId = x.CaseId,
            ClinicalDetailCode =
                x.ClinicalDetailCode,
            Name = x.Name,
            Description = x.Description,
            CreatedBy = x.CreatedBy,
            CreatedDate = x.CreatedDate,
            ModifiedBy = x.ModifiedBy,
            ModifiedDate = x.ModifiedDate
        };
    }

    #endregion

    #region AdditionalSetting

    private static CaseAdditionalSettingDTO ToDTO(
        CaseAdditionalSetting x)
    {
        return new CaseAdditionalSettingDTO
        {
            CaseId = x.CaseId,
            SecondReferenceId =
                x.SecondReferenceId,
            SecondReferenceName =
                x.SecondReferenceName,
            SecondConsultantId =
                x.SecondConsultantId,
            SecondConsultantName =
                x.SecondConsultantName,
            MedicalRecordNo =
                x.MedicalRecordNo,
            SampleReceivedFrom =
                x.SampleReceivedFrom,
            SampleReceivedBy =
                x.SampleReceivedBy,
            Ponumber =
                x.Ponumber,
            CaseSettings =
                x.CaseSettings
        };
    }

    private static CaseAdditionalSetting ToEntity(
        CaseAdditionalSettingDTO x)
    {
        return new CaseAdditionalSetting
        {
            CaseId = x.CaseId,
            SecondReferenceId =
                x.SecondReferenceId,
            SecondReferenceName =
                x.SecondReferenceName,
            SecondConsultantId =
                x.SecondConsultantId,
            SecondConsultantName =
                x.SecondConsultantName,
            MedicalRecordNo =
                x.MedicalRecordNo,
            SampleReceivedFrom =
                x.SampleReceivedFrom,
            SampleReceivedBy =
                x.SampleReceivedBy,
            Ponumber =
                x.Ponumber,
            CaseSettings =
                x.CaseSettings
        };
    }

    #endregion

    #region CaseInfo

    private static CaseInfoDTO ToDTO(
        CaseInfo x)
    {
        return new CaseInfoDTO
        {
            CaseId = x.CaseId,
            UserId = x.UserId,
            Server = x.Server,
            ClientIp = x.ClientIp,
            ClientName = x.ClientName
        };
    }

    private static CaseInfo ToEntity(
        CaseInfoDTO x)
    {
        return new CaseInfo
        {
            CaseId = x.CaseId,
            UserId = x.UserId,
            Server = x.Server,
            ClientIp = x.ClientIp,
            ClientName = x.ClientName
        };
    }

    #endregion

    #region PaymentOnline

    private static CasePaymentOnlineDTO ToDTO(
        CasePaymentOnline x)
    {
        return new CasePaymentOnlineDTO
        {
            Id = x.Id,
            CaseId = x.CaseId,
            Method = x.Method,
            Amount = x.Amount,
            ModifiedBy = x.ModifiedBy,
            ModifiedDate = x.ModifiedDate,
            PaymentUrl = x.PaymentUrl,
            IsReceived = x.IsReceived,
            IsAlertSent = x.IsAlertSent,
            AlertSentDate = x.AlertSentDate,
            PaymentType = x.PaymentType
        };
    }

    private static CasePaymentOnline ToEntity(
        CasePaymentOnlineDTO x)
    {
        return new CasePaymentOnline
        {
            Id = x.Id,
            CaseId = x.CaseId,
            Method = x.Method,
            Amount = x.Amount,
            ModifiedBy = x.ModifiedBy,
            ModifiedDate = x.ModifiedDate,
            PaymentUrl = x.PaymentUrl,
            IsReceived = x.IsReceived,
            IsAlertSent = x.IsAlertSent,
            AlertSentDate = x.AlertSentDate,
            PaymentType = x.PaymentType
        };
    }

    #endregion

    #region Setting

    private static CaseSettingDTO ToDTO(
        CaseSetting x)
    {
        return new CaseSettingDTO
        {
            CaseId = x.CaseId,
            IsCompleted = x.IsCompleted,
            IsAlertSent = x.IsAlertSent,
            IsEmailSent = x.IsEmailSent,
            BborderNumber = x.BborderNumber,
            Bbtype = x.Bbtype,
            Status = x.Status,
            Bbdin = x.Bbdin,
            Qrstring = x.Qrstring,
            Qrimage = x.Qrimage
        };
    }

    private static CaseSetting ToEntity(
        CaseSettingDTO x)
    {
        return new CaseSetting
        {
            CaseId = x.CaseId,
            IsCompleted = x.IsCompleted,
            IsAlertSent = x.IsAlertSent,
            IsEmailSent = x.IsEmailSent,
            BborderNumber = x.BborderNumber,
            Bbtype = x.Bbtype,
            Status = x.Status,
            Bbdin = x.Bbdin,
            Qrstring = x.Qrstring,
            Qrimage = x.Qrimage
        };
    }

    #endregion

    #region Corporate

    private static CorporatePaymentFinancialDTO ToDTO(
        CorporatePaymentFinancial x)
    {
        return new CorporatePaymentFinancialDTO
        {
            CaseId = x.CaseId,
            CaseNetAmount = x.CaseNetAmount,
            CompanyAmount = x.CompanyAmount,
            CompanyPaidAmount = x.CompanyPaidAmount,
            CompanyBalance = x.CompanyBalance,
            PatientAmount = x.PatientAmount,
            PatientPaidAmount = x.PatientPaidAmount,
            PatientBalance = x.PatientBalance
        };
    }

    private static CorporatePaymentFinancial ToEntity(
        CorporatePaymentFinancialDTO x)
    {
        return new CorporatePaymentFinancial
        {
            CaseId = x.CaseId,
            CaseNetAmount = x.CaseNetAmount,
            CompanyAmount = x.CompanyAmount,
            CompanyPaidAmount = x.CompanyPaidAmount,
            CompanyBalance = x.CompanyBalance,
            PatientAmount = x.PatientAmount,
            PatientPaidAmount = x.PatientPaidAmount,
            PatientBalance = x.PatientBalance
        };
    }

    #endregion

    #region Instrument

    private static CaseDetailInstrumentDTO ToDTO(
        CaseDetailInstrument x)
    {
        return new CaseDetailInstrumentDTO
        {
            Id = x.Id,
            CaseDetailId = x.CaseDetailId,
            InstrumentId = x.InstrumentId,
            CreatedBy = x.CreatedBy,
            CreatedDate = x.CreatedDate,
            ModifiedBy = x.ModifiedBy,
            ModifiedDate = x.ModifiedDate
        };
    }

    private static CaseDetailInstrument ToEntity(
        CaseDetailInstrumentDTO x)
    {
        return new CaseDetailInstrument
        {
            Id = x.Id,
            CaseDetailId = x.CaseDetailId,
            InstrumentId = x.InstrumentId,
            CreatedBy = x.CreatedBy,
            CreatedDate = x.CreatedDate,
            ModifiedBy = x.ModifiedBy,
            ModifiedDate = x.ModifiedDate
        };
    }

    #endregion

    #region Outsource

    private static OutsourceCaseDetailDTO ToDTO(
        OutsourceCaseDetail x)
    {
        return new OutsourceCaseDetailDTO
        {
            Id = x.Id,
            CaseDetailId = x.CaseDetailId,
            //ClientId = x.ClientId,
            DispatchDate = x.DispatchDate,
            DispatchClient = x.DispatchClient
        };
    }

    private static OutsourceCaseDetail ToEntity(
        OutsourceCaseDetailDTO x)
    {
        return new OutsourceCaseDetail
        {
            Id = x.Id,
            CaseDetailId = x.CaseDetailId,
            //ClientId = x.ClientId,
            DispatchDate = x.DispatchDate,
            DispatchClient = x.DispatchClient
        };
    }

    #endregion

    public static CompanyCase CompanyCaseToEntity(int companyId, long caseId)
    {
        return new CompanyCase
        {
            CompanyId = companyId,
            CaseId = caseId
        };
    }
}