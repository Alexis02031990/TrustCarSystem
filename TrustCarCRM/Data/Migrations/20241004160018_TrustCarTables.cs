using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrustCarCRM.Data.Migrations
{
    public partial class TrustCarTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id_Categorie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom_Categorie = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id_Categorie);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nom_Client = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Prenom_Client = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Adresse_Client = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email_Client = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Telephone_Client = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Date_Inscription = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    UtilisateurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom_Utilisateur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prenom_Utilisateur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email_Utilisateur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone_Utilisateur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password_Utilisateur = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.UtilisateurId);
                });

            migrationBuilder.CreateTable(
                name: "Vehicules",
                columns: table => new
                {
                    VehiculeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modele = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Annee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Couleur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kilometrage = table.Column<int>(type: "int", nullable: false),
                    Prix = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date_Ajout = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategorieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicules", x => x.VehiculeId);
                    table.ForeignKey(
                        name: "FK_Vehicules_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id_Categorie");
                });

            migrationBuilder.CreateTable(
                name: "Historiques",
                columns: table => new
                {
                    HistoriqueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableModifiee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_Enregistrement = table.Column<int>(type: "int", nullable: false),
                    TypeModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historiques", x => x.HistoriqueId);
                    table.ForeignKey(
                        name: "FK_Historiques_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "UtilisateurId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RendezVous",
                columns: table => new
                {
                    RendezVousId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VehiculeId = table.Column<int>(type: "int", nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    Date_RendezVous = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RendezVous", x => x.RendezVousId);
                    table.ForeignKey(
                        name: "FK_RendezVous_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RendezVous_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "UtilisateurId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RendezVous_Vehicules_VehiculeId",
                        column: x => x.VehiculeId,
                        principalTable: "Vehicules",
                        principalColumn: "VehiculeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VehiculeId = table.Column<int>(type: "int", nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    Date_Reservation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "UtilisateurId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Vehicules_VehiculeId",
                        column: x => x.VehiculeId,
                        principalTable: "Vehicules",
                        principalColumn: "VehiculeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ventes",
                columns: table => new
                {
                    VenteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VehiculeId = table.Column<int>(type: "int", nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    Date_Vente = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Montant_Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Methode_Paiement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statut_Paiement = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventes", x => x.VenteId);
                    table.ForeignKey(
                        name: "FK_Ventes_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventes_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "UtilisateurId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventes_Vehicules_VehiculeId",
                        column: x => x.VehiculeId,
                        principalTable: "Vehicules",
                        principalColumn: "VehiculeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Historiques_UtilisateurId",
                table: "Historiques",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_RendezVous_ClientId",
                table: "RendezVous",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RendezVous_UtilisateurId",
                table: "RendezVous",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_RendezVous_VehiculeId",
                table: "RendezVous",
                column: "VehiculeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ClientId",
                table: "Reservations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UtilisateurId",
                table: "Reservations",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_VehiculeId",
                table: "Reservations",
                column: "VehiculeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicules_CategorieId",
                table: "Vehicules",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventes_ClientId",
                table: "Ventes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventes_UtilisateurId",
                table: "Ventes",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventes_VehiculeId",
                table: "Ventes",
                column: "VehiculeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historiques");

            migrationBuilder.DropTable(
                name: "RendezVous");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Ventes");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Vehicules");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
