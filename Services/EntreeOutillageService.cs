
using LimsOutillageService.Data;
using LimsOutillageService.Dtos;
using LimsOutillageService.Mapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimsOutillageService.Services
{
    public class EntreeOutillageService : IEntreeOutillageService
    {
        private readonly OutillageContext _context;

        public EntreeOutillageService(OutillageContext context)
        {
            _context = context;
        }

        public async Task<int> CountEntreeOutillagesAsync()
        {
            return await _context.EntreesOutillage.CountAsync();
        }

        public async Task<IEnumerable<EntreeOutillageDto>> GetEntreeOutillagesAsync(int pageIndex, int pageSize)
        {
            var entreeOutillages = await _context.EntreesOutillage
                .Include(eo => eo.Outillage)
                .Include(eo => eo.Fournisseur)
                .OrderByDescending(eo => eo.DateEntree)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return entreeOutillages.Select(EntreeOutillageMapper.ToDto);
        }

        public async Task<EntreeOutillageDto> GetEntreeOutillageByIdAsync(int id)
        {
            var entreeOutillage = await _context.EntreesOutillage
                .Include(eo => eo.Outillage)
                .Include(eo => eo.Fournisseur)
                .FirstOrDefaultAsync(eo => eo.IdEntreeOutillage == id);

            if (entreeOutillage == null)
            {
                throw new Exception("Entrée outillage non trouvée");
            }

            return EntreeOutillageMapper.ToDto(entreeOutillage);
        }

        public async Task<EntreeOutillageDto> CreateEntreeOutillageAsync(EntreeOutillageDto entreeOutillageDto)
        {
            if (!await _context.Outillages.AnyAsync(o => o.IdOutillage == entreeOutillageDto.IdOutillage))
            {
                throw new ArgumentException("L'outillage spécifié n'existe pas.");
            }
            if (!await _context.Fournisseurs.AnyAsync(f => f.IdFournisseur == entreeOutillageDto.IdFournisseur))
            {
                throw new ArgumentException("Le fournisseur spécifié n'existe pas.");
            }

            var entreeOutillage = EntreeOutillageMapper.ToEntity(entreeOutillageDto);
            _context.EntreesOutillage.Add(entreeOutillage);
            await _context.SaveChangesAsync();

            var createdEntreeOutillage = await _context.EntreesOutillage
                .Include(eo => eo.Outillage)
                .Include(eo => eo.Fournisseur)
                .FirstOrDefaultAsync(eo => eo.IdEntreeOutillage == entreeOutillage.IdEntreeOutillage);

            if (createdEntreeOutillage == null)
            {
                throw new Exception("Erreur lors de la récupération de l'entrée outillage créée.");
            }

            return EntreeOutillageMapper.ToDto(createdEntreeOutillage);
        }

        public async Task<Dictionary<string, decimal>> GetDepensesParMoisAsync(int annee)
        {
            return await _context.EntreesOutillage
                .Where(eo => eo.DateEntree.Year == annee)
                .GroupBy(eo => new { eo.DateEntree.Year, eo.DateEntree.Month })
                .Select(g => new
                {
                    Periode = $"{g.Key.Year}-{g.Key.Month:D2}",
                    Total = g.Sum(eo => eo.PrixAchat)
                })
                .ToDictionaryAsync(x => x.Periode, x => x.Total);
        }
    }
}