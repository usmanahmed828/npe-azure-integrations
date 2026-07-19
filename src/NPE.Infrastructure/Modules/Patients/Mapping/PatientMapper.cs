using NPE.Core.Modules.Patients.BusinessObjects;
using NPE.Core.Modules.Patients.Models;
using NPE.Infrastructure.Modules.Patients.Entities;
using NPE.Infrastructure.Modules.Tenancy.Entities;
using System.Diagnostics.Metrics;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;

namespace NPE.Infrastructure.Modules.Patients.Mapping
{
    public static class PatientMapper
    {
        public static PatientDTO? ToCore(Patient entity)
        {
            if (entity == null) return null;

            return new PatientDTO
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Fhname = entity.Fhname,
                Sex = entity.Sex,
                DateOfBirth = entity.DateOfBirth,
                MaritalStatus = entity.MaritalStatus,
                BloodGroup = entity.BloodGroup,
                Nic = entity.Nic,
                Phone = entity.Phone,
                Mobile = entity.Mobile,
                Fax = entity.Fax,
                Email = entity.Email,
                Address = entity.Address,
                City = entity.City,
                Country = entity.Country,
                DateRegistered = entity.DateRegistered,
                PatientNumber = entity.PatientNumber,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate,
                Status = entity.Status,
                Location = entity.Location,
                Cabgno = entity.Cabgno,
                MedicalRecordNo = entity.MedicalRecordNo,
                PatientDetail = entity.PatientDetail == null ? null : new PatientDetailDTO
                {
                    PassportId = entity.PatientDetail.PassportId,
                    JobType = entity.PatientDetail.JobType,
                    District = entity.PatientDetail.District,
                    Province = entity.PatientDetail.Province,
                    ReferenceNumber = entity.PatientDetail.ReferenceNumber,
                    AgencyNo = entity.PatientDetail.AgencyNo,
                    Field3 = entity.PatientDetail.Field3,
                    Field4 = entity.PatientDetail.Field4
                },
                PatientSetting = entity.PatientSetting == null ? null : new PatientSettingDTO
                {
                    ReferenceId = entity.PatientSetting.ReferenceId,
                    RateTypeId = entity.PatientSetting.RateTypeId,
                    Discount = entity.PatientSetting.Discount,
                    MaxDiscount = entity.PatientSetting.MaxDiscount,
                    AlertByEmail = entity.PatientSetting.AlertByEmail,
                    AlertByMobile = entity.PatientSetting.AlertByMobile,
                    AlertByFax = entity.PatientSetting.AlertByFax,
                    AllowCredit = entity.PatientSetting.AllowCredit,
                    CreditLimit = entity.PatientSetting.CreditLimit,
                    CreditAmount = entity.PatientSetting.CreditAmount,
                    CreditReference = entity.PatientSetting.CreditReference,
                    Comments = entity.PatientSetting.Comments,
                    //Tmxid = entity.PatientSetting.Tmxid,
                    ExternalSystemId = entity.PatientSetting.ExternalSystemId
                },
                PatientCorporateInfo =
                    entity.PatientCorporateInfo == null
                    ? null
                    : new PatientCorporateInfoDTO
                    {
                        //Id = entity.PatientCorporateInfo.Id,
                        EmployeeId = entity.PatientCorporateInfo.EmployeeId,
                        NameofEmployee = entity.PatientCorporateInfo.NameofEmployee,
                        Relation = entity.PatientCorporateInfo.Relation,
                        Region = entity.PatientCorporateInfo.Region,
                        Division = entity.PatientCorporateInfo.Division,
                        Description = entity.PatientCorporateInfo.Description
                    }
            };
        }

        public static Patient ToEntity(PatientDTO core)
        {
            var entity = new Patient();

            entity.Id = core.Id;
            entity.FirstName = core.FirstName;
            entity.LastName = core.LastName;
            entity.Fhname = core.Fhname;
            entity.Sex = core.Sex;
            entity.DateOfBirth = core.DateOfBirth;
            entity.MaritalStatus = core.MaritalStatus;
            entity.BloodGroup = core.BloodGroup;
            entity.Nic = core.Nic;
            entity.Phone = core.Phone;
            entity.Mobile = core.Mobile;
            entity.Email = core.Email;
            entity.Address = core.Address;
            entity.City = core.City;
            entity.Country = core.Country;
            entity.DateRegistered = core.DateRegistered;
            entity.PatientNumber = core.PatientNumber;
            entity.CreatedBy = core.CreatedBy;
            entity.CreatedDate = core.CreatedDate;
            entity.Status = core.Status;
            entity.Location = core.Location;
            entity.ModifiedBy = core.ModifiedBy;
            entity.ModifiedDate = core.ModifiedDate;

            if (core.PatientDetail != null)
            {
                entity.PatientDetail = new PatientDetail
                {
                    PatientId = core.Id,
                    PassportId = core.PatientDetail.PassportId,
                    JobType = core.PatientDetail.JobType,
                    District = core.PatientDetail.District,
                    Province = core.PatientDetail.Province,
                    ReferenceNumber = core.PatientDetail.ReferenceNumber,
                    AgencyNo = core.PatientDetail.AgencyNo,
                    Field3 = core.PatientDetail.Field3,
                    Field4 = core.PatientDetail.Field4
                };
            }

            if (core.PatientSetting != null)
            {
                entity.PatientSetting = new PatientSetting
                {
                    Id = core.Id,
                    ReferenceId = core.PatientSetting.ReferenceId,
                    RateTypeId = core.PatientSetting.RateTypeId,
                    Discount = core.PatientSetting.Discount,
                    MaxDiscount = core.PatientSetting.MaxDiscount,
                    AlertByEmail = core.PatientSetting.AlertByEmail,
                    AlertByMobile = core.PatientSetting.AlertByMobile,
                    AlertByFax = core.PatientSetting.AlertByFax,
                    AllowCredit = core.PatientSetting.AllowCredit,
                    CreditLimit = core.PatientSetting.CreditLimit,
                    CreditReference = core.PatientSetting.CreditReference,
                    Comments = core.PatientSetting.Comments,
                    ExternalSystemId = core.PatientSetting.ExternalSystemId
                };
            }

            if (core.PatientCorporateInfo != null)
            {
                entity.PatientCorporateInfo =
                    new PatientCorporateInfo
                    {
                        Id = core.Id,
                        EmployeeId = core.PatientCorporateInfo.EmployeeId,
                        NameofEmployee = core.PatientCorporateInfo.NameofEmployee,
                        Relation = core.PatientCorporateInfo.Relation,
                        Region = core.PatientCorporateInfo.Region,
                        Division = core.PatientCorporateInfo.Division,
                        Description = core.PatientCorporateInfo.Description
                    };
            }

            return entity;
        }

        public static void UpdateEntity(Patient entity, PatientDTO core)
        {
            entity.FirstName = core.FirstName;
            entity.LastName = core.LastName;
            entity.Fhname = core.Fhname;
            entity.Sex = core.Sex;
            entity.DateOfBirth = core.DateOfBirth;
            entity.MaritalStatus = core.MaritalStatus;
            entity.BloodGroup = core.BloodGroup;
            entity.Nic = core.Nic;
            entity.Phone = core.Phone;
            entity.Mobile = core.Mobile;
            entity.Fax = core.Fax;
            entity.Email = core.Email;
            entity.Address = core.Address;
            entity.City = core.City;
            entity.Country = core.Country;
            //entity.DateRegistered = core.DateRegistered;
            //entity.PatientNumber = core.PatientNumber;
            entity.ModifiedBy = core.ModifiedBy;
            entity.ModifiedDate = DateTime.Now;
            entity.Status = core.Status;
            entity.Location = core.Location;
            entity.Cabgno = core.Cabgno;
            entity.MedicalRecordNo = core.MedicalRecordNo;

            if (core.PatientDetail != null)
            {
                if (entity.PatientDetail == null)
                {
                    //entity.PatientDetail = new PatientDetail { PatientId = entity.Id };
                    entity.PatientDetail ??= new PatientDetail();
                    entity.PatientDetail.PatientId = entity.Id;
                }

                entity.PatientDetail.PassportId = core.PatientDetail.PassportId;
                entity.PatientDetail.JobType = core.PatientDetail.JobType;
                entity.PatientDetail.District = core.PatientDetail.District;
                entity.PatientDetail.Province = core.PatientDetail.Province;
                entity.PatientDetail.ReferenceNumber = core.PatientDetail.ReferenceNumber;
                entity.PatientDetail.AgencyNo = core.PatientDetail.AgencyNo;
                entity.PatientDetail.Field3 = core.PatientDetail.Field3;
                entity.PatientDetail.Field4 = core.PatientDetail.Field4;
            }

            if (core.PatientSetting != null)
            {
                if (entity.PatientSetting == null)
                {
                    //entity.PatientSetting = new PatientSetting { Id = entity.Id };
                    entity.PatientSetting ??= new PatientSetting();
                    entity.PatientSetting.Id = entity.Id;
                }

                entity.PatientSetting.ReferenceId = core.PatientSetting.ReferenceId;
                entity.PatientSetting.RateTypeId = core.PatientSetting.RateTypeId;
                entity.PatientSetting.Discount = core.PatientSetting.Discount;
                entity.PatientSetting.MaxDiscount = core.PatientSetting.MaxDiscount;
                entity.PatientSetting.AlertByEmail = core.PatientSetting.AlertByEmail;
                entity.PatientSetting.AlertByMobile = core.PatientSetting.AlertByMobile;
                entity.PatientSetting.AlertByFax = core.PatientSetting.AlertByFax;
                entity.PatientSetting.AllowCredit = core.PatientSetting.AllowCredit;
                entity.PatientSetting.CreditLimit = core.PatientSetting.CreditLimit;
                entity.PatientSetting.CreditAmount = core.PatientSetting.CreditAmount;
                entity.PatientSetting.CreditReference = core.PatientSetting.CreditReference;
                entity.PatientSetting.Comments = core.PatientSetting.Comments;
                //entity.PatientSetting.Tmxid = core.PatientSetting.Tmxid;
                entity.PatientSetting.ExternalSystemId = core.PatientSetting.ExternalSystemId;
            }

            if (core.PatientCorporateInfo != null)
            {
                entity.PatientCorporateInfo
                    ??= new PatientCorporateInfo();

                entity.PatientCorporateInfo.Id =
                    entity.Id;

                entity.PatientCorporateInfo.EmployeeId =
                    core.PatientCorporateInfo.EmployeeId;

                entity.PatientCorporateInfo.NameofEmployee =
                    core.PatientCorporateInfo.NameofEmployee;

                entity.PatientCorporateInfo.Relation =
                    core.PatientCorporateInfo.Relation;

                entity.PatientCorporateInfo.Region =
                    core.PatientCorporateInfo.Region;

                entity.PatientCorporateInfo.Division =
                    core.PatientCorporateInfo.Division;

                entity.PatientCorporateInfo.Description =
                    core.PatientCorporateInfo.Description;
            }
        }

        public static CompanyPatient CompanyPatientToEntity(long companyId, long patientId)
        {
            return new CompanyPatient
            {
                CompanyId = (int)companyId,
                PatientId = patientId
            };
        }

        public static CompanyCase ToEntity(int companyId, long caseId)
        {
            return new CompanyCase
            {
                CompanyId = companyId,
                CaseId = caseId
            };
        }
    }
}