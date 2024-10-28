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
    public class RendezVousController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RendezVousController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RendezVous
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RendezVous.Include(r => r.ClientRendezVous).Include(r => r.UtilisateurRendezVous).Include(r => r.VehiculeRendezVous);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RendezVous/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RendezVous == null)
            {
                return NotFound();
            }

            var rendezVous = await _context.RendezVous
                .Include(r => r.ClientRendezVous)
                .Include(r => r.UtilisateurRendezVous)
                .Include(r => r.VehiculeRendezVous)
                .FirstOrDefaultAsync(m => m.RendezVousId == id);
            if (rendezVous == null)
            {
                return NotFound();
            }

            return View(rendezVous);
        }

        // GET: RendezVous/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id");
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "UtilisateurId", "UtilisateurId");
            ViewData["VehiculeId"] = new SelectList(_context.Vehicules, "VehiculeId", "VehiculeId");
            return View();
        }

        // POST: RendezVous/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RendezVousId,ClientId,VehiculeId,UtilisateurId,Date_RendezVous,Statut")] RendezVous rendezVous)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rendezVous);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", rendezVous.ClientId);
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "UtilisateurId", "UtilisateurId", rendezVous.UtilisateurId);
            ViewData["VehiculeId"] = new SelectList(_context.Vehicules, "VehiculeId", "VehiculeId", rendezVous.VehiculeId);
            return View(rendezVous);
        }

        // GET: RendezVous/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RendezVous == null)
            {
                return NotFound();
            }

            var rendezVous = await _context.RendezVous.FindAsync(id);
            if (rendezVous == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", rendezVous.ClientId);
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "UtilisateurId", "UtilisateurId", rendezVous.UtilisateurId);
            ViewData["VehiculeId"] = new SelectList(_context.Vehicules, "VehiculeId", "VehiculeId", rendezVous.VehiculeId);
            return View(rendezVous);
        }

        // POST: RendezVous/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RendezVousId,ClientId,VehiculeId,UtilisateurId,Date_RendezVous,Statut")] RendezVous rendezVous)
        {
            if (id != rendezVous.RendezVousId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rendezVous);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RendezVousExists(rendezVous.RendezVousId))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", rendezVous.ClientId);
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "UtilisateurId", "UtilisateurId", rendezVous.UtilisateurId);
            ViewData["VehiculeId"] = new SelectList(_context.Vehicules, "VehiculeId", "VehiculeId", rendezVous.VehiculeId);
            return View(rendezVous);
        }

        // GET: RendezVous/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RendezVous == null)
            {
                return NotFound();
            }

            var rendezVous = await _context.RendezVous
                .Include(r => r.ClientRendezVous)
                .Include(r => r.UtilisateurRendezVous)
                .Include(r => r.VehiculeRendezVous)
                .FirstOrDefaultAsync(m => m.RendezVousId == id);
            if (rendezVous == null)
            {
                return NotFound();
            }

            return View(rendezVous);
        }

        // POST: RendezVous/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RendezVous == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RendezVous'  is null.");
            }
            var rendezVous = await _context.RendezVous.FindAsync(id);
            if (rendezVous != null)
            {
                _context.RendezVous.Remove(rendezVous);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RendezVousExists(int id)
        {
          return (_context.RendezVous?.Any(e => e.RendezVousId == id)).GetValueOrDefault();
        }
    }
}
