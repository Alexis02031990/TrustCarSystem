using System;
using System.Collections.Generic;

namespace TrustCarCRM.Models
{
    public class Client
    {
        public string Id { get; set; } // Assurez-vous qu'il y a un identifiant
        public string? Nom_Client { get; set; }
        public string? Prenom_Client { get; set; }
        public string? Adresse_Client { get; set; }
        public string? Email_Client { get; set; }  // Correction de l'orthographe
        public string? Telephone_Client { get; set; }  // Correction de l'orthographe
        public DateTime Date_Inscription { get; set; }

        // Relations avec d'autres entités
        public ICollection<Vente>? VenteClients { get; set; }
        public ICollection<RendezVous>? RendezVousClients { get; set; }
        public ICollection<Reservation>? ReservationsClients { get; set; }
    }
}
