namespace TrustCarCRM.Models
{
    public class Categorie
    {
        public int Id_Categorie { get; set; }
        public string? Nom_Categorie { get; set; }

        // Relation: Une catégorie peut avoir plusieurs véhicules
        public ICollection<Vehicule>? VehiculeCategories { get; set; }
    }
}
