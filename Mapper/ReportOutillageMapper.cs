using LimsOutillageService.Dtos;
using LimsOutillageService.Models;

namespace LimsOutillageService.Mapper
{
    public static class ReportOutillageMapper
    {
        public static ReportOutillageDto ToDto(ReportOutillage reportOutillage)
        {
            return new ReportOutillageDto
            {
                IdReportOutillage = reportOutillage.IdReportOutillage,
                DateReport = reportOutillage.DateReport,
                Quantite = reportOutillage.Quantite,
                IdOutillage = reportOutillage.IdOutillage,
                Outillage = reportOutillage.Outillage != null ? OutillageMapper.ToDto(reportOutillage.Outillage) : null
            };
        }

        public static ReportOutillage ToEntity(ReportOutillageDto reportOutillageDto)
        {
            return new ReportOutillage
            {
                IdReportOutillage = reportOutillageDto.IdReportOutillage,
                DateReport = reportOutillageDto.DateReport,
                Quantite = reportOutillageDto.Quantite,
                IdOutillage = reportOutillageDto.IdOutillage
            };
        }
    }
}