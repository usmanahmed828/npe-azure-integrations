using NPE.Core.Modules.Management.Consultant.DTOs;
using NPE.Infrastructure.Modules.Tenancy;

namespace NPE.Infrastructure.Modules.Management.Consultant.Mapping
{
    public static class ConsultantMapper
    {
        public static Entities.Consultant ToEntity(ConsultantDto dto)
        {
            return new Entities.Consultant
            {
                Id = dto.Id,
                Code = string.IsNullOrWhiteSpace(dto.Code) ? $"DEF{dto.Id}" : dto.Code,
                Name = string.IsNullOrWhiteSpace(dto.Name) ? "Default Doctor" : dto.Name,
                Company = string.IsNullOrWhiteSpace(dto.Company) ? "." : dto.Company,
                Address = dto.Address,
                City = dto.City,
                Country = dto.Country,
                Mobile = dto.Mobile,
                Phone = dto.Phone,
                Fax = dto.Fax,
                Email = dto.Email,
                Description = dto.Description,
                Status = dto.Status,
                CreatedBy = string.IsNullOrWhiteSpace(dto.CreatedBy) ? "Administrator" : dto.CreatedBy,
                CreatedDate = dto.CreatedDate == default ? DateTime.Now : dto.CreatedDate,
                ModifiedBy = string.IsNullOrWhiteSpace(dto.ModifiedBy) ? "Administrator" : dto.ModifiedBy,
                ModifiedDate = dto.ModifiedDate == default ? DateTime.Now : dto.ModifiedDate,
                RegionId = dto.RegionId
            };
        }

        public static void MapToExisting(ConsultantDto source, Entities.Consultant destination)
        {
            destination.Code = string.IsNullOrWhiteSpace(source.Code) ? $"DEF{destination.Id}" : source.Code;
            destination.Name = string.IsNullOrWhiteSpace(source.Name) ? "Default Doctor" : source.Name;
            destination.Company = string.IsNullOrWhiteSpace(source.Company) ? "." : source.Company;
            destination.Address = source.Address;
            destination.City = source.City;
            destination.Country = source.Country;
            destination.Mobile = source.Mobile;
            destination.Phone = source.Phone;
            destination.Fax = source.Fax;
            destination.Email = source.Email;
            destination.Description = source.Description;
            destination.Status = source.Status;
            destination.ModifiedBy = string.IsNullOrWhiteSpace(source.ModifiedBy) ? destination.ModifiedBy : source.ModifiedBy;
            destination.ModifiedDate = DateTime.Now;
            destination.RegionId = source.RegionId;
        }

        public static ConsultantDto ToDTO(Entities.Consultant entity)
        {
            return new ConsultantDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                Company = entity.Company,
                Address = entity.Address,
                City = entity.City,
                Country = entity.Country,
                Mobile = entity.Mobile,
                Phone = entity.Phone,
                Fax = entity.Fax,
                Email = entity.Email,
                Description = entity.Description,
                Status = entity.Status,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate,
                RegionId = entity.RegionId,

                Setting = entity.ConsultantSetting == null ? null : new ConsultantSettingDto
                {
                    ConsultantId = entity.Id,
                    Commission = entity.ConsultantSetting.Commission,
                    RateTypeId = entity.ConsultantSetting.RateTypeId,
                    MaxDiscount = entity.ConsultantSetting.MaxDiscount,
                    CommissionCalculationMethod = entity.ConsultantSetting.CommissionCalculationMethod,
                    IsTestCountByFlightNumber = entity.ConsultantSetting.IsTestCountByFlightNumber,
                    SecondaryConsultant = entity.ConsultantSetting.SecondaryConsultant,
                    Speciality = entity.ConsultantSetting.Speciality
                }
            };
        }

        public static Entities.ConsultantSetting ConsultantSettingToEntity(ConsultantSettingDto setting)
        {
            return new Entities.ConsultantSetting
            {
                ConsultantId = setting.ConsultantId,
                Commission = setting.Commission,
                RateTypeId = setting.RateTypeId,
                MaxDiscount = setting.MaxDiscount,
                CommissionCalculationMethod = setting.CommissionCalculationMethod,
                IsTestCountByFlightNumber = setting.IsTestCountByFlightNumber,
                SecondaryConsultant = setting.SecondaryConsultant,
                Speciality = setting.Speciality
            };
        }

        public static void MapConsultantSettingToExisting(ConsultantSettingDto source, Entities.ConsultantSetting destination)
        {
            destination.Commission = source.Commission;
            destination.RateTypeId = source.RateTypeId;
            destination.MaxDiscount = source.MaxDiscount;
            destination.CommissionCalculationMethod = source.CommissionCalculationMethod;
            destination.IsTestCountByFlightNumber = source.IsTestCountByFlightNumber;
            destination.SecondaryConsultant = source.SecondaryConsultant;
            destination.Speciality = source.Speciality;
        }

        public static CompanyConsultant CompanyConsultantToEntity(long companyId, long consultantId)
        {
            return new CompanyConsultant
            {
                CompanyId = (int)companyId,
                ConsultantId = (int)consultantId
            };
        }
    }
}