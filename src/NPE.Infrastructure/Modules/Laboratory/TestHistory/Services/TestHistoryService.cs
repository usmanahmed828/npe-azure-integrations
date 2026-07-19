using NPE.Core.Modules.Laboratory.TestHistory.BusinessObjects;
using NPE.Core.Modules.Laboratory.TestHistory.DTOs;
using NPE.Infrastructure.Common.Data;

namespace NPE.Infrastructure.Modules.Laboratory.TestHistory.Services
{
    public class TestHistoryService : ITestHistoryService
    {
        private readonly IStoredProcedureExecutor _spExecutor;

        public TestHistoryService(IStoredProcedureExecutor spExecutor)
        {
            _spExecutor = spExecutor;
        }

        public async Task<List<TestHistoryCaseDto>> LoadByPatientCaseForSimpleTestHistoryAsync(
            TestHistorySearchRequest request)
        {
            var (cases, tests) =
                await _spExecutor.ExecuteMultipleAsync<
                    TestHistoryCaseDto,
                    TestHistoryTestDto>(
                    "cproc_LoadByPatientCaseForSimpleTestHistory",
                    cmd =>
                    {
                        DbHelper.AddParam(cmd, "@PatientNumber", request.PatientNumber);
                        DbHelper.AddParam(cmd, "@PatientName", request.PatientName);
                        DbHelper.AddParam(cmd, "@Sex", request.Sex);
                        DbHelper.AddParam(cmd, "@BloodGroup", request.BloodGroup);
                        DbHelper.AddParam(cmd, "@Phone", request.Phone);
                        DbHelper.AddParam(cmd, "@NIC", request.NIC);

                        DbHelper.AddParam(cmd, "@RegistrationCenter", request.RegistrationCenter);
                        DbHelper.AddParam(cmd, "@RegistrationDateFrom", request.RegistrationDateFrom);
                        DbHelper.AddParam(cmd, "@RegistrationDateTo", request.RegistrationDateTo);
                        DbHelper.AddParam(cmd, "@FilterByDate", request.FilterByDate);

                        DbHelper.AddParam(cmd, "@CaseNumber", request.CaseNumber);
                        DbHelper.AddParam(cmd, "@TestCodeFrom", request.TestCodeFrom);
                        DbHelper.AddParam(cmd, "@TestCodeTo", request.TestCodeTo);
                        DbHelper.AddParam(cmd, "@TestName", request.TestName);
                        DbHelper.AddParam(cmd, "@TestStatus", request.TestStatus);

                        DbHelper.AddParam(cmd, "@CaseReglocation", request.CaseReglocation);
                        DbHelper.AddParam(cmd, "@ConsultantID", request.ConsultantID);
                        DbHelper.AddParam(cmd, "@ReferenceID", request.ReferenceID);

                        DbHelper.AddParam(cmd, "@CaseRegFromdate", request.CaseRegFromdate);
                        DbHelper.AddParam(cmd, "@CaseRegToDate", request.CaseRegToDate);
                        DbHelper.AddParam(cmd, "@CaseRegFilterByDate", request.CaseRegFilterByDate);

                        DbHelper.AddParam(cmd, "@PageNumber", request.PageNumber);
                        DbHelper.AddParam(cmd, "@PageSize", request.PageSize);

                        DbHelper.AddParam(cmd, "@Diagonosis", request.Diagonosis);
                        DbHelper.AddParam(cmd, "@Specimen", request.Specimen);
                        DbHelper.AddParam(cmd, "@BiopsyNo", request.BiopsyNo);
                        DbHelper.AddParam(cmd, "@CS", request.CS);

                        DbHelper.AddParam(cmd, "@MRNo", request.MRNo);
                        DbHelper.AddParam(cmd, "@CABGNo", request.CABGNo);

                        DbHelper.AddParam(cmd, "@IsSpecialCase", request.IsSpecialCase);
                        DbHelper.AddParam(cmd, "@TableName", request.TableName);
                        DbHelper.AddParam(cmd, "@CriticalResult", request.CriticalResult);
                    });

            // Build lookup for child tests
            var testLookup = tests
                .GroupBy(t => t.CaseID)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Attach tests to cases
            foreach (var c in cases)
            {
                c.Tests = testLookup.ContainsKey(c.CaseID)
                    ? testLookup[c.CaseID]
                    : new List<TestHistoryTestDto>();
            }

            return cases;
        }
    }
}