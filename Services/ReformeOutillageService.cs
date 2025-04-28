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
    public class ReformeOutillageService : IReformeOutillageService
    {
        private readonly OutillageContext _context;

        public ReformeOutillageService(OutillageContext context)
        {
            _context = context;
        }

        public async Task<int> CountReformeOutillagesAsync()
        {
            return await _context.ReformesOutillage.CountAsync();
        }

        public async Task<IEnumerable<ReformeOutillageDto>> GetReformeOutillagesAsync(int pageIndex, int pageSize)
        {
            var reformeOutillages = await _context.ReformesOutillage
                .Include(ro => ro.Outillage)
                .OrderByDescending(ro => ro.DateReforme)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return reformeOutillages.Select(ReformeOutillageMapper.ToDto);
        }

        public async Task<ReformeOutillageDto> GetReformeOutillageByIdAsync(int id)
        {
            var reformeOutillage = await _context.ReformesOutillage
                .Include(ro => ro.Outillage)
                .FirstOrDefaultAsync(ro => ro.IdReformeOutillage == id);

            if (reformeOutillage == null)
            {
                throw new Exception("Réforme outillage non trouvée");
            }

            return ReformeOutillageMapper.ToDto(reformeOutillage);
        }

        public async Task<ReformeOutillageDto> CreateReformeOutillageAsync(ReformeOutillageDto reformeOutillageDto)
        {
            var reformeOutillage = ReformeOutillageMapper.ToEntity(reformeOutillageDto);
            _context.ReformesOutillage.Add(reformeOutillage);
            await _context.SaveChangesAsync();

            return ReformeOutillageMapper.ToDto(reformeOutillage);
        }
    }
}