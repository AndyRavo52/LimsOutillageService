﻿using System.Text.Json.Serialization;

namespace LimsOutillageService.Dtos
{
    public class OutillageDto
    {
        [JsonPropertyName("idOutillage")]
        public int IdOutillage { get; set; }

        [JsonPropertyName("designation")]
        public required string Designation { get; set; }

        [JsonPropertyName("idMarque")]
        public int? IdMarque { get; set; }

        [JsonPropertyName("marque")]
        public MarqueDto? Marque { get; set; }
    }
}