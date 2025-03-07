using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsOutillageService.Models
{
    [Table("Marque")] // Spécifie le nom de la table dans la base de données
    public class Marque
    {
        [Key] // Indique que cette propriété est la clé primaire
        [Column("id_marque")] // Spécifie le nom de la colonne dans la base de données
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Indique que la valeur est auto-incrémentée
        public int IdMarque { get; set; }

        [Required] // Indique que la propriété est non nullable
        [Column("designation")]
        public required string Designation { get; set; }

        // Propriété de navigation pour la relation avec Outillage
        public ICollection<Outillage> Outillages { get; set; } = new List<Outillage>();
    }
}