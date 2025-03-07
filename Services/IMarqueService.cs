
using LimsOutillageService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsOutillageService.Services
{
    public interface IMarqueService
    {
        // Compte le nombre total de marques
        Task<int> CountMarquesAsync();

        // Récupère une liste paginée de marques
        Task<IEnumerable<MarqueDto>> GetMarquesAsync(int pageIndex, int pageSize);

        // Récupère une marque par son ID
        Task<MarqueDto> GetMarqueByIdAsync(int id);
    }
}
