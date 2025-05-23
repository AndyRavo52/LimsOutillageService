// using LimsOutillageService.Data;
// using LimsOutillageService.Dtos;
// using LimsOutillageService.Mapper;
// using LimsOutillageService.Models;
// using Microsoft.EntityFrameworkCore;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace LimsOutillageService.Services
// {
//     public class ReformeOutillageService : IReformeOutillageService
//     {
//         private readonly OutillageContext _context;

//         public ReformeOutillageService(OutillageContext context)
//         {
//             _context = context;
//         }

//         public async Task<int> CountReformeOutillagesAsync()
//         {
//             return await _context.ReformesOutillage.CountAsync();
//         }

//         public async Task<IEnumerable<ReformeOutillageDto>> GetReformeOutillagesAsync(int pageIndex, int pageSize)
//         {
//             var reformeOutillages = await _context.ReformesOutillage
//                 .Include(ro => ro.Outillage)
//                 .OrderByDescending(ro => ro.DateReforme)
//                 .Skip((pageIndex - 1) * pageSize)
//                 .Take(pageSize)
//                 .ToListAsync();

//             return reformeOutillages.Select(ReformeOutillageMapper.ToDto);
//         }

//         public async Task<ReformeOutillageDto> GetReformeOutillageByIdAsync(int id)
//         {
//             var reformeOutillage = await _context.ReformesOutillage
//                 .Include(ro => ro.Outillage)
//                 .FirstOrDefaultAsync(ro => ro.IdReformeOutillage == id);

//             if (reformeOutillage == null)
//             {
//                 throw new Exception("Réforme outillage non trouvée");
//             }

//             return ReformeOutillageMapper.ToDto(reformeOutillage);
//         }

//         public async Task<ReformeOutillageDto> CreateReformeOutillageAsync(ReformeOutillageDto reformeOutillageDto)
//         {
//             var reformeOutillage = ReformeOutillageMapper.ToEntity(reformeOutillageDto);
//             _context.ReformesOutillage.Add(reformeOutillage);
//             await _context.SaveChangesAsync();

//             return ReformeOutillageMapper.ToDto(reformeOutillage);
//         }
//     }
// }
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
    public class ReformeOutillageService : IReformeOutillageService
    {
        private readonly OutillageContext _context;
        private readonly ILogger<ReformeOutillageService> _logger;

        public ReformeOutillageService(OutillageContext context, ILogger<ReformeOutillageService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> CountReformeOutillagesAsync()
        {
            return await _context.ReformesOutillage.CountAsync();
        }

        public async Task<IEnumerable<ReformeOutillageDto>> GetReformeOutillagesAsync(int pageIndex, int pageSize)
        {
            var reformeOutillages = await _context.ReformesOutillage
                .Include(ro => ro.Outillage)
                .OrderByDescending(ro => ro.DateReforme)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return reformeOutillages.Select(ReformeOutillageMapper.ToDto);
        }

        public async Task<ReformeOutillageDto> GetReformeOutillageByIdAsync(int id)
        {
            var reformeOutillage = await _context.ReformesOutillage
                .Include(ro => ro.Outillage)
                .FirstOrDefaultAsync(ro => ro.IdReformeOutillage == id);

            if (reformeOutillage == null)
            {
                throw new Exception("Réforme outillage non trouvée");
            }

            return ReformeOutillageMapper.ToDto(reformeOutillage);
        }

        public async Task<ReformeOutillageDto> CreateReformeOutillageAsync(ReformeOutillageDto reformeOutillageDto)
        {
            // Vérifier si l'outillage existe
            var outillageExiste = await _context.Outillages.AnyAsync(o => o.IdOutillage == reformeOutillageDto.IdOutillage);
            if (!outillageExiste)
            {
                throw new InvalidOperationException("L'outillage spécifié n'existe pas.");
            }

            // Vérifier que l'objet n'est pas vide
            if (string.IsNullOrWhiteSpace(reformeOutillageDto.Objet))
            {
                throw new InvalidOperationException("L'objet de la réforme est obligatoire.");
            }

            // Vérifier que le nombre est positif
            if (reformeOutillageDto.Nombre <= 0)
            {
                throw new InvalidOperationException("La quantité à réformer doit être supérieure à zéro.");
            }

            // Vérifier la quantité disponible
            int quantiteDisponible = await GetQuantiteDisponibleAsync(reformeOutillageDto.IdOutillage);
            if (reformeOutillageDto.Nombre > quantiteDisponible)
            {
                throw new InvalidOperationException(
                    $"La quantité à réformer ({reformeOutillageDto.Nombre}) dépasse la quantité disponible ({quantiteDisponible}) pour cet outillage."
                );
            }

            var reformeOutillage = ReformeOutillageMapper.ToEntity(reformeOutillageDto);
            _context.ReformesOutillage.Add(reformeOutillage);
            await _context.SaveChangesAsync();

            return ReformeOutillageMapper.ToDto(reformeOutillage);
        }

        private async Task<int> GetQuantiteDisponibleAsync(int idOutillage)
        {
            _logger.LogInformation("Calcul de la quantité disponible pour outillage {Id}", idOutillage);

            // Récupérer le dernier rapport pour l'outillage, trié par DateReport décroissant
            var dernierRapport = await _context.ReportsOutillage
                .Where(r => r.IdOutillage == idOutillage)
                .OrderByDescending(r => r.DateReport)
                .FirstOrDefaultAsync();

            int quantiteDisponible;

            if (dernierRapport != null)
            {
                // Base : quantité du dernier rapport
                quantiteDisponible = dernierRapport.Quantite;
                _logger.LogInformation("Rapport trouvé : Quantité = {Quantite}, DateReport = {Date}", quantiteDisponible, dernierRapport.DateReport);

                // Ajouter les entrées strictement postérieures au rapport
                var totalEntreesApresRapport = await _context.EntreesOutillage
                    .Where(e => e.IdOutillage == idOutillage && e.DateEntree > dernierRapport.DateReport)
                    .SumAsync(e => e.Quantite);
                _logger.LogInformation("Entrées après rapport : {Total}, DateReport = {Date}", totalEntreesApresRapport, dernierRapport.DateReport);

                // Soustraire les réformes strictement postérieures au rapport
                var totalReformesApresRapport = await _context.ReformesOutillage
                    .Where(r => r.IdOutillage == idOutillage && r.DateReforme > dernierRapport.DateReport)
                    .SumAsync(r => r.Nombre);
                _logger.LogInformation("Réformes après rapport : {Total}, DateReport = {Date}", totalReformesApresRapport, dernierRapport.DateReport);

                quantiteDisponible += totalEntreesApresRapport - totalReformesApresRapport;
                _logger.LogInformation("Quantité disponible finale : {Quantite}", quantiteDisponible);
            }
            else
            {
                // Pas de rapport : somme des entrées - somme des réformes
                var totalEntrees = await _context.EntreesOutillage
                    .Where(e => e.IdOutillage == idOutillage)
                    .SumAsync(e => e.Quantite);
                var totalReformes = await _context.ReformesOutillage
                    .Where(r => r.IdOutillage == idOutillage)
                    .SumAsync(r => r.Nombre);
                quantiteDisponible = totalEntrees - totalReformes;
                _logger.LogInformation("Pas de rapport. Entrées : {Entrees}, Réformes : {Reformes}, Quantité : {Quantite}",
                    totalEntrees, totalReformes, quantiteDisponible);
            }

            return Math.Max(0, quantiteDisponible);
        }
    }
}