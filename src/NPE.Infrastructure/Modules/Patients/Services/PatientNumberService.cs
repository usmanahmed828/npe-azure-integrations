using NPE.Core.Common;
using NPE.Core.Common.Identity;

namespace NPE.Infrastructure.Modules.Patients.Services
{
    public class PatientNumberService : IPatientNumberService
    {
        private readonly IIdentityService _identityService;

        public PatientNumberService(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<string> GenerateAsync(int centerCode, long patientId)
        {
            //var next = await _identityService.ConsumeAsync(
            //    centerCode,
            //    IdentityTypes.Patient);

            var year = DateTime.Now.ToString("yy");

            var padded = patientId.ToString().PadLeft(5, '0');

            return $"{centerCode}-{year}-{padded}";
        }
    }
}