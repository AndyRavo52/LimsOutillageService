using LimsOutillageService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsOutillageService.Services
{
    public interface IReportOutillageService
    {
        Task<int> CountReportOutillagesAsync();
        Task<IEnumerable<ReportOutillageDto>> GetReportOutillagesAsync(int pageIndex, int pageSize);
        Task<ReportOutillageDto> GetReportOutillageByIdAsync(int id);
        Task<ReportOutillageDto> CreateReportOutillageAsync(ReportOutillageDto reportOutillageDto);
    }
}