using Microsoft.EntityFrameworkCore;
using NPE.Core.Common.CaseNumbers;
using NPE.Core.Common.Identity;
using NPE.Core.Common.Context.Services;
using NPE.Core.Common.Tenancy.Services;
using NPE.Core.Modules.Cases.BusinessObjects;
using NPE.Core.Modules.Cases.DTOs;
using NPE.Core.Modules.Cases.Models;
using NPE.Core.Modules.Patients.BusinessObjects;
using NPE.Infrastructure.Common.Tenancy;
using NPE.Infrastructure.Modules.Cases.Entities;
using NPE.Infrastructure.Modules.Cases.Mapping;

namespace NPE.Infrastructure.Modules.Cases.Services
{
    public sealed class CaseService : ICaseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPatientService _patientService;
        private readonly IIdentityService _identityService;
        private readonly ICaseNumberService _caseNumberService;
        private readonly ICaseDiscountService _discountService;
        private readonly ICasePricingService _pricingService;
        private readonly ICaseReferenceBehaviourService _referenceBehaviourService;
        private readonly ICaseCoverageService _coverageService;
        private readonly ICurrentContextService _currentContextService;
        private readonly ITenantOwnershipResolver _tenantOwnershipResolver;

        public CaseService(
            ApplicationDbContext context,
            IPatientService patientService,
            IIdentityService identityService,
            ICaseNumberService caseNumberService,
            ICaseDiscountService discountService,
            ICasePricingService pricingService,
            ICaseReferenceBehaviourService referenceBehaviourService,
            ICaseCoverageService coverageService,
            ICurrentContextService currentContextService,
            ITenantOwnershipResolver tenantOwnershipResolver)
        {
            _context = context;
            _patientService = patientService;
            _identityService = identityService;
            _caseNumberService = caseNumberService;
            _discountService = discountService;
            _pricingService = pricingService;
            _referenceBehaviourService = referenceBehaviourService;
            _coverageService = coverageService;
            _currentContextService = currentContextService;
            _tenantOwnershipResolver = tenantOwnershipResolver;
        }

        #region CRUD
        public async Task<long> CreateAsync(CaseDTO dto)
        {
            #region Identity

            var caseId = await _identityService.ConsumeAsync<long>(dto.RegistrationLocation, IdentityTypes.PatientCase);
            var caseNumber = await _caseNumberService.ConsumeAsync(dto.RegistrationLocation, dto.RegistrationDate);

            dto.Id = caseId;
            dto.CaseNumber = caseNumber;

            #endregion

            #region Reference Behaviour.

            var behaviour = await _referenceBehaviourService.ResolveAsync(dto.ReferenceId);

            if (behaviour.IsCreditPayment)
            {
                if (dto.Discount > 0)
                {
                    throw new InvalidOperationException(
                        "Discount not allowed for credit references.");
                }

                dto.PaidAmount = 0;
            }

            #endregion

            #region Discount

            var discountRequest = new CaseDiscountRequest
            {
                RegistrationLocation = dto.RegistrationLocation,
                ReferenceId = dto.ReferenceId,
                ConsultantId = dto.ConsultantId,
                DiscountPercent = dto.Discount,
                IsManualDiscount = dto.Discount > 0
            };

            await _discountService.ValidateDiscountAsync(discountRequest);
            dto.Discount = await _discountService.ResolveDiscountAsync(discountRequest);

            #endregion

            #region Pricing

            var rateTypeId = await ResolveRateTypeAsync(dto);

            var pricingRequest = new CasePricingRequest
            {
                RegistrationLocation = dto.RegistrationLocation,
                ReferenceId = dto.ReferenceId,
                RateTypeId = rateTypeId,
                DiscountPercent = dto.Discount,
                Less = dto.Less,
                PaidAmount = dto.PaidAmount,
                Tests = dto.Details.Select(x => new TestPricingItem
                {
                    TestId = x.TestId,
                    ManualRate = x.Rate
                }).ToList()
            };

            var financials = await _pricingService.CalculateAsync(pricingRequest);

            #endregion

            #region Coverage

            var coverage = await _coverageService.ResolveAsync(new CaseCoverageRequest
            {
                ReferenceId = dto.ReferenceId,
                NetAmount = financials.NetAmount,
                PaidAmount = dto.PaidAmount
            });

            financials.CompanyPayable = coverage.CompanyPayable;
            financials.PatientPayable = coverage.PatientPayable;

            #endregion

            #region Parent Totals

            dto.TotalAmount = financials.TotalAmount;
            dto.NetAmount = financials.NetAmount;
            dto.Due = financials.Due;

            dto.Status = true;
            dto.Completed = false;

            dto.CreatedDate = DateTime.Now;
            dto.ModifiedDate = DateTime.Now;

            dto.CreatedBy ??= "Admin";
            dto.ModifiedBy ??= dto.CreatedBy;

            #endregion

            #region Save Header

            var entity = CaseMapper.ToEntity(dto);
            entity.ReferenceName = await ResolveReferenceNameAsync(dto.ReferenceId);
            entity.ConsultantName = await ResolveConsultantNameAsync(dto.ConsultantId);
            _context.Cases.Add(entity);

            #endregion

            #region Save Details

            if (dto.Details != null)
            {
                foreach (var item in dto.Details)
                {
                    var detailId = await _identityService.ConsumeAsync<long>(dto.RegistrationLocation, IdentityTypes.CaseDetail);

                    _context.CaseDetails.Add(new CaseDetail
                    {
                        Id = detailId,
                        CaseId = caseId,
                        TestId = item.TestId,
                        TestName = item.TestName,
                        Rate = item.Rate,
                        TestStatus = item.TestStatus,
                        ConductedAt = item.ConductedAt,
                        ReportingDate = item.ReportingDate == default ? DateTime.Now : item.ReportingDate,
                        TemplateId = item.TemplateId,
                        Comments = item.Comments,
                        IsDelayed = item.IsDelayed,
                        ConductedBy = item.ConductedBy,
                        ApprovedBy = item.ApprovedBy,
                        ExternalSystemDetailId = item.ExternalSystemDetailId,
                        Status = true,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    });
                }
            }

            #endregion

            #region Save Remarks

            if (dto.Remarks != null)
            {
                foreach (var item in dto.Remarks)
                {
                    var id = await _identityService.ConsumeAsync<long>(dto.RegistrationLocation, IdentityTypes.CaseRemark);

                    _context.CaseRemarks.Add(new CaseRemark
                    {
                        Id = id,
                        CaseId = caseId,
                        RemarkId = item.RemarkId,
                        Description = item.Description,
                        Type = item.Type,
                        Rate = item.Rate,
                        Status = true,
                        CreatedBy = dto.CreatedBy,
                        ModifiedBy = dto.ModifiedBy,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    });
                }
            }

            #endregion

            #region Save Clinical

            if (dto.ClinicalFindings != null)
            {
                foreach (var item in dto.ClinicalFindings)
                {
                    var id = await _identityService.ConsumeAsync<long>(dto.RegistrationLocation, IdentityTypes.CaseClinicalDetail);

                    _context.CaseClinicalDetails.Add(new CaseClinicalDetail
                    {
                        Id = id,
                        CaseId = caseId,
                        ClinicalDetailCode = item.ClinicalDetailCode,
                        Name = item.Name,
                        Description = item.Description,
                        CreatedBy = dto.CreatedBy,
                        ModifiedBy = dto.ModifiedBy,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    });
                }
            }

            #endregion

            #region Save Payment

            if (dto.PaidAmount.HasValue && dto.PaidAmount.Value > 0)
            {
                var payMethod = dto.PaymentMethod ?? PaymentMethod.Cash;
                var payType = dto.PaymentType ?? PaymentType.RegistrationPayment;

                CasePayment paymentEntity;
                if (payType == PaymentType.CorporatePayment)
                {
                    paymentEntity = await CreateCorporatePayment(caseId, dto.PaidAmount.Value, payMethod, dto.PaymentCno, dto.PaymentRemarks, dto.RegistrationLocation, dto.CreatedBy ?? "Admin");
                }
                else
                {
                    paymentEntity = await CreateRegistrationPayment(caseId, dto.PaidAmount.Value, payMethod, dto.PaymentCno, dto.PaymentRemarks, dto.RegistrationLocation, dto.CreatedBy ?? "Admin");
                }

                _context.CasePayments.Add(paymentEntity);
            }

            #endregion

            #region Save AdditionalSetting

            if (dto.AdditionalSetting != null)
            {
                _context.CaseAdditionalSettings.Add(
                    new CaseAdditionalSetting
                    {
                        CaseId = caseId,
                        SecondReferenceId = dto.AdditionalSetting.SecondReferenceId,
                        SecondReferenceName = dto.AdditionalSetting.SecondReferenceName,
                        SecondConsultantId = dto.AdditionalSetting.SecondConsultantId,
                        SecondConsultantName = dto.AdditionalSetting.SecondConsultantName,
                        MedicalRecordNo = dto.AdditionalSetting.MedicalRecordNo,
                        SampleReceivedFrom = dto.AdditionalSetting.SampleReceivedFrom,
                        SampleReceivedBy = dto.AdditionalSetting.SampleReceivedBy,
                        Ponumber = dto.AdditionalSetting.Ponumber,
                        CaseSettings = dto.AdditionalSetting.CaseSettings
                    });
            }

            #endregion

            #region Save CaseInfo

            if (dto.CaseInfo != null)
            {
                _context.CaseInfos.Add(
                    new CaseInfo
                    {
                        CaseId = caseId,
                        Server = dto.CaseInfo.Server,
                        ClientIp = dto.CaseInfo.ClientIp,
                        ClientName = dto.CaseInfo.ClientName,
                        UserId = dto.CaseInfo.UserId
                    });
            }

            #endregion

            #region Save CaseSetting

            if (dto.Setting != null)
            {
                _context.CaseSettings.Add(
                    new CaseSetting
                    {
                        CaseId = caseId,
                        IsCompleted = dto.Setting.IsCompleted,
                        IsAlertSent = dto.Setting.IsAlertSent,
                        IsEmailSent = dto.Setting.IsEmailSent,
                        BborderNumber = dto.Setting.BborderNumber,
                        Bbtype = dto.Setting.Bbtype,
                        Status = dto.Setting.Status,
                        Bbdin = dto.Setting.Bbdin,
                        Qrstring = dto.Setting.Qrstring,
                        Qrimage = dto.Setting.Qrimage
                    });
            }

            #endregion

            #region Split Financial

            if (behaviour.IsInsurance || behaviour.IsWelfare)
            {
                _context.CorporatePaymentFinancials.Add(new CorporatePaymentFinancial
                {
                    CaseId = caseId,
                    CaseNetAmount = financials.NetAmount,
                    CompanyAmount = coverage.CompanyPayable,
                    CompanyPaidAmount = 0,
                    CompanyBalance = coverage.CompanyPayable,
                    PatientAmount = coverage.PatientPayable,
                    PatientPaidAmount = financials.PaidAmount,
                    PatientBalance = coverage.PatientPayable - financials.PaidAmount
                });
            }

            #endregion

            #region Tenancy Support

            if (_tenantOwnershipResolver.IsPureSaaS())
            {
                var context = await _currentContextService.GetAsync();

                _context.CompanyCases.Add(CaseMapper.CompanyCaseToEntity(context.CompanyId, caseId));
            }

            #endregion

            return caseId;
        }
        public async Task UpdateAsync(CaseDTO dto)
        {
            var context = await _currentContextService.GetAsync();

            var entity = await _context.Cases
                .ApplyCaseOwnership(_context, _tenantOwnershipResolver, context.CompanyId)
                .Include(x => x.CaseDetails)
                .Include(x => x.CasePayments)
                .Include(x => x.CaseRemarks)
                .Include(x => x.CaseClinicalDetails)
                .SingleOrDefaultAsync(x => x.Id == dto.Id && x.Status);

            if (entity == null)
                throw new KeyNotFoundException("Case not found.");

            #region Parent Update

            entity.RegistrationDate = dto.RegistrationDate;
            entity.ReportingDate = dto.ReportingDate;
            entity.ReferenceId = dto.ReferenceId;
            entity.ReferenceName = dto.ReferenceName;
            entity.ConsultantId = dto.ConsultantId;
            entity.ConsultantName = dto.ConsultantName;
            entity.DestinationLocation = dto.DestinationLocation;
            entity.Discount = dto.Discount;
            entity.Less = dto.Less;
            entity.Comments = dto.Comments;
            entity.WithoutHistory = dto.WithoutHistory;
            entity.ModifiedBy = dto.ModifiedBy ?? "Admin";
            entity.ModifiedDate = DateTime.Now;

            #endregion

            #region Case Details

            if (dto.Details != null)
            {
                foreach (var item in dto.Details)
                {
                    var existing = entity.CaseDetails.FirstOrDefault(x => x.Id == item.Id);

                    if (existing == null)
                    {
                        var newId = await _identityService.ConsumeAsync<long>(entity.RegistrationLocation, IdentityTypes.CaseDetail);

                        entity.CaseDetails.Add(new CaseDetail
                        {
                            Id = newId,
                            CaseId = entity.Id,
                            TestId = item.TestId,
                            TestName = item.TestName,
                            Rate = item.Rate,
                            TestStatus = item.TestStatus,
                            ConductedAt = item.ConductedAt,
                            ReportingDate = item.ReportingDate,
                            TemplateId = item.TemplateId,
                            Comments = item.Comments,
                            IsDelayed = item.IsDelayed,
                            ConductedBy = item.ConductedBy,
                            ApprovedBy = item.ApprovedBy,
                            ExternalSystemDetailId = item.ExternalSystemDetailId,
                            Status = true,
                            CreatedBy = dto.ModifiedBy ?? "Admin",
                            ModifiedBy = dto.ModifiedBy ?? "Admin",
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        });
                    }
                    else
                    {
                        existing.TestId = item.TestId;
                        existing.TestName = item.TestName;
                        existing.Rate = item.Rate;
                        existing.TestStatus = item.TestStatus;
                        existing.ConductedAt = item.ConductedAt;
                        existing.ReportingDate = item.ReportingDate;
                        existing.TemplateId = item.TemplateId;
                        existing.Comments = item.Comments;
                        existing.IsDelayed = item.IsDelayed;
                        existing.ConductedBy = item.ConductedBy;
                        existing.ApprovedBy = item.ApprovedBy;
                        existing.ExternalSystemDetailId = item.ExternalSystemDetailId;
                        existing.Status = item.Status;
                        existing.ModifiedBy = dto.ModifiedBy ?? "Admin";
                        existing.ModifiedDate = DateTime.Now;
                    }
                }

                var incomingIds = dto.Details.Where(x => x.Id > 0).Select(x => x.Id).ToHashSet();

                foreach (var row in entity.CaseDetails)
                {
                    if (!incomingIds.Contains(row.Id))
                    {
                        row.Status = false;
                        row.ModifiedBy = dto.ModifiedBy ?? "Admin";
                        row.ModifiedDate = DateTime.Now;
                    }
                }
            }

            #endregion

            #region Remarks

            if (dto.Remarks != null)
            {
                foreach (var item in dto.Remarks)
                {
                    var existing = entity.CaseRemarks.FirstOrDefault(x => x.Id == item.Id);

                    if (existing == null)
                    {
                        var id = await _identityService.ConsumeAsync<long>(entity.RegistrationLocation, IdentityTypes.CaseRemark);

                        entity.CaseRemarks.Add(new CaseRemark
                        {
                            Id = id,
                            CaseId = entity.Id,
                            RemarkId = item.RemarkId,
                            Description = item.Description,
                            Type = item.Type,
                            Rate = item.Rate,
                            Status = true,
                            CreatedBy = dto.ModifiedBy ?? "Admin",
                            ModifiedBy = dto.ModifiedBy ?? "Admin",
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        });
                    }
                    else
                    {
                        existing.RemarkId = item.RemarkId;
                        existing.Description = item.Description;
                        existing.Type = item.Type;
                        existing.Rate = item.Rate;
                        existing.Status = item.Status;
                        existing.ModifiedBy = dto.ModifiedBy ?? "Admin";
                        existing.ModifiedDate = DateTime.Now;
                    }
                }
            }

            #endregion

            #region Clinical Findings

            if (dto.ClinicalFindings != null)
            {
                foreach (var item in dto.ClinicalFindings)
                {
                    var existing = entity.CaseClinicalDetails.FirstOrDefault(x => x.Id == item.Id);

                    if (existing == null)
                    {
                        var id = await _identityService.ConsumeAsync<long>(entity.RegistrationLocation, IdentityTypes.CaseClinicalDetail);

                        entity.CaseClinicalDetails.Add(new CaseClinicalDetail
                        {
                            Id = id,
                            CaseId = entity.Id,
                            ClinicalDetailCode = item.ClinicalDetailCode,
                            Name = item.Name,
                            Description = item.Description,
                            CreatedBy = dto.ModifiedBy ?? "Admin",
                            ModifiedBy = dto.ModifiedBy ?? "Admin",
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        });
                    }
                    else
                    {
                        existing.ClinicalDetailCode = item.ClinicalDetailCode;
                        existing.Name = item.Name;
                        existing.Description = item.Description;
                        existing.ModifiedBy = dto.ModifiedBy ?? "Admin";
                        existing.ModifiedDate = DateTime.Now;
                    }
                }
            }

            #endregion

            #region Recalculate Totals

            var oldNetAmount = entity.NetAmount;

            var detailsTotal = entity.CaseDetails.Where(x => x.Status).Sum(x => x.Rate);
            var remarksTotal = entity.CaseRemarks.Where(x => x.Status == true).Sum(x => x.Rate ?? 0);
            entity.TotalAmount = detailsTotal + remarksTotal;
            var discountAmount = entity.TotalAmount * entity.Discount / 100m;
            entity.NetAmount = entity.TotalAmount - discountAmount - entity.Less;

            if (entity.NetAmount < oldNetAmount)
            {
                var difference = oldNetAmount - entity.NetAmount;

                var originalPaymentMethod = entity.CasePayments.OrderBy(x => x.Dated).Select(x => (PaymentMethod)x.Method).FirstOrDefault();
                var chosenMethod = originalPaymentMethod;
                if (chosenMethod == default && dto.PaymentMethod.HasValue)
                {
                    chosenMethod = dto.PaymentMethod.Value;
                }

                var adjustmentPayment = await CreateAdjustmentPayment(
                    entity.Id, 
                    difference, 
                    chosenMethod, 
                    dto.PaymentCno, 
                    dto.PaymentRemarks ?? "Case update recalculation adjustment", 
                    entity.RegistrationLocation, 
                    dto.ModifiedBy ?? "Admin"
                );

                _context.CasePayments.Add(adjustmentPayment);
            }

            entity.PaidAmount = entity.CasePayments.Sum(x => x.Amount);
            entity.Due = entity.NetAmount - (entity.PaidAmount ?? 0);

            #endregion

            //await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(long id)
        {
            var context = await _currentContextService.GetAsync();

            var entity = await _context.Cases
                .ApplyCaseOwnership(_context, _tenantOwnershipResolver, context.CompanyId)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new KeyNotFoundException("Case not found.");

            _context.Cases.Remove(entity);
        }
        public async Task<CaseDTO?> GetByIdAsync(long id)
        {
            var context = await _currentContextService.GetAsync();

            var entity = await _context.Cases
                    .ApplyCaseOwnership(_context, _tenantOwnershipResolver, context.CompanyId)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .Include(x => x.Patient)
                    .Include(x => x.CaseDetails)
                    .Include(x => x.CasePayments)
                    .Include(x => x.CaseRemarks)
                    .Include(x => x.CaseClinicalDetails)
                    .Include(x => x.AdditionalSetting)
                    .Include(x => x.CaseInfo)
                    .Include(x => x.PaymentOnline)
                    .Include(x => x.CaseSetting)
                    .Include(x => x.CorporatePaymentFinancial)
                    .SingleOrDefaultAsync(x => x.Id == id && x.Status);

            return entity == null ? null : CaseMapper.ToDTO(entity);
        }
        //public async Task<IEnumerable<CaseDTO>> GetAllAsync()
        //{
        //    var list = await _context.Cases.AsNoTracking().ToListAsync();

        //    return list.Select(CaseMapper.ToDTO);
        //}
        #endregion

        public Task<string> GetNextCaseNumberAsync(int location, DateTime date)
        {
            return _caseNumberService.GetNextCaseNumberAsync(location, date);
        }

        #region PRIVATE HELPERS
        private async Task<int> ResolveRateTypeAsync(CaseDTO request)
        {
            if (request.ReferenceId.HasValue)
            {
                return await _context.References.Where(x => x.Id == request.ReferenceId.Value).Select(x => x.RateTypeId).SingleAsync();
            }

            return await _context.Centers.Where(x => x.Id == request.RegistrationLocation).Select(x => x.RateTypeId).SingleAsync();
        }
        private async Task<string> ResolveReferenceNameAsync(int? id)
        {
            if (!id.HasValue)
                return string.Empty;

            return await _context.References.Where(x => x.Id == id.Value).Select(x => x.Name).SingleAsync();
        }
        private async Task<string> ResolveConsultantNameAsync(int? id)
        {
            if (!id.HasValue)
                return string.Empty;

            return await _context.Consultants.Where(x => x.Id == id.Value).Select(x => x.Name).SingleAsync();
        }
        #endregion

        #region Update Parent
        public async Task<PatientCaseCreateResult> CreateWithPatientAsync(PatientCaseCreateDTO request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Patient == null)
                throw new InvalidOperationException("Patient data is required.");

            if (request.Case == null)
                throw new InvalidOperationException("Case data is required.");

            #region Create Patient

            var patientId = await _patientService.CreateAsync(request.Patient);

            #endregion

            #region Create Case

            request.Case.PatientId = patientId;
            var caseId = await CreateAsync(request.Case);

            #endregion

            return new PatientCaseCreateResult
            {
                PatientId = patientId,
                CaseId = caseId
            };
        }
        #endregion

        #region ADD CASE CHILDREN
        public async Task<SaveCaseResult> AddTestInCaseAsync(AddCaseChildrenRequest request)
        {
            var context = await _currentContextService.GetAsync();

            var entity = await _context.Cases
                .ApplyCaseOwnership(_context, _tenantOwnershipResolver, context.CompanyId)
                .SingleOrDefaultAsync(x => x.Id == request.CaseId && x.Status);

            if (entity == null)
                throw new KeyNotFoundException("Case not found.");

            var location = entity.RegistrationLocation;
            decimal addedAmount = 0;

            #region Add Details

            if (request.Details != null && request.Details.Count > 0)
            {
                // Load existing active tests for this case
                var existingTestIds = await _context.CaseDetails
                    .Where(x => x.CaseId == entity.Id && x.Status == true)
                    .Select(x => x.TestId)
                    .ToListAsync();

                foreach (var item in request.Details)
                {
                    // Skip duplicate test
                    if (existingTestIds.Contains(item.TestId))
                        continue;

                    var detailId = await _identityService
                        .ConsumeAsync<long>(location, IdentityTypes.CaseDetail);

                    var rate = item.Rate;

                    addedAmount += rate;

                    _context.CaseDetails.Add(new CaseDetail
                    {
                        Id = detailId,
                        CaseId = entity.Id,
                        TestId = item.TestId,
                        TestName = item.TestName,
                        Rate = rate,
                        TestStatus = 0,
                        ConductedAt = item.ConductedAt,
                        ReportingDate = item.ReportingDate == default
                            ? DateTime.Now
                            : item.ReportingDate,

                        TemplateId = item.TemplateId,
                        Comments = item.Comments,
                        IsDelayed = item.IsDelayed,
                        ConductedBy = item.ConductedBy,
                        ApprovedBy = item.ApprovedBy,
                        ExternalSystemDetailId = item.ExternalSystemDetailId,

                        CreatedBy = request.ModifiedBy ?? "Admin",
                        ModifiedBy = request.ModifiedBy ?? "Admin",
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Status = true
                    });

                    // Add to memory list to avoid duplicate insertion
                    existingTestIds.Add(item.TestId);

                    #region Instrument

                    if (item.Instrument != null)
                    {
                        var instId = await _identityService
                            .ConsumeAsync<long>(location, IdentityTypes.CaseDetail);

                        _context.CaseDetailInstruments.Add(
                            new CaseDetailInstrument
                            {
                                Id = instId,
                                CaseDetailId = detailId,
                                InstrumentId = item.Instrument.InstrumentId,
                                CreatedBy = request.ModifiedBy ?? "Admin",
                                CreatedDate = DateTime.Now
                            });
                    }

                    #endregion

                    #region Outsource

                    if (item.Outsource != null)
                    {
                        var outId = await _identityService
                            .ConsumeAsync<long>(location, IdentityTypes.CaseDetail);

                        _context.OutsourceCaseDetails.Add(
                            new OutsourceCaseDetail
                            {
                                Id = outId,
                                CaseDetailId = detailId,
                                DispatchId = item.Outsource.DispatchId,
                                DispatchDate = item.Outsource.DispatchDate,
                                DispatchStatus = item.Outsource.DispatchStatus,
                                DispatchClient = item.Outsource.DispatchClient
                            });
                    }

                    #endregion
                }
            }


            //if (request.Details != null && request.Details.Count > 0)
            //{
            //    foreach (var item in request.Details)
            //    {
            //        var detailId = await _identityService.ConsumeAsync(location, IdentityTypes.CaseDetail);

            //        var rate = item.Rate;
            //        addedAmount += rate;

            //        _context.CaseDetails.Add(new CaseDetail
            //        {
            //            Id = detailId,
            //            CaseId = entity.Id,
            //            TestId = item.TestId,
            //            TestName = item.TestName,
            //            Rate = rate,
            //            TestStatus = 0,
            //            ConductedAt = item.ConductedAt,
            //            ReportingDate = item.ReportingDate == default ? DateTime.Now : item.ReportingDate,
            //            TemplateId = item.TemplateId,
            //            Comments = item.Comments,
            //            IsDelayed = item.IsDelayed,
            //            ConductedBy = item.ConductedBy,
            //            ApprovedBy = item.ApprovedBy,
            //            ExternalSystemDetailId = item.ExternalSystemDetailId,
            //            CreatedBy = request.ModifiedBy ?? "Admin",
            //            ModifiedBy = request.ModifiedBy ?? "Admin",
            //            CreatedDate = DateTime.Now,
            //            ModifiedDate = DateTime.Now,
            //            Status = true
            //        });

            //        if (item.Instrument != null)
            //        {
            //            var instId = await _identityService.ConsumeAsync(location, IdentityTypes.CaseDetail);

            //            _context.CaseDetailInstruments.Add(new CaseDetailInstrument
            //            {
            //                Id = instId,
            //                CaseDetailId = detailId,
            //                InstrumentId = item.Instrument.InstrumentId,
            //                CreatedBy = request.ModifiedBy ?? "Admin",
            //                CreatedDate = DateTime.Now
            //            });
            //        }

            //        if (item.Outsource != null)
            //        {
            //            var outId = await _identityService.ConsumeAsync(location, IdentityTypes.CaseDetail);

            //            _context.OutsourceCaseDetails.Add(new OutsourceCaseDetail
            //            {
            //                Id = outId,
            //                CaseDetailId = detailId,
            //                DispatchId = item.Outsource.DispatchId,
            //                DispatchDate = item.Outsource.DispatchDate,
            //                DispatchStatus = item.Outsource.DispatchStatus,
            //                DispatchClient = item.Outsource.DispatchClient
            //            });
            //        }
            //    }
            //}

            //#endregion

            //#region Add Remarks

            //if (request.Remarks != null)
            //{
            //    foreach (var item in request.Remarks)
            //    {
            //        var id = await _identityService.ConsumeAsync(location, IdentityTypes.CaseDetail);

            //        addedAmount += item.Rate ?? 0;

            //        _context.CaseRemarks.Add(new CaseRemark
            //        {
            //            Id = id,
            //            CaseId = entity.Id,

            //            RemarkId = item.RemarkId,
            //            Description = item.Description,
            //            Type = item.Type,
            //            Rate = item.Rate,
            //            Status = true,
            //            CreatedBy = request.ModifiedBy ?? "Admin",
            //            ModifiedBy = request.ModifiedBy ?? "Admin",
            //            CreatedDate = DateTime.Now,
            //            ModifiedDate = DateTime.Now
            //        });
            //    }
            //}

            #endregion

            #region Add Clinical

            if (request.ClinicalFindings != null)
            {
                foreach (var item in request.ClinicalFindings)
                {
                    var id = await _identityService.ConsumeAsync<long>(location, IdentityTypes.CaseDetail);

                    _context.CaseClinicalDetails.Add(new CaseClinicalDetail
                    {
                        Id = id,
                        CaseId = entity.Id,
                        ClinicalDetailCode = item.ClinicalDetailCode,
                        Name = item.Name,
                        Description = item.Description,
                        CreatedBy = request.ModifiedBy ?? "Admin",
                        ModifiedBy = request.ModifiedBy ?? "Admin",
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    });
                }
            }

            #endregion

            #region Add Payment

            if (request.Payment != null && request.Payment.Amount > 0)
            {
                var payType = (PaymentType)request.Payment.Type;
                var payMethod = (PaymentMethod)request.Payment.Method;

                CasePayment paymentEntity;
                if (payType == PaymentType.DueReceive)
                {
                    paymentEntity = await CreateDueReceivePayment(entity.Id, request.Payment.Amount, payMethod, request.Payment.Cno, request.Payment.Description, location, request.ModifiedBy ?? "Admin");
                }
                else if (payType == PaymentType.CorporatePayment)
                {
                    paymentEntity = await CreateCorporatePayment(entity.Id, request.Payment.Amount, payMethod, request.Payment.Cno, request.Payment.Description, location, request.ModifiedBy ?? "Admin");
                }
                else if (payType == PaymentType.Adjustment)
                {
                    paymentEntity = await CreateAdjustmentPayment(entity.Id, request.Payment.Amount, payMethod, request.Payment.Cno, request.Payment.Description, location, request.ModifiedBy ?? "Admin");
                }
                else if (payType == PaymentType.DueReceiveOtherCenter)
                {
                    paymentEntity = await CreateDueReceiveOtherCenterPayment(entity.Id, request.Payment.Amount, payMethod, request.Payment.Cno, request.Payment.Description, location, request.ModifiedBy ?? "Admin");
                }
                else
                {
                    paymentEntity = await CreateRegistrationPayment(entity.Id, request.Payment.Amount, payMethod, request.Payment.Cno, request.Payment.Description, location, request.ModifiedBy ?? "Admin");
                }

                _context.CasePayments.Add(paymentEntity);
            }

            #endregion

            #region Recalculate Parent

            entity.TotalAmount += addedAmount;
            var discountAmount = entity.TotalAmount * entity.Discount / 100m;
            entity.NetAmount = entity.TotalAmount - discountAmount - entity.Less;
            var extraPaid = request.Payment?.Amount ?? 0;
            entity.PaidAmount = (entity.PaidAmount ?? 0) + extraPaid;
            entity.Due = entity.NetAmount - (entity.PaidAmount ?? 0);
            entity.ModifiedBy = request.ModifiedBy ?? "Admin";
            entity.ModifiedDate = DateTime.Now;

            #endregion

            await _context.SaveChangesAsync();

            return SaveCaseResult.Success(entity.Id);
        }
        #endregion

        #region Update Parent
        public async Task<SaveCaseResult> UpdatePatientCaseInfoAsync(UpdatePatientCaseInfoRequest request)
        {
            #region Load Entities

            var patient = await _context.Patients.SingleOrDefaultAsync(x => x.Id == request.Patient.Id && x.Status);

            if (patient == null)
                throw new KeyNotFoundException("Patient not found.");

            var entity = await _context.Cases.SingleOrDefaultAsync(x => x.Id == request.Case.Id && x.Status);

            if (entity == null)
                throw new KeyNotFoundException("Case not found.");

            #endregion

            #region Update Patient

            patient.FirstName = request.Patient.FirstName;
            patient.LastName = request.Patient.LastName;
            patient.Fhname = request.Patient.Fhname;
            patient.Sex = request.Patient.Sex;
            patient.DateOfBirth = request.Patient.DateOfBirth;
            patient.MaritalStatus = request.Patient.MaritalStatus;
            patient.BloodGroup = request.Patient.BloodGroup;
            patient.Nic = request.Patient.Nic;
            patient.Phone = request.Patient.Phone;
            patient.Mobile = request.Patient.Mobile;
            patient.Fax = request.Patient.Fax;
            patient.Email = request.Patient.Email;
            patient.Address = request.Patient.Address;
            patient.City = request.Patient.City;
            patient.Country = request.Patient.Country;
            patient.MedicalRecordNo = request.Patient.MedicalRecordNo;
            patient.ModifiedBy = request.Patient.ModifiedBy ?? "Admin";
            patient.ModifiedDate = DateTime.Now;

            #endregion

            #region Update Case Parent

            entity.RegistrationDate = request.Case.RegistrationDate;
            entity.ReportingDate = request.Case.ReportingDate;
            entity.ReferenceId = request.Case.ReferenceId;
            entity.ReferenceName = request.Case.ReferenceName;
            entity.ConsultantId = request.Case.ConsultantId;
            entity.ConsultantName = request.Case.ConsultantName;
            entity.DestinationLocation = request.Case.DestinationLocation;
            entity.Discount = request.Case.Discount;
            entity.Less = request.Case.Less;
            entity.Comments = request.Case.Comments;
            entity.WithoutHistory = request.Case.WithoutHistory;
            entity.ModifiedBy = request.Case.ModifiedBy ?? "Admin";
            entity.ModifiedDate = DateTime.Now;

            #endregion

            #region Recalculate Parent Totals

            var discountAmount = entity.TotalAmount * entity.Discount / 100m;
            entity.NetAmount = entity.TotalAmount - discountAmount - entity.Less;
            entity.Due = entity.NetAmount - (entity.PaidAmount ?? 0);

            #endregion

            await _context.SaveChangesAsync();

            return SaveCaseResult.Success(entity.Id);
        }
        #endregion

        #region Receive Due & Payment Helpers

        public async Task ReceiveDueAsync(long caseId, decimal receivedAmount, PaymentMethod method, string? cno = null, string? description = null, string? modifiedBy = null)
        {
            var context = await _currentContextService.GetAsync();

            var entity = await _context.Cases
                .ApplyCaseOwnership(_context, _tenantOwnershipResolver, context.CompanyId)
                .Include(x => x.CasePayments)
                .SingleOrDefaultAsync(x => x.Id == caseId && x.Status);

            if (entity == null)
                throw new KeyNotFoundException("Case not found.");

            if (entity.Due < receivedAmount)
            {
                throw new InvalidOperationException($"Received amount ({receivedAmount}) exceeds the outstanding due ({entity.Due}).");
            }

            var payment = await CreateDueReceivePayment(
                caseId, 
                receivedAmount, 
                method, 
                cno, 
                description ?? "Due receive payment", 
                entity.RegistrationLocation, 
                modifiedBy ?? "Admin"
            );

            _context.CasePayments.Add(payment);

            // Recalculate parent values
            entity.PaidAmount = (entity.PaidAmount ?? 0) + receivedAmount;
            entity.Due = entity.Due - receivedAmount;
            entity.ModifiedBy = modifiedBy ?? "Admin";
            entity.ModifiedDate = DateTime.Now;
        }

        private async Task<CasePayment> CreateRegistrationPayment(long caseId, decimal amount, PaymentMethod method, string? cno, string? description, int locationId, string createdBy)
        {
            var payId = await _identityService.ConsumeAsync<long>(locationId, IdentityTypes.CasePayment);

            return new CasePayment
            {
                Id = payId,
                CaseId = caseId,
                Amount = amount,
                Method = (byte)method,
                Type = (byte)PaymentType.RegistrationPayment,
                Cno = cno,
                Description = description,
                CenterId = locationId,
                CreatedBy = createdBy,
                ModifiedBy = createdBy,
                Dated = DateTime.Now,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        private async Task<CasePayment> CreateAdjustmentPayment(long caseId, decimal amount, PaymentMethod method, string? cno, string? description, int locationId, string modifiedBy)
        {
            var payId = await _identityService.ConsumeAsync<long>(locationId, IdentityTypes.CasePayment);

            return new CasePayment
            {
                Id = payId,
                CaseId = caseId,
                Amount = amount,
                Method = (byte)method,
                Type = (byte)PaymentType.Adjustment,
                Cno = cno,
                Description = description,
                CenterId = locationId,
                CreatedBy = modifiedBy,
                ModifiedBy = modifiedBy,
                Dated = DateTime.Now,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        private async Task<CasePayment> CreateDueReceivePayment(long caseId, decimal amount, PaymentMethod method, string? cno, string? description, int locationId, string modifiedBy)
        {
            var payId = await _identityService.ConsumeAsync<long>(locationId, IdentityTypes.CasePayment);

            return new CasePayment
            {
                Id = payId,
                CaseId = caseId,
                Amount = amount,
                Method = (byte)method,
                Type = (byte)PaymentType.DueReceive,
                Cno = cno,
                Description = description,
                CenterId = locationId,
                CreatedBy = modifiedBy,
                ModifiedBy = modifiedBy,
                Dated = DateTime.Now,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        private async Task<CasePayment> CreateCorporatePayment(long caseId, decimal amount, PaymentMethod method, string? cno, string? description, int locationId, string createdBy)
        {
            var payId = await _identityService.ConsumeAsync<long>(locationId, IdentityTypes.CasePayment);

            return new CasePayment
            {
                Id = payId,
                CaseId = caseId,
                Amount = amount,
                Method = (byte)method,
                Type = (byte)PaymentType.CorporatePayment,
                Cno = cno,
                Description = description,
                CenterId = locationId,
                CreatedBy = createdBy,
                ModifiedBy = createdBy,
                Dated = DateTime.Now,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        private async Task<CasePayment> CreateDueReceiveOtherCenterPayment(long caseId, decimal amount, PaymentMethod method, string? cno, string? description, int locationId, string createdBy)
        {
            var payId = await _identityService.ConsumeAsync<long>(locationId, IdentityTypes.CasePayment);

            return new CasePayment
            {
                Id = payId,
                CaseId = caseId,
                Amount = amount,
                Method = (byte)method,
                Type = (byte)PaymentType.DueReceiveOtherCenter,
                Cno = cno,
                Description = description,
                CenterId = locationId,
                CreatedBy = createdBy,
                ModifiedBy = createdBy,
                Dated = DateTime.Now,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        #endregion
    }
}
