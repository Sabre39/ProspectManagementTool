using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProspectManagementTool.Data;
using ProspectManagementTool.Models;
using ProspectManagementTool.ViewModels;

namespace ProspectManagementTool.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProspectManagementContext _context;

        public HomeController(ProspectManagementContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var team = from t in _context.Teams.Include(t => t.Prospects).ThenInclude(t => t.Attribute)
                        select t;
            return View(await team.ToListAsync());
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prospect = await _context.Prospects
                .Include(p => p.Team).Include(p => p.Attribute)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (prospect == null)
            {
                return NotFound();
            }

            return View(prospect);
        }

        // GET: Prospects/Create
        public IActionResult Create()
        {
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "CityName");
            return View();
        }

        // POST: Prospects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProspectName,ProspectPosition,ProspectOV,ProspectPotential,ProspectAge,ProspectInitialRating,TeamID")] Prospect prospect)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prospect);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "CityName", prospect.TeamID);
            return View(prospect);
        }

        // GET: Prospects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prospect = await _context.Prospects.FindAsync(id);
            if (prospect == null)
            {
                return NotFound();
            }
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "CityName", prospect.TeamID);
            return View(prospect);
        }

        // POST: Prospects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProspectName,ProspectPosition,ProspectOV,ProspectPotential,ProspectAge,ProspectInitialRating,TeamID")] Prospect prospect)
        {
            if (id != prospect.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prospect);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProspectExists(prospect.ID))
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
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "CityName", prospect.TeamID);
            return View(prospect);
        }

        // GET: Prospects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prospect = await _context.Prospects
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (prospect == null)
            {
                return NotFound();
            }

            return View(prospect);
        }

        // POST: Prospects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prospect = await _context.Prospects.FindAsync(id);
            _context.Prospects.Remove(prospect);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProspectExists(int id)
        {
            return _context.Prospects.Any(e => e.ID == id);
        }
    }  
}