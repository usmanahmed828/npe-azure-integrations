namespace NPE.Core.Common.CaseNumbers
{
    public interface ICaseNumberService
    {
        /// <summary>
        /// Returns the next expected case number WITHOUT consuming it.
        /// Used on Case Register screen load.
        /// </summary>
        Task<string> GetNextCaseNumberAsync(
            int centerCode,
            DateTime caseDate,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Generates and CONSUMES the next case number.
        /// Used only during Save.
        /// Must be called inside a transaction.
        /// </summary>
        Task<string> ConsumeAsync(
            int centerCode,
            DateTime caseDate,
            CancellationToken cancellationToken = default);
    }
}