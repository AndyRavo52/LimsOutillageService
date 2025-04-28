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
    public class MarqueService : IMarqueService
    {
        private readonly OutillageContext _context;

        // Injection de dépendance du DbContext
        public MarqueService(OutillageContext context)
        {
            _context = context;
        }

        // Compte le nombre total de marques
        public async Task<int> CountMarquesAsync()
        {
            return await _context.Marques.CountAsync();
        }

        // Récupère une liste paginée de marques
        public async Task<IEnumerable<MarqueDto>> GetMarquesAsync(int pageIndex, int pageSize)
        {
            var marques = await _context.Marques
                .OrderBy(m => m.Designation)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return marques.Select(MarqueMapper.ToDto);
        }

        // Récupère une marque par son ID
        public async Task<MarqueDto> GetMarqueByIdAsync(int id)
        {
            var marque = await _context.Marques.FindAsync(id);
            if (marque == null)
            {
                throw new Exception("Marque non trouvée");
            }

            return MarqueMapper.ToDto(marque);
        }

        
    }
}
