namespace TrustCarCRM.Models
{
    // Table Historiques (pour l'audit des modifications)
    public class Historique
    {
        public int HistoriqueId { get; set; }
        public string? TableModifiee { get; set; }  // Nom de la table modifiée (Clients, Véhicules, Ventes, etc.)
        public int Id_Enregistrement { get; set; }  // ID de l'enregistrement modifié
        public string? TypeModification { get; set; }  // Création, Modification, Suppression
        public DateTime DateModification { get; set; }
        public int UtilisateurId { get; set; }  // ID de l'utilisateur ayant effectué la modification

        // Clé étrangère et relation
        public Utilisateur? UtilisateurHistoriques { get; set; }
    }
}
