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
    public class ReportOutillageService : IReportOutillageService
    {
        private readonly OutillageContext _context;

        public ReportOutillageService(OutillageContext context)
        {
            _context = context;
        }

        public async Task<int> CountReportOutillagesAsync()
        {
            return await _context.ReportsOutillage.CountAsync();
        }

        public async Task<IEnumerable<ReportOutillageDto>> GetReportOutillagesAsync(int pageIndex, int pageSize)
        {
            var reportOutillages = await _context.ReportsOutillage
                .Include(ro => ro.Outillage)
                .OrderByDescending(ro => ro.DateReport)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return reportOutillages.Select(ReportOutillageMapper.ToDto);
        }

        public async Task<ReportOutillageDto> GetReportOutillageByIdAsync(int id)
        {
            var reportOutillage = await _context.ReportsOutillage
                .Include(ro => ro.Outillage)
                .FirstOrDefaultAsync(ro => ro.IdReportOutillage == id);

            if (reportOutillage == null)
            {
                throw new Exception("Report outillage non trouvé");
            }

            return ReportOutillageMapper.ToDto(reportOutillage);
        }

        public async Task<ReportOutillageDto> CreateReportOutillageAsync(ReportOutillageDto reportOutillageDto)
        {
            var reportOutillage = ReportOutillageMapper.ToEntity(reportOutillageDto);
            _context.ReportsOutillage.Add(reportOutillage);
            await _context.SaveChangesAsync();

            return ReportOutillageMapper.ToDto(reportOutillage);
        }
    }
}