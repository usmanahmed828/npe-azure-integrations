namespace NPE.Core.Common.Identity
{
    public interface IIdentityService
    {
        Task<T>
            ConsumeAsync<T>(
                int centerCode,
                string identityType,
                CancellationToken cancellationToken = default)
            where T : struct;

        Task<T>
            GetNextAsync<T>(
                int centerCode,
                string identityType,
                CancellationToken cancellationToken = default)
            where T : struct;

        Task<int>
            ConsumeIlockAsync(
                string identityType,
                CancellationToken cancellationToken = default);
    }
}