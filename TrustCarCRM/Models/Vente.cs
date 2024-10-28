namespace TrustCarCRM.Models
{
    // Table Ventes
    public class Vente
    {
        public int VenteId { get; set; }
        public string ClientId { get; set; }  // Changer de int à string pour correspondre à la clé primaire de Client
        public int VehiculeId { get; set; }
        public int UtilisateurId { get; set; }
        public DateTime Date_Vente { get; set; }
        public decimal Montant_Total { get; set; }
        public string? Methode_Paiement { get; set; }  // Espèce, Carte, Virement
        public string? Statut_Paiement { get; set; }   // Payé, En attente, Annulé

        // Clés étrangères et relations
        public Client? ClientVentes { get; set; }
        public Vehicule? VehiculeVentes { get; set; }
        public Utilisateur? UtilisateurVentes { get; set; }
    }
}
