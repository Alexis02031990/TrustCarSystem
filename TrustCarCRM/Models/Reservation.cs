using TrustCarCRM.Models;

namespace TrustCarCRM.Models
{
    // Table Reservations
    public class Reservation
    {
        public int ReservationId { get; set; }

        // Remplacer int par string pour correspondre à la clé primaire de Client
        public string ClientId { get; set; }  // ClientId est maintenant de type string
        public int VehiculeId { get; set; }
        public int UtilisateurId { get; set; }
        public DateTime Date_Reservation { get; set; }
        public DateTime? Date_Expiration { get; set; }
        public string? Statut { get; set; }  // Confirmée, Annulée, Expirée

        // Clés étrangères et relations
        public Client? ClientReservations { get; set; }
        public Vehicule? VehiculeReservations { get; set; }
        public Utilisateur? UtilisateurReservations { get; set; }
    }
}
