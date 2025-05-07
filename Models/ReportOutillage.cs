using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsOutillageService.Models
{
    [Table("Report_outillage")]
    public class ReportOutillage
    {
        [Key]
        [Column("id_report_outillage")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdReportOutillage { get; set; }

        [Required]
        [Column("date_report")]
        public DateTime DateReport { get; set; }

        [Required]
        [Column("quantite")]
        public int Quantite { get; set; }

        [Required]
        [Column("id_outillage")]
        public int IdOutillage { get; set; }

        [ForeignKey("IdOutillage")]
        public Outillage? Outillage { get; set; }
    }
}