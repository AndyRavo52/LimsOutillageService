using LimsOutillageService.Dtos;
using LimsOutillageService.Models;

namespace LimsOutillageService.Mapper
{
    public static class MarqueMapper
    {
        public static MarqueDto ToDto(Marque marque)
        {
            return new MarqueDto
            {
                IdMarque = marque.IdMarque,
                Designation = marque.Designation
            };
        }

        public static Marque ToEntity(MarqueDto marqueDto)
        {
            return new Marque
            {
                IdMarque = marqueDto.IdMarque,
                Designation = marqueDto.Designation
            };
        }
    }
}