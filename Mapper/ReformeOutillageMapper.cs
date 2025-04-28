using LimsOutillageService.Dtos;
using LimsOutillageService.Models;

namespace LimsOutillageService.Mapper
{
    public static class ReformeOutillageMapper
    {
        public static ReformeOutillageDto ToDto(ReformeOutillage reformeOutillage)
        {
            return new ReformeOutillageDto
            {
                IdReformeOutillage = reformeOutillage.IdReformeOutillage,
                DateReforme = reformeOutillage.DateReforme,
                Objet = reformeOutillage.Objet,
                Nombre = reformeOutillage.Nombre,
                IdOutillage = reformeOutillage.IdOutillage,
                Outillage = reformeOutillage.Outillage != null ? OutillageMapper.ToDto(reformeOutillage.Outillage) : null
            };
        }

        public static ReformeOutillage ToEntity(ReformeOutillageDto reformeOutillageDto)
        {
            return new ReformeOutillage
            {
                IdReformeOutillage = reformeOutillageDto.IdReformeOutillage,
                DateReforme = reformeOutillageDto.DateReforme,
                Objet = reformeOutillageDto.Objet,
                Nombre = reformeOutillageDto.Nombre,
                IdOutillage = reformeOutillageDto.IdOutillage
            };
        }
    }
}