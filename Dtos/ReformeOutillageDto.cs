using System;
using System.Text.Json.Serialization;

namespace LimsOutillageService.Dtos
{
    public class ReformeOutillageDto
    {
        [JsonPropertyName("idReformeOutillage")]
        public int IdReformeOutillage { get; set; }

        [JsonPropertyName("dateReforme")]
        public DateTime DateReforme { get; set; }

        [JsonPropertyName("objet")]
        public string? Objet { get; set; }

        [JsonPropertyName("nombre")]
        public int Nombre { get; set; }

        [JsonPropertyName("idOutillage")]
        public int IdOutillage { get; set; }

        [JsonPropertyName("outillage")]
        public OutillageDto? Outillage { get; set; }
    }
}