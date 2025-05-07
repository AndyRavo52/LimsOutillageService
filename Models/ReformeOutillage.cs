using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsOutillageService.Models
{
    [Table("Reforme_outillage")]
    public class ReformeOutillage
    {
        [Key]
        [Column("id_reforme_outillage")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdReformeOutillage { get; set; }

        [Required]
        [Column("date_reforme")]
        public DateTime DateReforme { get; set; }

        [Required]
        [Column("objet")]
        public string? Objet { get; set; }

        [Required]
        [Column("nombre")]
        public int Nombre { get; set; }

        [Required]
        [Column("id_outillage")]
        public int IdOutillage { get; set; }

        [ForeignKey("IdOutillage")]
        public Outillage? Outillage { get; set; }
    }
}