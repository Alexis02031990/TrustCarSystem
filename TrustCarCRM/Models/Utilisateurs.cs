using Humanizer.Localisation;
using System;
using System.Collections.Generic;

namespace TrustCarCRM.Models
{
    // Table Utilisateurs (Agents)
    public class Utilisateur
    {
        public int UtilisateurId { get; set; }  // UtilisateurId utilisé comme clé primaire pour la table Utilisateur
        public string? Nom_Utilisateur { get; set; }
        public string? Prenom_Utilisateur { get; set; }
        public string? Email_Utilisateur { get; set; }
        public string? Telephone_Utilisateur { get; set; }
        public string? Role { get; set; }  // Admin, Agent
        public string? Password_Utilisateur { get; set; }

        // Relations: Un utilisateur (agent) peut être lié à plusieurs ventes, rendez-vous, et réservations
        public ICollection<Vente>? VenteUsers { get; set; }
        public ICollection<RendezVous>? RendezVousUsers { get; set; }
        public ICollection<Reservation>? ReservationUsers { get; set; }

        // Ajout de la collection pour les historiques
        public ICollection<Historique>? HistoriqueUsers { get; set; }
    }
}
