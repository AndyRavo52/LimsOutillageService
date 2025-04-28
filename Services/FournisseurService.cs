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
    public class FournisseurService : IFournisseurService
    {
        private readonly OutillageContext _context;

        public FournisseurService(OutillageContext context)
        {
            _context = context;
        }

        public async Task<int> CountFournisseursAsync()
        {
            return await _context.Fournisseurs.CountAsync();
        }

        public async Task<IEnumerable<FournisseurDto>> GetFournisseursAsync(int pageIndex, int pageSize)
        {
            var fournisseurs = await _context.Fournisseurs
                .OrderBy(f => f.Designation)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return fournisseurs.Select(FournisseurMapper.ToDto);
        }

        public async Task<FournisseurDto> GetFournisseurByIdAsync(int id)
        {
            var fournisseur = await _context.Fournisseurs.FindAsync(id);
            if (fournisseur == null)
            {
                throw new Exception("Fournisseur non trouvé");
            }

            return FournisseurMapper.ToDto(fournisseur);
        }
    }
}