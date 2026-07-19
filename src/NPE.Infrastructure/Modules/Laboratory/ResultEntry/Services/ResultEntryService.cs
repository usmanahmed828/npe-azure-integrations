using Microsoft.EntityFrameworkCore;
using NPE.Core.Modules.Laboratory.ResultEntry.BusinessObjects;
using NPE.Core.Modules.Laboratory.ResultEntry.DTOs;
using NPE.Infrastructure.Common.Data;
using System.Collections.Generic;
using static NPE.Core.Common.Security.Permissions;

namespace NPE.Infrastructure.Modules.Laboratory.ResultEntry.Services
{
    public class ResultEntryService : IResultEntryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IStoredProcedureExecutor _spExecutor;

        public ResultEntryService(ApplicationDbContext context, IStoredProcedureExecutor spExecutor)
        {
            _context = context;
            _spExecutor = spExecutor;
        }

        //public async Task<List<ResultEntryMappedResponse>> LoadPatientAndCaseInfoForTestProcessAsync(
        //    ResultEntrySearchRequest request)
        //{
        //    var response = new ResultEntrySearchResponse();

        //    using var conn = _context.Database.GetDbConnection();
        //    await conn.OpenAsync();

        //    using var cmd = conn.CreateCommand();

        //    cmd.CommandText = "cproc_LoadPatientAndCaseInfoForTestProcess";
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //    AddParameter(cmd, "@Fromdate", request.FromDate);
        //    AddParameter(cmd, "@ToDate", request.ToDate);
        //    AddParameter(cmd, "@FilterByDate", request.FilterByDate);
        //    AddParameter(cmd, "@TestCodeFrom", request.TestCodeFrom);
        //    AddParameter(cmd, "@TestCodeTo", request.TestCodeTo);
        //    AddParameter(cmd, "@TestName", request.TestName);
        //    AddParameter(cmd, "@CaseNumber", request.CaseNumber);
        //    AddParameter(cmd, "@Registrationlocation", request.RegistrationLocation);
        //    AddParameter(cmd, "@UserID", request.UserID);
        //    AddParameter(cmd, "@TestStatus", request.TestStatus);
        //    AddParameter(cmd, "@IsDelayed", request.IsDelayed);
        //    AddParameter(cmd, "@ConnectedCenters", request.ConnectedCenters);
        //    AddParameter(cmd, "@ForConnectedCenters", request.ForConnectedCenters);
        //    AddParameter(cmd, "@IsSendToHeadOffice", request.IsSendToHeadOffice);
        //    AddParameter(cmd, "@IsForAbnormalValue", request.IsForAbnormalValue);
        //    AddParameter(cmd, "@ConductedAt", request.ConductedAt);
        //    AddParameter(cmd, "@Consultant", request.Consultant);
        //    AddParameter(cmd, "@Reference", request.Reference);
        //    //AddParameter(cmd, "@PatientName", request.PatientName);

        //    using var reader = await cmd.ExecuteReaderAsync();

        //    // RESULT SET 1 = Patients
        //    while (await reader.ReadAsync())
        //    {
        //        response.Patients.Add(new ResultEntryPatientDto
        //        {
        //            Id = Convert.ToInt64(reader["ID"]),
        //            PName = reader["PName"]?.ToString() ?? "",
        //            FHName = reader["FHName"]?.ToString() ?? "",
        //            AgeSex = reader["AgeSex"]?.ToString() ?? "",
        //            BloodGroup = reader["BloodGroup"]?.ToString() ?? "",
        //            CaseID = Convert.ToInt64(reader["CaseID"]),
        //            CaseNumber = reader["CaseNumber"]?.ToString() ?? "",
        //            RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"]),
        //            RegLoc = Convert.ToInt32(reader["RegLoc"]),
        //            CenterName = reader["CenterName"]?.ToString() ?? "",
        //            RemarkID = Convert.ToInt32(reader["RemarkID"])

        //        });
        //    }

        //    // RESULT SET 2 = Tests
        //    if (await reader.NextResultAsync())
        //    {
        //        while (await reader.ReadAsync())
        //        {
        //            response.Tests.Add(new ResultEntryTestDto
        //            {
        //                ID = Convert.ToInt64(reader["ID"]),
        //                CaseID = Convert.ToInt64(reader["CaseID"]),
        //                TestID = Convert.ToInt64(reader["TestID"]),
        //                TestName = reader["TestName"]?.ToString() ?? "",
        //                TestStatus = Convert.ToInt32(reader["TestStatus"]),
        //                CaseTestStatus = reader["CaseTestStatus"]?.ToString() ?? "",
        //                PerformedAt = reader["PerformedAt"]?.ToString() ?? "",
        //                Code = reader["Code"]?.ToString() ?? "",
        //                Comments = reader["Comments"]?.ToString() ?? "",
        //                TemplateID = Convert.ToInt32(reader["TemplateID"]),
        //                DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
        //                IsDelayed = Convert.ToBoolean(reader["IsDelayed"]),
        //                ConductedAt = Convert.ToInt32(reader["ConductedAt"])
        //            });
        //        }
        //    }

        //    return ResultEntryMapper.Map(response);
        //}

        public async Task<List<ResultEntryMappedResponse>>
        LoadPatientAndCaseInfoForTestProcessAsync(ResultEntrySearchRequest request)
        {
            (List<ResultEntryPatientDto> patients, List<ResultEntryTestDto> tests) =
                await _spExecutor.ExecuteMultipleAsync<ResultEntryPatientDto, ResultEntryTestDto>("cproc_LoadPatientAndCaseInfoForTestProcess", cmd =>
                {
                    DbHelper.AddParam(cmd, "@Fromdate", request.FromDate);
                    DbHelper.AddParam(cmd, "@ToDate", request.ToDate);
                    DbHelper.AddParam(cmd, "@FilterByDate", request.FilterByDate);
                    DbHelper.AddParam(cmd, "@TestCodeFrom", request.TestCodeFrom);
                    DbHelper.AddParam(cmd, "@TestCodeTo", request.TestCodeTo);
                    DbHelper.AddParam(cmd, "@TestName", request.TestName);
                    DbHelper.AddParam(cmd, "@CaseNumber", request.CaseNumber);
                    DbHelper.AddParam(cmd, "@Registrationlocation", request.RegistrationLocation);
                    DbHelper.AddParam(cmd, "@UserID", request.UserID);
                    DbHelper.AddParam(cmd, "@TestStatus", request.TestStatus);
                    DbHelper.AddParam(cmd, "@IsDelayed", request.IsDelayed);
                    DbHelper.AddParam(cmd, "@ConnectedCenters", request.ConnectedCenters);
                    DbHelper.AddParam(cmd, "@ForConnectedCenters", request.ForConnectedCenters);
                    DbHelper.AddParam(cmd, "@IsSendToHeadOffice", request.IsSendToHeadOffice);
                    DbHelper.AddParam(cmd, "@IsForAbnormalValue", request.IsForAbnormalValue);
                    DbHelper.AddParam(cmd, "@ConductedAt", request.ConductedAt);
                    DbHelper.AddParam(cmd, "@Consultant", request.Consultant);
                    DbHelper.AddParam(cmd, "@Reference", request.Reference);
                });

            // 🔥 Build lookup (FAST)
            var testLookup = tests
                .GroupBy(t => t.CaseID)
                .ToDictionary(g => g.Key, g => g.ToList());

            // 🔥 Map parent-child
            var result = patients.Select(p => new ResultEntryMappedResponse
            {
                Id = p.Id,
                PName = p.PName,
                FHName = p.FHName,
                AgeSex = p.AgeSex,
                BloodGroup = p.BloodGroup,
                CaseID = p.CaseID,
                CaseNumber = p.CaseNumber,
                RegistrationDate = p.RegistrationDate,
                RegLoc = p.RegLoc,
                CenterName = p.CenterName,
                RemarkID = p.RemarkID,

                Tests = testLookup.ContainsKey(p.CaseID) ? testLookup[p.CaseID] : new List<ResultEntryTestDto>()
            }).ToList();

            return result;
        }

        private void AddParameter(
            System.Data.Common.DbCommand cmd,
            string name,
            object? value)
        {
            var param = cmd.CreateParameter();
            param.ParameterName = name;
            param.Value = value ?? DBNull.Value;
            cmd.Parameters.Add(param);
        }
    }
}