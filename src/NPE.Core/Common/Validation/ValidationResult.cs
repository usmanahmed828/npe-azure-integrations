namespace NPE.Core.Common.Validation
{
    public sealed class ValidationResult
    {
        private readonly List<string> _errors = new();

        public IReadOnlyList<string> Errors => _errors;
        public bool IsValid => _errors.Count == 0;

        public void Add(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                _errors.Add(message);
        }

        public void AddRange(IEnumerable<string> messages)
        {
            foreach (var m in messages)
                Add(m);
        }
    }
}
