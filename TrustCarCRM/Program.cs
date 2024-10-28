using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrustCarCRM.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuration de la chaîne de connexion à la base de données
var connectionString = builder.Configuration.GetConnectionString("CRMConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configuration d'Identity pour la gestion des utilisateurs
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Ajout des services pour les contrôleurs et les vues
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuration du pipeline HTTP (middleware)
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Middleware pour la sécurité
app.UseHttpsRedirection();
app.UseStaticFiles();

// Configuration des routes
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Gestion des pages Razor (si utilisées dans le projet)
app.MapRazorPages();

app.Run();
