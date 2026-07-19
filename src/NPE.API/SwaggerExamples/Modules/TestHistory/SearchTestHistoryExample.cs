using NPE.Core.Modules.Laboratory.TestHistory.DTOs;
using NPE.Core.Modules.Management.Center.Models;
using Swashbuckle.AspNetCore.Filters;

namespace NPE.API.SwaggerExamples.Modules.TestHistory
{
    public class SearchTestHistoryExample : IExamplesProvider<TestHistorySearchRequest>
    {
        public TestHistorySearchRequest GetExamples()
        {
            return new TestHistorySearchRequest
            {
                // Patient filters
                PatientNumber = "",
                PatientName = "",
                Sex = 3,
                BloodGroup = "",
                Phone = "",
                NIC = "",

                // Patient registration filters
                RegistrationCenter = 0,
                RegistrationDateFrom = new DateTime(2026, 07, 01),
                RegistrationDateTo = new DateTime(2026, 07, 06),
                FilterByDate = false,

                // Case / test filters
                CaseNumber = "",
                TestCodeFrom = "",
                TestCodeTo = "",
                TestName = "",
                TestStatus = "-4",

                // Case registration filters
                CaseReglocation = 0,
                ConsultantID = 0,
                ReferenceID = 0,
                CaseRegFromdate = new DateTime(2026, 07, 01),
                CaseRegToDate = new DateTime(2026, 07, 06),
                CaseRegFilterByDate = true,

                // Paging
                PageNumber = 1,
                PageSize = 50,

                // Additional filters
                Diagonosis = "",
                Specimen = "",
                BiopsyNo = "",
                CS = "",
                MRNo = "",
                CABGNo = "",
                IsSpecialCase = false,
                TableName = "",
                CriticalResult = false
            };
        }
    }
}
