using LimsOutillageService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsOutillageService.Services
{
    public interface IReformeOutillageService
    {
        Task<int> CountReformeOutillagesAsync();
        Task<IEnumerable<ReformeOutillageDto>> GetReformeOutillagesAsync(int pageIndex, int pageSize);
        Task<ReformeOutillageDto> GetReformeOutillageByIdAsync(int id);
        Task<ReformeOutillageDto> CreateReformeOutillageAsync(ReformeOutillageDto reformeOutillageDto);
    }
}