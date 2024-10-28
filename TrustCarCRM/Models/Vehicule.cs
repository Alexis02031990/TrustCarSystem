namespace TrustCarCRM.Models
{
    public class Vehicule
    {
        public int VehiculeId { get; set; }
        public string? Marque { get; set; }
        public string? Modele { get; set; }
        public string? Annee { get; set; }
        public string? Couleur { get; set; }
        public int Kilometrage { get; set; }
        public decimal Prix { get; set; }
        public DateTime Date_Ajout { get; set; }
        public string? Statut { get; set; }  // Disponible, Vendu, Réservé, etc.

        // Clé étrangère vers la table Categorie
        public int? CategorieId { get; set; }
        public Categorie? Categorie { get; set; }  // Relation vers Categorie

        // Relation: Un véhicule peut être lié à plusieurs ventes, rendez-vous, et réservations
        public ICollection<Vente>? VenteVehicules { get; set; }
        public ICollection<RendezVous>? RendezVousVehicules { get; set; }
        public ICollection<Reservation>? ReservationsVehicules { get; set; }
    }
}
