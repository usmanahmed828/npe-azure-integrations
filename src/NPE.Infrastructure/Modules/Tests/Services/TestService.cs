using Microsoft.EntityFrameworkCore;
using NPE.Core.Common.Identity;
using NPE.Core.Common.Context.Services;
using NPE.Core.Common.Tenancy.Services;
using NPE.Core.Modules.Tests.BusinessObjects;
using NPE.Core.Modules.Tests.Mapping;
using NPE.Core.Modules.Tests.Models;
using NPE.Infrastructure.Modules.Tests.Entities;
using NPE.Infrastructure.Modules.Tenancy.Entities;
using NPE.Infrastructure.Common.Tenancy;

namespace NPE.Infrastructure.Modules.Tests.Services
{
    public class TestService : ITestService
    {
        private readonly ApplicationDbContext _db;
        private readonly IIdentityService _identityService;
        private readonly ICurrentContextService _currentContextService;
        private readonly ITenantOwnershipResolver _tenantOwnershipResolver;

        public TestService(
            ApplicationDbContext db, 
            IIdentityService identityService,
            ICurrentContextService currentContextService,
            ITenantOwnershipResolver tenantOwnershipResolver)
        {
            _db = db;
            _identityService = identityService;
            _currentContextService = currentContextService;
            _tenantOwnershipResolver = tenantOwnershipResolver;
        }

        public async Task<TestDTO?> GetByIdAsync(int id)
        {
            var context = await _currentContextService.GetAsync();

            var entity = await _db.Tests
                .ApplyTestOwnership(_db, _tenantOwnershipResolver, context.CompanyId)
                .SingleOrDefaultAsync(x => x.ID == id);

            return entity?.ToCore();
        }

        public async Task<IEnumerable<TestDTO>> GetAllAsync()
        {
            var context = await _currentContextService.GetAsync();

            var items = await _db.Tests.AsNoTracking()
                .ApplyTestOwnership(_db, _tenantOwnershipResolver, context.CompanyId)
                .Select(t => t.ToCore())
                .ToListAsync();

            return items;
        }

        public async Task<TestDTO> CreateAsync(TestDTO model)
        {
            var entity = new Test();
            model.UpdateEntity(entity);

            _db.Tests.Add(entity);

            if (_tenantOwnershipResolver.IsPureSaaS())
            {
                var context = await _currentContextService.GetAsync();

                _db.CompanyTests.Add(TestMapper.ToEntity(context.CompanyId, entity.ID));
            }

            //await _db.SaveChangesAsync();

            return entity.ToCore();
        }

        public async Task<TestDTO> UpdateAsync(TestDTO model)
        {
            var context = await _currentContextService.GetAsync();

            var entity = await _db.Tests
                .ApplyTestOwnership(_db, _tenantOwnershipResolver, context.CompanyId)
                .SingleOrDefaultAsync(x => x.ID == model.Id)
                         ?? throw new KeyNotFoundException("Test not found");

            model.UpdateEntity(entity);
            //await _db.SaveChangesAsync();

            return entity.ToCore();
        }


       

        public async Task CloneBaselineTestsAsync(int targetClientId)
        {
            const int sourceClientId = 1001;
            var ClonedTestIDForProfile = new Dictionary<int, int>();

            bool AllowCloneNormalTest = false;
            if (AllowCloneNormalTest)
            {
                var NormalTests = await _db.Tests.AsNoTracking().AsSplitQuery().Where(t => t.ClientID == sourceClientId && t.Status == false && t.Type == 0)
                             .Include(t => t.TestSetting)
                             .Include(t => t.TestDetail)
                             .Include(t => t.TestInstrumentSettings)
                             .Include(t => t.LISSpecimenSettings)
                             .Include(t => t.TestNormalValues)
                               .ThenInclude(nv => nv.TestNormalValueGraph)
                           .ToListAsync();
                foreach (var newTest in NormalTests)
                {
                    int oldTestId = newTest.ID;
                    long newTestId = await _identityService.ConsumeAsync<int>(1001, IdentityTypes.Test);

                    newTest.ClientID = targetClientId;
                    newTest.ID = Convert.ToInt32(newTestId);
                    ClonedTestIDForProfile[oldTestId] = newTest.ID;

                    await MapStandardTestTables(newTest);

                    foreach (var nv in newTest.TestNormalValues)
                    {
                        long NormalValueID = await _identityService.ConsumeAsync<int>(1001, IdentityTypes.TestNormalValue);
                        nv.ID = Convert.ToInt32(NormalValueID);
                        nv.TestID = newTest.ID;

                        foreach (var graph in nv.TestNormalValueGraph)
                        {
                            long NormalValueGraphID = await _identityService.ConsumeAsync<int>(1001, IdentityTypes.TestNormalValueGraph);
                            graph.ID = Convert.ToInt32(NormalValueGraphID);
                            graph.TestNormalValueID = nv.ID;
                        }
                    }

                    _db.Tests.Add(newTest);
                    await _db.SaveChangesAsync();
                }
            }

            bool AllowCloneParameterTest = false;
            if (AllowCloneParameterTest)
            {
                var ParameterizedTests = await _db.Tests.AsNoTracking().AsSplitQuery().Where(t => t.ClientID == sourceClientId && t.Status == false && t.Type == 1)
                .Include(t => t.TestSetting)
                .Include(t => t.TestDetail)
                .Include(t => t.TestInstrumentSettings)
                .Include(t => t.LISSpecimenSettings)
                .Include(t => t.TestParameters)
                  .ThenInclude(nv => nv.TestParameterNormalValues)
                      .ThenInclude(nv => nv.TestNormalValueGraph)
                .ToListAsync();
                foreach (var newTest in ParameterizedTests)
                {
                    int oldTestId = newTest.ID;
                    long newTestId = await _identityService.ConsumeAsync<int>(1001, IdentityTypes.Test);

                    newTest.ClientID = targetClientId;
                    newTest.ID = Convert.ToInt32(newTestId);
                    ClonedTestIDForProfile[oldTestId] = newTest.ID;

                    await MapStandardTestTables(newTest);

                    foreach (var param in newTest.TestParameters)
                    {
                        long ParameterID = await _identityService.ConsumeAsync<int>(1001, IdentityTypes.TestParameter);
                        param.ID = Convert.ToInt32(ParameterID);
                        param.TestID = newTest.ID;

                        foreach (var pnv in param.TestParameterNormalValues)
                        {
                            long ParameterNormalValueID = await _identityService.ConsumeAsync<int>(1001, IdentityTypes.TestParameterNormalValue);
                            pnv.ID = Convert.ToInt32(ParameterNormalValueID);
                            pnv.TestParameterID = param.ID;

                            foreach (var graph in pnv.TestNormalValueGraph)
                            {
                                long NormalValueGraphID = await _identityService.ConsumeAsync<int>(1001, IdentityTypes.TestNormalValueGraph);
                                graph.ID = Convert.ToInt32(NormalValueGraphID);
                                graph.TestParameterNormalValueID = pnv.ID;
                            }
                        }
                    }

                    _db.Tests.Add(newTest);
                    await _db.SaveChangesAsync();
                }
            }

            bool AllowCloneProfileTest = true;
            if (AllowCloneProfileTest)
            {
                // 🚀 THE SMART CHECK: Only rebuild from DB if the dictionary is empty!
                if (!ClonedTestIDForProfile.Any())
                {
                    bool AllowClonedTestIDForProfileFromDB = true;
                    if (AllowClonedTestIDForProfileFromDB)
                    {
                        // 🚀 REBUILD THE DICTIONARY FROM THE DB
                        // Match Client 1001 tests to Client 2001 tests using their Code
                        var oldTests = await _db.Tests.Where(t => t.ClientID == sourceClientId).Select(t => new { t.ID, t.Code }).ToListAsync();
                        var newTests = await _db.Tests.Where(t => t.ClientID == targetClientId).Select(t => new { t.ID, t.Code }).ToListAsync();
                        foreach (var oldT in oldTests)
                        {
                            var matchedNew = newTests.FirstOrDefault(n => n.Code == oldT.Code);
                            if (matchedNew != null)
                            {
                                ClonedTestIDForProfile[oldT.ID] = matchedNew.ID; // Map restored!
                            }
                        }
                    }
                }


                var ProfilePackageTest = await _db.Tests.AsNoTracking().AsSplitQuery().Where(t => t.ClientID == sourceClientId && t.Status == false && (t.Type == 2 || t.Type==3))
                              .Include(t => t.TestSetting)
                              .Include(t => t.TestDetail)
                              .Include(t => t.TestInstrumentSettings)
                              .Include(t => t.LISSpecimenSettings)
                              .Include(t => t.TestProfile)
                            .ToListAsync();
                foreach (var newTest in ProfilePackageTest)
                {
                    int oldTestId = newTest.ID;
                    long newTestId = await _identityService.ConsumeAsync<int>(1001, IdentityTypes.Test);

                    newTest.ClientID = targetClientId;
                    newTest.ID = Convert.ToInt32(newTestId);

                    await MapStandardTestTables(newTest);

                    foreach (var profile in newTest.TestProfile)
                    {
                        profile.ProfileID = newTest.ID;

                        // Look up the NEW ID of the child test we already cloned in Phase 1 or 2
                        if (ClonedTestIDForProfile.TryGetValue(profile.TestID, out int newChildTestId))
                        {
                            profile.TestID = newChildTestId;
                        }
                    }

                    _db.Tests.Add(newTest);
                    await _db.SaveChangesAsync();
                }
            }

        }
        private async Task MapStandardTestTables(Test newTest)
        {
            if (newTest.TestSetting != null) newTest.TestSetting.TestID = newTest.ID;
            if (newTest.TestDetail != null) newTest.TestDetail.ID = newTest.ID;

            foreach (var inst in newTest.TestInstrumentSettings)
            {
                long InstrumentID = await _identityService.ConsumeAsync<int>(1001, IdentityTypes.TestInstrumentSetting);
                inst.ID = Convert.ToInt32(InstrumentID);
                inst.TestID = newTest.ID;
            }

            foreach (var specimen in newTest.LISSpecimenSettings)
            {
                long SpecimenSettingID = await _identityService.ConsumeAsync<int>(1001, IdentityTypes.LISSpecimenSetting);
                specimen.ID = Convert.ToInt32(SpecimenSettingID);
                specimen.TestID = newTest.ID;
            }
        }
    }
}
