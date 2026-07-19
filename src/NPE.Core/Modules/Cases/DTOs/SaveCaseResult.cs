namespace NPE.Core.Modules.Cases.Models
{
    public sealed class SaveCaseResult
    {
        public bool IsSuccess { get; }
        public long? CaseId { get; }
        public IReadOnlyList<string> Errors { get; }

        private SaveCaseResult(bool isSuccess, long? caseId, IReadOnlyList<string> errors)
        {
            IsSuccess = isSuccess;
            CaseId = caseId;
            Errors = errors;
        }

        public static SaveCaseResult Success(long caseId)
            => new(true, caseId, Array.Empty<string>());

        public static SaveCaseResult Fail(IEnumerable<string> errors)
            => new(false, null, errors.ToList());

        public static SaveCaseResult CaseNumberChanged(string newNumber)
        => new(false, null, new[]
        {
            $"CASE_NUMBER_CHANGED:{newNumber}"
        });
    }
}
