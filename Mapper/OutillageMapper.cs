using LimsOutillageService.Dtos;
using LimsOutillageService.Mapper;
using LimsOutillageService.Models;

namespace LimsOutillageService.Mapper
{
    public static class OutillageMapper
    {
        public static OutillageDto ToDto(Outillage outillage)
        {
            return new OutillageDto
            {
                IdOutillage = outillage.IdOutillage,
                Designation = outillage.Designation,
                IdMarque = outillage.IdMarque,
                Marque = outillage.Marque != null ? MarqueMapper.ToDto(outillage.Marque) : null
            };
        }

        public static Outillage ToEntity(OutillageDto outillageDto)
        {
            return new Outillage
            {
                IdOutillage = outillageDto.IdOutillage,
                Designation = outillageDto.Designation,
                IdMarque = outillageDto.IdMarque
                // La propriété Marque n'est pas assignée ici car elle est gérée par EF Core
            };
        }
    }
}