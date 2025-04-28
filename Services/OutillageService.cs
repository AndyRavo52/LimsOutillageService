using LimsOutillageService.Data;
using LimsOutillageService.Dtos;
using LimsOutillageService.Mapper;
using LimsOutillageService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimsOutillageService.Services
{
    public class OutillageService : IOutillageService
    {
        private readonly OutillageContext _context;

        public OutillageService(OutillageContext context)
        {
            _context = context;
        }

        public async Task<int> CountOutillagesAsync()
        {
            return await _context.Outillages.CountAsync();
        }

        public async Task<IEnumerable<OutillageDto>> GetOutillagesAsync(int pageIndex, int pageSize)
        {
            var outillages = await _context.Outillages
                .OrderBy(o => o.Designation)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return outillages.Select(OutillageMapper.ToDto);
        }

        public async Task<OutillageDto> GetOutillageByIdAsync(int id)
        {
            var outillage = await _context.Outillages
                .Include(o => o.Marque)
                .FirstOrDefaultAsync(o => o.IdOutillage == id);

            if (outillage == null)
            {
                throw new Exception("Outillage non trouvé");
            }

            return OutillageMapper.ToDto(outillage);
        }

        public async Task<OutillageDto> CreateOutillageAsync(OutillageDto outillageDto)
        {
            var outillage = OutillageMapper.ToEntity(outillageDto);
            _context.Outillages.Add(outillage);
            await _context.SaveChangesAsync();
            return OutillageMapper.ToDto(outillage);
        }

        public async Task<OutillageDto> UpdateOutillageAsync(int id, OutillageDto outillageDto)
        {
            var outillage = await _context.Outillages
                .FirstOrDefaultAsync(o => o.IdOutillage == id);

            if (outillage == null)
            {
                throw new Exception("Outillage non trouvé");
            }

            outillage.Designation = outillageDto.Designation;
            outillage.IdMarque = outillageDto.IdMarque;
            await _context.SaveChangesAsync();
            return OutillageMapper.ToDto(outillage);
        }

        public async Task<bool> DeleteOutillageAsync(int id)
        {
            var outillage = await _context.Outillages
                .FirstOrDefaultAsync(o => o.IdOutillage == id);

            if (outillage == null)
            {
                return false;
            }

            _context.Outillages.Remove(outillage);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OutillageDto>> SearchOutillagesAsync(string searchTerm)
        {
            var query = _context.Outillages
                                .Include(o => o.Marque)
                                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(o => EF.Functions.Like(o.Designation, $"%{searchTerm}%"));
            }

            var outillages = await query
                .OrderBy(o => o.Designation)
                .ToListAsync();

            return outillages.Select(OutillageMapper.ToDto);
        }
    }
}