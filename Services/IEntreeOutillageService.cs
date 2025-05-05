using LimsOutillageService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsOutillageService.Services
{
    public interface IEntreeOutillageService
    {
        Task<int> CountEntreeOutillagesAsync();
        Task<IEnumerable<EntreeOutillageDto>> GetEntreeOutillagesAsync(int pageIndex, int pageSize);
        Task<EntreeOutillageDto> GetEntreeOutillageByIdAsync(int id);
        Task<EntreeOutillageDto> CreateEntreeOutillageAsync(EntreeOutillageDto entreeOutillageDto);
        Task<Dictionary<string, decimal>> GetDepensesParMoisAsync(int annee);
    }
}