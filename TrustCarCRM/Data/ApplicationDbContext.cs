using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrustCarCRM.Models;

namespace TrustCarCRM.Data
{
    public class ApplicationDbContext : IdentityDbContext // Héritage d'IdentityDbContext<Client>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Suppression de DbSet<Client>, IdentityDbContext le gère déjà.
        // Ajout du DbSet pour les Utilisateurs (agents/employés)
        public DbSet<Utilisateur> Utilisateurs { get; set; }

        // Autres DbSets pour les entités du système
        public DbSet<Vente> Ventes { get; set; }
        public DbSet<Vehicule> Vehicules { get; set; }
        public DbSet<RendezVous> RendezVous { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Historique> Historiques { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  // Nécessaire pour Identity

            // Configuration des clés primaires
            modelBuilder.Entity<Vente>().HasKey(v => v.VenteId);
            modelBuilder.Entity<Vehicule>().HasKey(v => v.VehiculeId);
            modelBuilder.Entity<RendezVous>().HasKey(r => r.RendezVousId);
            modelBuilder.Entity<Reservation>().HasKey(r => r.ReservationId);
            modelBuilder.Entity<Historique>().HasKey(h => h.HistoriqueId);
            modelBuilder.Entity<Categorie>().HasKey(c => c.Id_Categorie);

            // Relation un-à-plusieurs entre Vehicule et Categorie
            modelBuilder.Entity<Vehicule>()
                .HasOne(v => v.Categorie)
                .WithMany(c => c.VehiculeCategories)
                .HasForeignKey(v => v.CategorieId);

            // Relation un-à-plusieurs entre Vente et Client
            modelBuilder.Entity<Vente>()
                .HasOne(v => v.ClientVentes)
                .WithMany(c => c.VenteClients)
                .HasForeignKey(v => v.ClientId)  // ClientId est de type string (IdentityUser.Id)
                .HasPrincipalKey(c => c.Id);

            // Relation un-à-plusieurs entre Vente et Vehicule
            modelBuilder.Entity<Vente>()
                .HasOne(v => v.VehiculeVentes)
                .WithMany(ve => ve.VenteVehicules)
                .HasForeignKey(v => v.VehiculeId);

            // Relation un-à-plusieurs entre RendezVous et Client
            modelBuilder.Entity<RendezVous>()
                .HasOne(r => r.ClientRendezVous)
                .WithMany(c => c.RendezVousClients)
                .HasForeignKey(r => r.ClientId)  // ClientId de type string
                .HasPrincipalKey(c => c.Id);

            // Relation un-à-plusieurs entre RendezVous et Vehicule
            modelBuilder.Entity<RendezVous>()
                .HasOne(r => r.VehiculeRendezVous)
                .WithMany(v => v.RendezVousVehicules)
                .HasForeignKey(r => r.VehiculeId);

            // Relation un-à-plusieurs entre Reservation et Client
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.ClientReservations)
                .WithMany(c => c.ReservationsClients)
                .HasForeignKey(r => r.ClientId)  // ClientId de type string
                .HasPrincipalKey(c => c.Id);

            // Relation un-à-plusieurs entre Reservation et Vehicule
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.VehiculeReservations)
                .WithMany(v => v.ReservationsVehicules)
                .HasForeignKey(r => r.VehiculeId);

            // Relation un-à-plusieurs entre Historique et Utilisateur
            modelBuilder.Entity<Historique>()
                .HasOne(h => h.UtilisateurHistoriques)
                .WithMany(u => u.HistoriqueUsers)
                .HasForeignKey(h => h.UtilisateurId);

            // Ajout de la configuration spécifique pour Client
            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Nom_Client).HasMaxLength(100);
                entity.Property(e => e.Prenom_Client).HasMaxLength(100);
                entity.Property(e => e.Adresse_Client).HasMaxLength(255);
                entity.Property(e => e.Email_Client).HasMaxLength(255);
                entity.Property(e => e.Telephone_Client).HasMaxLength(20);
            });
        }
    }
}
