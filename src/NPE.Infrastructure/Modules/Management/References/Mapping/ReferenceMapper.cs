using NPE.Core.Modules.Management.Reference.DTOs;
using NPE.Infrastructure.Modules.Tenancy;

namespace NPE.Infrastructure.Modules.Management.Reference.Mapping
{
    public static class ReferenceMapper
    {
        public static ReferenceDTO ToDTO(Entities.Reference entity)
        {
            return new ReferenceDTO
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                Address = entity.Address,
                City = entity.City,
                Country = entity.Country,
                Phone = entity.Phone,
                Fax = entity.Fax,
                Email = entity.Email,
                RateTypeId = entity.RateTypeId,
                PaymentMode = entity.PaymentMode,
                CreditLimit = entity.CreditLimit,
                CreditDays = entity.CreditDays,
                CurrentBalance = entity.CurrentBalance,
                DefaultDiscount = entity.DefaultDiscount,
                MaxDiscount = entity.MaxDiscount,
                Description = entity.Description,
                ContactPerson = entity.ContactPerson,
                ContactPhone = entity.ContactPhone,
                ContactMobile = entity.ContactMobile,
                ContactEmail = entity.ContactEmail,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate,
                Status = entity.Status,

                Setting = entity.ReferenceSetting == null ? null : new ReferenceSettingDTO
                {
                    IsPrescriptionEnabled = entity.ReferenceSetting.IsPrescriptionEnabled,
                    IsCouponEnabled = entity.ReferenceSetting.IsCouponEnabled,
                    IsExtendedSearchEnabled = entity.ReferenceSetting.IsExtendedSearchEnabled,
                    IsLoyaltyCardEnabled = entity.ReferenceSetting.IsLoyaltyCardEnabled,
                    IsOutsourceRequestEnabled = entity.ReferenceSetting.IsOutsourceRequestEnabled,
                    CourierName = entity.ReferenceSetting.CourierName,
                    IsAllowReportAccess = entity.ReferenceSetting.IsAllowReportAccess,
                    SecondaryReference = entity.ReferenceSetting.SecondaryReference,
                    AdditionalInfo = entity.ReferenceSetting.AdditionalInfo,
                    AdditionalInfoValidationFields = entity.ReferenceSetting.AdditionalInfoValidationFields,
                    Settings = entity.ReferenceSetting.Settings,
                    Status = entity.ReferenceSetting.Status
                }
            };
        }

        public static Entities.Reference ToEntity(ReferenceDTO request)
        {
            return new Entities.Reference
            {
                Id = request.Id,
                Code = request.Code,
                Name = request.Name,
                Address = request.Address ?? string.Empty,
                City = request.City,
                Country = request.Country,
                Phone = request.Phone,
                Fax = request.Fax,
                Email = request.Email,
                RateTypeId = request.RateTypeId == 0 ? (short)1 : request.RateTypeId,
                PaymentMode = request.PaymentMode,
                CreditLimit = request.CreditLimit,
                CreditDays = request.CreditDays,
                CurrentBalance = request.CurrentBalance,
                DefaultDiscount = request.DefaultDiscount,
                MaxDiscount = request.MaxDiscount,
                Description = request.Description,
                ContactPerson = request.ContactPerson,
                ContactPhone = request.ContactPhone,
                ContactMobile = request.ContactMobile,
                ContactEmail = request.ContactEmail,
                CreatedBy = string.IsNullOrWhiteSpace(request.CreatedBy) ? "Administrator" : request.CreatedBy,
                CreatedDate = request.CreatedDate == default ? DateTime.Now : request.CreatedDate,
                ModifiedBy = string.IsNullOrWhiteSpace(request.ModifiedBy) ? "Administrator" : request.ModifiedBy,
                ModifiedDate = request.ModifiedDate == default ? DateTime.Now : request.ModifiedDate,
                Status = request.Status
            };
        }

        public static void MapToExisting(ReferenceDTO source, Entities.Reference destination)
        {
            destination.Code = source.Code;
            destination.Name = source.Name;
            destination.Address = source.Address;
            destination.City = source.City;
            destination.Country = source.Country;
            destination.Phone = source.Phone;
            destination.Fax = source.Fax;
            destination.Email = source.Email;
            destination.RateTypeId = source.RateTypeId;
            destination.PaymentMode = source.PaymentMode;
            destination.CreditLimit = source.CreditLimit;
            destination.CreditDays = source.CreditDays;
            destination.CurrentBalance = source.CurrentBalance;
            destination.DefaultDiscount = source.DefaultDiscount;
            destination.MaxDiscount = source.MaxDiscount;
            destination.Description = source.Description;
            destination.ContactPerson = source.ContactPerson;
            destination.ContactPhone = source.ContactPhone;
            destination.ContactMobile = source.ContactMobile;
            destination.ContactEmail = source.ContactEmail;
            destination.Status = source.Status;
        }

        public static Entities.ReferenceSetting ReferenceSettingToEntity(ReferenceSettingDTO setting)
        {
            return new Entities.ReferenceSetting
            {
                IsPrescriptionEnabled = setting.IsPrescriptionEnabled,
                IsCouponEnabled = setting.IsCouponEnabled,
                IsExtendedSearchEnabled = setting.IsExtendedSearchEnabled,
                IsLoyaltyCardEnabled = setting.IsLoyaltyCardEnabled,
                IsOutsourceRequestEnabled = setting.IsOutsourceRequestEnabled,
                CourierName = setting.CourierName,
                IsAllowReportAccess = setting.IsAllowReportAccess,
                SecondaryReference = setting.SecondaryReference,
                AdditionalInfo = setting.AdditionalInfo,
                AdditionalInfoValidationFields = setting.AdditionalInfoValidationFields,
                Settings = setting.Settings,
                Status = setting.Status
            };
        }

        public static void MapReferenceSettingToExisting(ReferenceSettingDTO source, Entities.ReferenceSetting destination)
        {
            destination.IsPrescriptionEnabled = source.IsPrescriptionEnabled;
            destination.IsCouponEnabled = source.IsCouponEnabled;
            destination.IsExtendedSearchEnabled = source.IsExtendedSearchEnabled;
            destination.IsLoyaltyCardEnabled = source.IsLoyaltyCardEnabled;
            destination.IsOutsourceRequestEnabled = source.IsOutsourceRequestEnabled;
            destination.CourierName = source.CourierName;
            destination.IsAllowReportAccess = source.IsAllowReportAccess;
            destination.SecondaryReference = source.SecondaryReference;
            destination.AdditionalInfo = source.AdditionalInfo;
            destination.AdditionalInfoValidationFields = source.AdditionalInfoValidationFields;
            destination.Settings = source.Settings;
            destination.Status = source.Status;
        }

        public static CompanyReference CompanyReferenceToEntity(long companyId, long referenceId)
        {
            return new CompanyReference
            {
                CompanyId = (int)companyId,
                ReferenceId = (int)referenceId
            };
        }
    }
}
