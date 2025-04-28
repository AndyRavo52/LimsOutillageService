using System;
using System.Text.Json.Serialization;

namespace LimsOutillageService.Dtos
{
    public class ReportOutillageDto
    {
        [JsonPropertyName("idReportOutillage")]
        public int IdReportOutillage { get; set; }

        [JsonPropertyName("dateReport")]
        public DateTime DateReport { get; set; }

        [JsonPropertyName("quantite")]
        public int Quantite { get; set; }

        [JsonPropertyName("idOutillage")]
        public int IdOutillage { get; set; }

        [JsonPropertyName("outillage")]
        public OutillageDto? Outillage { get; set; }
    }
}