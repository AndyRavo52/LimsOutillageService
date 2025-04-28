using LimsOutillageService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsOutillageService.Services
{
    public interface IOutillageService
    {
        Task<int> CountOutillagesAsync();
        Task<IEnumerable<OutillageDto>> GetOutillagesAsync(int pageIndex, int pageSize);
        Task<OutillageDto> GetOutillageByIdAsync(int id);
        Task<OutillageDto> CreateOutillageAsync(OutillageDto outillageDto);
        Task<OutillageDto> UpdateOutillageAsync(int id, OutillageDto outillageDto);
        Task<bool> DeleteOutillageAsync(int id);
        Task<IEnumerable<OutillageDto>> SearchOutillagesAsync(string searchTerm);
    }
}