using LimsOutillageService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsOutillageService.Services
{
    public interface IEntreeOutillageService
    {
        // Compte le nombre total d'entrées d'outillage
        Task<int> CountEntreeOutillagesAsync();

        // Récupère une liste paginée d'entrées d'outillage
        Task<IEnumerable<EntreeOutillageDto>> GetEntreeOutillagesAsync(int pageIndex, int pageSize);

        // Récupère une entrée d'outillage par son ID
        Task<EntreeOutillageDto> GetEntreeOutillageByIdAsync(int id);

        // Crée une nouvelle entrée d'outillage
        Task<EntreeOutillageDto> CreateEntreeOutillageAsync(EntreeOutillageDto entreeOutillageDto);
    }
}