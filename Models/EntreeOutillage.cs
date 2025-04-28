using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsOutillageService.Models
{
    [Table("entree_outillage")]
    public class EntreeOutillage
    {
        [Key]
        [Column("id_entree_outillage")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEntreeOutillage { get; set; }

        [Required]
        [Column("prix_achat")]
        public decimal PrixAchat { get; set; }

        [Required]
        [Column("quantite")]
        public int Quantite { get; set; }

        [Column("bon_reception")]
        [StringLength(50)]
        public string? BonReception { get; set; }

        [Required]
        [Column("date_entree")]
        public DateTime DateEntree { get; set; }

        [Required]
        [Column("id_fournisseur")]
        public int IdFournisseur { get; set; }

        [Required]
        [Column("id_outillage")]
        public int IdOutillage { get; set; }

        // Propriétés de navigation
        [ForeignKey("IdFournisseur")]
        public Fournisseur? Fournisseur { get; set; }

        [ForeignKey("IdOutillage")]
        public Outillage? Outillage { get; set; }
    }
}