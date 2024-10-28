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
    public class HistoriquesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoriquesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Historiques
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Historiques.Include(h => h.UtilisateurHistoriques);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Historiques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Historiques == null)
            {
                return NotFound();
            }

            var historique = await _context.Historiques
                .Include(h => h.UtilisateurHistoriques)
                .FirstOrDefaultAsync(m => m.HistoriqueId == id);
            if (historique == null)
            {
                return NotFound();
            }

            return View(historique);
        }

        // GET: Historiques/Create
        public IActionResult Create()
        {
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "UtilisateurId", "UtilisateurId");
            return View();
        }

        // POST: Historiques/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistoriqueId,TableModifiee,Id_Enregistrement,TypeModification,DateModification,UtilisateurId")] Historique historique)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historique);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "UtilisateurId", "UtilisateurId", historique.UtilisateurId);
            return View(historique);
        }

        // GET: Historiques/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Historiques == null)
            {
                return NotFound();
            }

            var historique = await _context.Historiques.FindAsync(id);
            if (historique == null)
            {
                return NotFound();
            }
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "UtilisateurId", "UtilisateurId", historique.UtilisateurId);
            return View(historique);
        }

        // POST: Historiques/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistoriqueId,TableModifiee,Id_Enregistrement,TypeModification,DateModification,UtilisateurId")] Historique historique)
        {
            if (id != historique.HistoriqueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historique);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoriqueExists(historique.HistoriqueId))
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
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "UtilisateurId", "UtilisateurId", historique.UtilisateurId);
            return View(historique);
        }

        // GET: Historiques/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Historiques == null)
            {
                return NotFound();
            }

            var historique = await _context.Historiques
                .Include(h => h.UtilisateurHistoriques)
                .FirstOrDefaultAsync(m => m.HistoriqueId == id);
            if (historique == null)
            {
                return NotFound();
            }

            return View(historique);
        }

        // POST: Historiques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Historiques == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Historiques'  is null.");
            }
            var historique = await _context.Historiques.FindAsync(id);
            if (historique != null)
            {
                _context.Historiques.Remove(historique);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoriqueExists(int id)
        {
          return (_context.Historiques?.Any(e => e.HistoriqueId == id)).GetValueOrDefault();
        }
    }
}
