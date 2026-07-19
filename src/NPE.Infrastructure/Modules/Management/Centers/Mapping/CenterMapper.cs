using NPE.Core.Modules.Management.Center.Models;
using NPE.Core.Modules.Management.Consultant.DTOs;
using NPE.Core.Modules.Management.Reference.DTOs;
using NPE.Infrastructure.Modules.iLock.Entities;
using NPE.Infrastructure.Modules.Tenancy.Entities;

namespace NPE.Infrastructure.Modules.Management.Centers.Mapping
{
    public static class CenterMapper
    {
        public static CenterDTO ToDTO(Center entity)
        {
            var dto = new CenterDTO
            {
                Id = entity.Id,

                Name = entity.Name,
                Description = entity.Description,

                Type = entity.Type,

                IsLab = entity.IsLab,
                IsCreditEnabled = entity.IsCreditEnabled,

                CreditLimit = entity.CreditLimit,
                CreditDays = entity.CreditDays,

                Balance = entity.Balance,

                RateTypeId = entity.RateTypeId,

                Address = entity.Address,
                City = entity.City,
                Country = entity.Country,

                Phone = entity.Phone,
                Fax = entity.Fax,
                Email = entity.Email,

                ContactPerson = entity.ContactPerson,
                ContactPhone = entity.ContactPhone,
                ContactMobile = entity.ContactMobile,
                ContactEmail = entity.ContactEmail,

                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,

                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate,

                Status = entity.Status,

                Rebate = entity.Rebate,
                SpecialDiscount = entity.SpecialDiscount,
                CourierCharges = entity.CourierCharges,

                Setting = entity.CenterSetting == null ? null : new CenterSettingDTO
                {
                    CenterId = entity.CenterSetting.CenterId,

                    DestinationLocation = entity.CenterSetting.DestinationLocation,

                    DefaultStatus = entity.CenterSetting.DefaultStatus,

                    TransportTime = entity.CenterSetting.TransportTime,

                    IsCreditFeatureEnabled = entity.CenterSetting?.IsCreditFeatureEnabled ?? false,

                    CreditWarningLimit = entity.CenterSetting?.CreditWarningLimit
                },

                CreditSummary = entity.CenterCreditSummary == null ? null : new CenterCreditSummaryDTO
                {
                    TotalAmount = entity.CenterCreditSummary.TotalAmount,
                    TotalUsed = entity.CenterCreditSummary.TotalUsed,
                    CreditAmount = entity.CenterCreditSummary.CreditAmount,
                    CreditUsed = entity.CenterCreditSummary.CreditUsed,
                    CurrentBalance = entity.CenterCreditSummary.CurrentBalance
                },

                Consultants = entity.CenterConsultants
                            .Where(x => x.Consultant != null)
                            .Select(x =>
                                new ConsultantLookupDTO
                                {
                                    Id = x.Consultant!.Id,
                                    Name = x.Consultant.Name ?? ""
                                })
                            .ToList(),

                References = entity.CenterReferences.Where(x => x.Reference != null)
                            .Select(x =>
                                new ReferenceLookupDTO
                                {
                                    Id = x.Reference!.Id,
                                    Name = x.Reference.Name ?? "",
                                    RateTypeId = x.Reference.RateTypeId,
                                    MaxDiscount = x.Reference.MaxDiscount,
                                    DefaultDiscount = x.Reference.DefaultDiscount
                                })
                            .ToList()

            };

            dto.DefaultConsultantId = ResolveDefaultConsultant(entity);
            dto.DefaultReferenceId = ResolveDefaultReference(entity);

            return dto;
        }

        private static int? ResolveDefaultConsultant(Center center)
        {
            var value = center.CenterAdditionalDatas.FirstOrDefault(x => x.TagName == "DefaultConsultant");

            if (value == null)
                return null;

            return int.TryParse(value.TagValue, out var id) ? id : null;
        }

        private static int? ResolveDefaultReference(Center center)
        {
            var value = center.CenterAdditionalDatas.FirstOrDefault(x => x.TagName == "DefaultReference");

            if (value == null)
                return null;

            return int.TryParse(value.TagValue, out var id) ? id : null;
        }

        public static Center ToEntity(CenterDTO request)
        {
            return new Center
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description ?? "",

                Type = request.Type,
                IsLab = request.IsLab,
                IsCreditEnabled = request.IsCreditEnabled,

                CreditLimit = request.CreditLimit,
                CreditDays = request.CreditDays,
                Balance = request.Balance,

                RateTypeId = request.RateTypeId,

                Address = request.Address,
                City = request.City,
                Country = request.Country,

                Phone = request.Phone,
                Fax = request.Fax,
                Email = request.Email,

                ContactPerson = request.ContactPerson,
                ContactPhone = request.ContactPhone,
                ContactMobile = request.ContactMobile,
                ContactEmail = request.ContactEmail,

                CreatedBy = request.CreatedBy ?? "Administrator",
                CreatedDate = DateTime.Now,

                ModifiedBy = request.ModifiedBy ?? "Administrator",
                ModifiedDate = DateTime.Now,

                Status = request.Status,

                Rebate = request.Rebate,
                SpecialDiscount = request.SpecialDiscount,
                CourierCharges = request.CourierCharges,

                CompanyId = request.CompanyId
            };
        }

        public static void MapToExisting(CenterDTO source, Center destination)
        {
            destination.Name = source.Name;

            destination.Description = source.Description ?? "";

            destination.Type = source.Type;

            destination.IsLab = source.IsLab;

            destination.IsCreditEnabled = source.IsCreditEnabled;

            destination.CreditLimit = source.CreditLimit;

            destination.CreditDays = source.CreditDays;

            destination.Balance = source.Balance;

            destination.RateTypeId = source.RateTypeId;

            destination.Address = source.Address;

            destination.City = source.City;

            destination.Country = source.Country;

            destination.Phone = source.Phone;

            destination.Fax = source.Fax;

            destination.Email = source.Email;

            destination.ContactPerson = source.ContactPerson;

            destination.ContactPhone = source.ContactPhone;

            destination.ContactMobile = source.ContactMobile;

            destination.ContactEmail = source.ContactEmail;

            destination.Status = source.Status;

            destination.Rebate = source.Rebate;

            destination.SpecialDiscount = source.SpecialDiscount;

            destination.CourierCharges = source.CourierCharges;
        }

        public static CenterSetting CenterSettingToEntity(CenterSettingDTO setting)
        {
            ArgumentNullException.ThrowIfNull(setting);

            return new CenterSetting
            {
                CenterId = setting.CenterId,
                DestinationLocation = setting.DestinationLocation,
                DefaultStatus = setting.DefaultStatus,
                RegionId = setting.RegionId,
                TransportTime = setting.TransportTime,
                IsCreditFeatureEnabled = setting.IsCreditFeatureEnabled,
                CreditWarningLimit = setting.CreditWarningLimit
            };
        }

        public static void MapCenterSettingToExisting(CenterSettingDTO source, CenterSetting destination)
        {
            destination.DestinationLocation = source.DestinationLocation;
            destination.DefaultStatus = source.DefaultStatus;
            destination.RegionId = source.RegionId;
            destination.TransportTime = source.TransportTime;
            destination.IsCreditFeatureEnabled = source.IsCreditFeatureEnabled;
            destination.CreditWarningLimit = source.CreditWarningLimit;
        }

        public static ILockCompany ILockCompanyToEntity(long companyId, string Name, string Description)
        {
            return new ILockCompany
            {
                CompanyId = (int)companyId,
                Name = Name,
                Description = Description ?? "Default"
            };
        }

        public static CompanyCenter CompanyCenterToEntity(long companyId, long centerId, bool isRootCenter)
        {
            return new CompanyCenter
            {
                CompanyId = (int)companyId,
                CenterId = (int)centerId,
                IsRootCenter = isRootCenter
            };
        }

        public static CenterReference CenterReferenceToEntity(long centerId, long referenceId)
        {
            return new CenterReference
            {
                CenterId = (int)centerId,
                ReferenceId = (int)referenceId
            };
        }

        public static CenterConsultant CenterConsultantToEntity(long centerId, long consultantId)
        {
            return new CenterConsultant
            {
                CenterId = (int)centerId,
                ConsultantId = (int)consultantId
            };
        }

        public static List<CenterAdditionalData> CenterAdditionalDataToEntity(long centerId, long consultantId, long referenceId)
        {
            return new List<CenterAdditionalData>
        {
            new CenterAdditionalData
            {
                CenterID = (int)centerId,
                TagName = "DefaultConsultant",
                TagValue = consultantId.ToString()
            },
            new CenterAdditionalData
            {
                CenterID = (int)centerId,
                TagName = "DefaultReference",
                TagValue = referenceId.ToString()
            }
        };
        }
    }
}
