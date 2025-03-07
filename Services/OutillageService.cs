using LimsOutillageService.Data;
using LimsOutillageService.Dtos;
using LimsOutillageService.Mapper;
using LimsOutillageService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimsOutillageService.Services
{
    public class OutillageService : IOutillageService
    {
        private readonly OutillageContext _context;

        // Constructeur : Injection de dépendance du DbContext
        public OutillageService(OutillageContext context)
        {
            _context = context;
        }

        // Compte le nombre total d'outillages
        public async Task<int> CountOutillagesAsync()
        {
            return await _context.Outillages.CountAsync();
        }

        // Récupère une liste paginée d'outillages
        public async Task<IEnumerable<OutillageDto>> GetOutillagesAsync(int pageIndex, int pageSize)
        {
            var outillages = await _context.Outillages
                .OrderBy(o => o.Designation) // Trie par désignation
                .Skip((pageIndex - 1) * pageSize) // Saute les éléments des pages précédentes
                .Take(pageSize) // Prend un nombre limité d'éléments
                .ToListAsync();

            // Convertit les entités en DTOs
            return outillages.Select(OutillageMapper.ToDto);
        }

        // Récupère un outillage par son ID
        public async Task<OutillageDto> GetOutillageByIdAsync(int id)
        {
            var outillage = await _context.Outillages
                .Include(o => o.Marque) // Charge la marque associée
                .FirstOrDefaultAsync(o => o.IdOutillage == id);

            if (outillage == null)
            {
                throw new Exception("Outillage non trouvé");
            }

            // Convertit l'entité en DTO
            return OutillageMapper.ToDto(outillage);
        }

        // Crée un nouvel outillage
        public async Task<OutillageDto> CreateOutillageAsync(OutillageDto outillageDto)
        {
            // Convertit le DTO en entité
            var outillage = OutillageMapper.ToEntity(outillageDto);

            // Ajoute l'outillage à la base de données
            _context.Outillages.Add(outillage);
            await _context.SaveChangesAsync();

            // Convertit l'entité en DTO pour la réponse
            return OutillageMapper.ToDto(outillage);
        }

        // Met à jour un outillage existant
        public async Task<OutillageDto> UpdateOutillageAsync(int id, OutillageDto outillageDto)
        {
            // Récupère l'outillage existant
            var outillage = await _context.Outillages
                .FirstOrDefaultAsync(o => o.IdOutillage == id);

            if (outillage == null)
            {
                throw new Exception("Outillage non trouvé");
            }

            // Met à jour les propriétés de l'outillage
            outillage.Designation = outillageDto.Designation;
            outillage.IdMarque = outillageDto.IdMarque;

            // Sauvegarde les modifications
            await _context.SaveChangesAsync();

            // Convertit l'entité en DTO pour la réponse
            return OutillageMapper.ToDto(outillage);
        }

        // Supprime un outillage
        public async Task<bool> DeleteOutillageAsync(int id)
        {
            // Récupère l'outillage à supprimer
            var outillage = await _context.Outillages
                .FirstOrDefaultAsync(o => o.IdOutillage == id);

            if (outillage == null)
            {
                return false; // Outillage non trouvé
            }

            // Supprime l'outillage
            _context.Outillages.Remove(outillage);
            await _context.SaveChangesAsync();

            return true; // Suppression réussie
        }
    }
}