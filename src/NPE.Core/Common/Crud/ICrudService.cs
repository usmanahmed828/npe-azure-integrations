namespace NPE.Core.Common.Crud
{
    public interface ICrudService<TDto, TKey>
    {
        Task<TDto?> GetByIdAsync(TKey id);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TKey> CreateAsync(TDto dto);
        Task<TDto> CreateAndReturnAsync(TDto dto);
        Task<TKey> UpdateAsync(TDto dto);
        Task DeleteAsync(TKey id);
    }
}
