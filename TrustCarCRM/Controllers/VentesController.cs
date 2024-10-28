using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrustCarCRM.Data;
using TrustCarCRM.Models;

namespace TrustCarCRM.Controllers
{
    public class VentesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VentesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ventes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ventes.Include(v => v.ClientVentes).Include(v => v.UtilisateurVentes).Include(v => v.VehiculeVentes);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ventes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ventes == null)
            {
                return NotFound();
            }

            var vente = await _context.Ventes
                .Include(v => v.ClientVentes)
                .Include(v => v.UtilisateurVentes)
                .Include(v => v.VehiculeVentes)
                .FirstOrDefaultAsync(m => m.VenteId == id);
            if (vente == null)
            {
                return NotFound();
            }

            return View(vente);
        }

        // GET: Ventes/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id");
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "UtilisateurId", "UtilisateurId");
            ViewData["VehiculeId"] = new SelectList(_context.Vehicules, "VehiculeId", "VehiculeId");
            return View();
        }

        // POST: Ventes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenteId,ClientId,VehiculeId,UtilisateurId,Date_Vente,Montant_Total,Methode_Paiement,Statut_Paiement")] Vente vente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", vente.ClientId);
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "UtilisateurId", "UtilisateurId", vente.UtilisateurId);
            ViewData["VehiculeId"] = new SelectList(_context.Vehicules, "VehiculeId", "VehiculeId", vente.VehiculeId);
            return View(vente);
        }

        // GET: Ventes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ventes == null)
            {
                return NotFound();
            }

            var vente = await _context.Ventes.FindAsync(id);
            if (vente == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", vente.ClientId);
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "UtilisateurId", "UtilisateurId", vente.UtilisateurId);
            ViewData["VehiculeId"] = new SelectList(_context.Vehicules, "VehiculeId", "VehiculeId", vente.VehiculeId);
            return View(vente);
        }

        // POST: Ventes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VenteId,ClientId,VehiculeId,UtilisateurId,Date_Vente,Montant_Total,Methode_Paiement,Statut_Paiement")] Vente vente)
        {
            if (id != vente.VenteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenteExists(vente.VenteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", vente.ClientId);
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "UtilisateurId", "UtilisateurId", vente.UtilisateurId);
            ViewData["VehiculeId"] = new SelectList(_context.Vehicules, "VehiculeId", "VehiculeId", vente.VehiculeId);
            return View(vente);
        }

        // GET: Ventes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ventes == null)
            {
                return NotFound();
            }

            var vente = await _context.Ventes
                .Include(v => v.ClientVentes)
                .Include(v => v.UtilisateurVentes)
                .Include(v => v.VehiculeVentes)
                .FirstOrDefaultAsync(m => m.VenteId == id);
            if (vente == null)
            {
                return NotFound();
            }

            return View(vente);
        }

        // POST: Ventes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ventes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Ventes'  is null.");
            }
            var vente = await _context.Ventes.FindAsync(id);
            if (vente != null)
            {
                _context.Ventes.Remove(vente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenteExists(int id)
        {
          return (_context.Ventes?.Any(e => e.VenteId == id)).GetValueOrDefault();
        }
    }
}
