using LimsOutillageService.Dtos;
using LimsOutillageService.Models;

namespace LimsOutillageService.Mapper
{
    public static class EntreeOutillageMapper
    {
        public static EntreeOutillageDto ToDto(EntreeOutillage entreeOutillage)
        {
            return new EntreeOutillageDto
            {
                IdEntreeOutillage = entreeOutillage.IdEntreeOutillage,
                PrixAchat = entreeOutillage.PrixAchat,
                Quantite = entreeOutillage.Quantite,
                BonReception = entreeOutillage.BonReception,
                DateEntree = entreeOutillage.DateEntree,
                IdFournisseur = entreeOutillage.IdFournisseur,
                IdOutillage = entreeOutillage.IdOutillage,
                Fournisseur = entreeOutillage.Fournisseur != null ? FournisseurMapper.ToDto(entreeOutillage.Fournisseur) : null,
                Outillage = entreeOutillage.Outillage != null ? OutillageMapper.ToDto(entreeOutillage.Outillage) : null
            };
        }

        public static EntreeOutillage ToEntity(EntreeOutillageDto entreeOutillageDto)
        {
            return new EntreeOutillage
            {
                IdEntreeOutillage = entreeOutillageDto.IdEntreeOutillage,
                PrixAchat = entreeOutillageDto.PrixAchat,
                Quantite = entreeOutillageDto.Quantite,
                BonReception = entreeOutillageDto.BonReception,
                DateEntree = entreeOutillageDto.DateEntree,
                IdFournisseur = entreeOutillageDto.IdFournisseur,
                IdOutillage = entreeOutillageDto.IdOutillage
                // Les propriétés Fournisseur et Outillage ne sont pas assignées ici car elles sont gérées par EF Core
            };
        }
    }
}