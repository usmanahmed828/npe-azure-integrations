namespace NPE.Core.Common.Validation
{
    public class ValidationException : Exception
    {
        public IReadOnlyList<string> Errors { get; }

        public ValidationException(IReadOnlyList<string> errors)
            : base("Validation failed")
        {
            Errors = errors;
        }
    }
}
