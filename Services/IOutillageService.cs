using LimsOutillageService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsOutillageService.Services
{
    public interface IOutillageService
    {
        // Compte le nombre total d'outillages
        Task<int> CountOutillagesAsync();

        // Récupère une liste paginée d'outillages
        Task<IEnumerable<OutillageDto>> GetOutillagesAsync(int pageIndex, int pageSize);

        // Récupère un outillage par son ID
        Task<OutillageDto> GetOutillageByIdAsync(int id);

        // Crée un nouvel outillage
        Task<OutillageDto> CreateOutillageAsync(OutillageDto outillageDto);

        // Met à jour un outillage existant
        Task<OutillageDto> UpdateOutillageAsync(int id, OutillageDto outillageDto);

        // Supprime un outillage
        Task<bool> DeleteOutillageAsync(int id);
    }
}