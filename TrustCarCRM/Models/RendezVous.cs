namespace TrustCarCRM.Models
{
    // Table RendezVous
    public class RendezVous
    {
        public int RendezVousId { get; set; }

        // Remplacer int par string pour correspondre à la clé primaire de Client
        public string ClientId { get; set; }  // ClientId est maintenant de type string
        public int VehiculeId { get; set; }
        public int UtilisateurId { get; set; }
        public DateTime Date_RendezVous { get; set; }
        public string? Statut { get; set; }  // Confirmé, En attente, Annulé

        // Clés étrangères et relations
        public Client? ClientRendezVous { get; set; }
        public Vehicule? VehiculeRendezVous { get; set; }
        public Utilisateur? UtilisateurRendezVous { get; set; }
    }
}
