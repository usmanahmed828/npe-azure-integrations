using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Infrastructure.Common.Crud
{
    public abstract class BaseCrudService<TEntity, TDto, TKey>
    where TEntity : class
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseCrudService(ApplicationDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual async Task<TDto?> GetByIdAsync(TKey id)
        {
            var entity = await DbSet.FindAsync(id);
            return entity == null ? default : ToDto(entity);
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var items = await DbSet.ToListAsync();
            return items.Select(ToDto);
        }

        public virtual async Task<TKey> CreateAsync(TDto dto)
        {
            var entity = ToEntity(dto);

            DbSet.Add(entity);

            await Context.SaveChangesAsync();

            return GetKey(entity);
        }

        public virtual async Task<TDto> CreateAndReturnAsync(TDto dto)
        {
            await CreateAsync(dto);

            return dto;
        }

        public virtual async Task UpdateAsync(TDto dto)
        {
            var entity = await DbSet.FindAsync(GetDtoKey(dto));

            if (entity == null)
                throw new KeyNotFoundException();

            UpdateEntity(entity, dto);
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            var entity = await DbSet.FindAsync(id);

            if (entity == null)
                throw new KeyNotFoundException();

            DbSet.Remove(entity);
        }

        protected abstract TDto ToDto(TEntity entity);
        protected abstract TEntity ToEntity(TDto dto);
        protected abstract void UpdateEntity(TEntity entity, TDto dto);
        protected abstract TKey GetKey(TEntity entity);
        protected abstract TKey GetDtoKey(TDto dto);
    }
}
