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
    public class VehiculesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiculesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehicules
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vehicules.Include(v => v.Categorie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vehicules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vehicules == null)
            {
                return NotFound();
            }

            var vehicule = await _context.Vehicules
                .Include(v => v.Categorie)
                .FirstOrDefaultAsync(m => m.VehiculeId == id);
            if (vehicule == null)
            {
                return NotFound();
            }

            return View(vehicule);
        }

        // GET: Vehicules/Create
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id_Categorie", "Id_Categorie");
            return View();
        }

        // POST: Vehicules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehiculeId,Marque,Modele,Annee,Couleur,Kilometrage,Prix,Date_Ajout,Statut,CategorieId")] Vehicule vehicule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id_Categorie", "Id_Categorie", vehicule.CategorieId);
            return View(vehicule);
        }

        // GET: Vehicules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehicules == null)
            {
                return NotFound();
            }

            var vehicule = await _context.Vehicules.FindAsync(id);
            if (vehicule == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id_Categorie", "Id_Categorie", vehicule.CategorieId);
            return View(vehicule);
        }

        // POST: Vehicules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehiculeId,Marque,Modele,Annee,Couleur,Kilometrage,Prix,Date_Ajout,Statut,CategorieId")] Vehicule vehicule)
        {
            if (id != vehicule.VehiculeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiculeExists(vehicule.VehiculeId))
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
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id_Categorie", "Id_Categorie", vehicule.CategorieId);
            return View(vehicule);
        }

        // GET: Vehicules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehicules == null)
            {
                return NotFound();
            }

            var vehicule = await _context.Vehicules
                .Include(v => v.Categorie)
                .FirstOrDefaultAsync(m => m.VehiculeId == id);
            if (vehicule == null)
            {
                return NotFound();
            }

            return View(vehicule);
        }

        // POST: Vehicules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vehicules == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vehicules'  is null.");
            }
            var vehicule = await _context.Vehicules.FindAsync(id);
            if (vehicule != null)
            {
                _context.Vehicules.Remove(vehicule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiculeExists(int id)
        {
          return (_context.Vehicules?.Any(e => e.VehiculeId == id)).GetValueOrDefault();
        }
    }
}
