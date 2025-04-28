using LimsOutillageService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsOutillageService.Services
{
    public interface IFournisseurService
    {
        // Compte le nombre total de fournisseurs
        Task<int> CountFournisseursAsync();

        // Récupère une liste paginée de fournisseurs
        Task<IEnumerable<FournisseurDto>> GetFournisseursAsync(int pageIndex, int pageSize);

        // Récupère un fournisseur par son ID
        Task<FournisseurDto> GetFournisseurByIdAsync(int id);
    }
}