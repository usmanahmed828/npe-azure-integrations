using Microsoft.EntityFrameworkCore;
using NPE.Core.Common.Crud;
using NPE.Core.Modules.Management.KeyValue.BusinessObjects;
using NPE.Core.Modules.Management.KeyValue.DTOs;
using System;

namespace NPE.Infrastructure.Modules.Management.Shared.Services
{
    public class KeyValueService : IKeyValueService
    {
        private readonly ApplicationDbContext _context;

        public KeyValueService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> CreateAsync(KeyValueDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _context.KeyValue.FindAsync(id);
            if (data != null)
            {
                _context.KeyValue.Remove(data);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<KeyValueDTO>> GetAllAsync()
        {
            var data = await _context.KeyValue
            .AsNoTracking()
            .Select(x => new KeyValueDTO
            {
                Key = x.Id,
                KeyName = x.KeyName,
                Value = x.Value
            })
            .ToListAsync();
            return data;
        }

        public async Task<KeyValueDTO?> GetByIdAsync(int id)
        {
            var data = await _context.KeyValue
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new KeyValueDTO
            {
                Key = x.Id,
                KeyName = x.KeyName,
                Value = x.Value
            })
            .FirstOrDefaultAsync();
            return data == null ? null : data;
        }

        public Task<int> UpdateAsync(KeyValueDTO dto)
        {
            throw new NotImplementedException();
            //var data = _context.KeyValue.Find(dto.Key);
            //data.Value = dto.Value;
            //_context.KeyValue.Update(data);
            //return _context.SaveChangesAsync();
        }

        Task<KeyValueDTO> ICrudService<KeyValueDTO, int>.CreateAndReturnAsync(KeyValueDTO dto)
        {
            throw new NotImplementedException();
        }

        Task<List<KeyValueDTO>> IKeyValueService.GetByKeyName()
        {
            throw new NotImplementedException();
        }


    }
}