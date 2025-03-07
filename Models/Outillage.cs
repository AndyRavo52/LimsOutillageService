using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsOutillageService.Models
{
    [Table("Outillage")] // Spécifie le nom de la table dans la base de données
    public class Outillage
    {
        [Key] // Indique que cette propriété est la clé primaire
        [Column("id_outillage")] // Spécifie le nom de la colonne dans la base de données
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Indique que la valeur est auto-incrémentée
        public int IdOutillage { get; set; }

        [Required] // Indique que la propriété est non nullable
        [Column("designation")]
        [StringLength(50)] // Limite la longueur à 50 caractères comme dans la table
        public required string Designation { get; set; }

        [Column("id_marque")] // Spécifie le nom de la colonne dans la base de données
        public int? IdMarque { get; set; } // Nullable car le champ accepte NULL dans la table

        // Propriété de navigation pour la relation avec Marque (facultative)
        [ForeignKey("IdMarque")] // Indique que cette propriété est une clé étrangère
        public Marque? Marque { get; set; }
    }
}