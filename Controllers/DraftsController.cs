using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProspectManagementTool.Data;
using ProspectManagementTool.Models;

namespace ProspectManagementTool.Controllers
{
    public class DraftsController : Controller
    {
        private readonly ProspectManagementContext _context;

        public DraftsController(ProspectManagementContext context)
        {
            _context = context;
        }

        // GET: Drafts
        public async Task<IActionResult> Index()
        {
            ViewData["TeamID"] = new SelectList(_context.Teams.OrderBy(a => a.HockeyTeam), "ID", "HockeyTeam");
            PopulateDropDownLists();

            var draft = from d in _context.Drafts
                        .Include(d => d.Team).ThenInclude(d => d.Prospects).Include(d => d.Attribute)
                        select d;
            return View(await draft.ToListAsync());
        }

        // GET: Drafts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var draft = await _context.Drafts.Include(d => d.Team).ThenInclude(d => d.Prospects).Include(d => d.Attribute)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (draft == null)
            {
                return NotFound();
            }

            return View(draft);
        }

        // GET: Drafts/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: Drafts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DraftName,DraftPosition,DraftOV,DraftPotential,DraftAge,AttributeID,DraftInitialRating,TeamID")] Draft draft)
        {
            if (ModelState.IsValid)
            {
                _context.Add(draft);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AttributeID"] = new SelectList(_context.Attributes, "ID", "AttributeName", draft.AttributeID);
            return View(draft);
        }

        // GET: Drafts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var draft = await _context.Drafts
                .Include(d => d.Attribute).Include(d => d.Team).ThenInclude(d =>d.Prospects)
                .AsNoTracking()
                .SingleOrDefaultAsync(d => d.ID == id);

            if (draft == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(draft);
            return View(draft);
        }

        // POST: Drafts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DraftName,DraftPosition,DraftOV,DraftPotential,DraftAge,AttributeID,DraftInitialRating,TeamID")] Draft draft)
        {
            var draftToUpdate = await _context.Drafts
       .Include(p => p.Team).Include(p => p.Attribute)
       .SingleOrDefaultAsync(p => p.ID == id);

            if (draftToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Draft>(draftToUpdate, "", d => d.DraftName, d => d.DraftPosition, d => d.DraftOV, d => d.DraftPotential,
                d => d.DraftAge, d => d.DraftInitialRating, d => d.AttributeID, d => d.TeamID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    throw;
                }
            }
            PopulateDropDownLists(draftToUpdate);
            return View(draftToUpdate);
        }

        // GET: Drafts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var draft = await _context.Drafts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (draft == null)
            {
                return NotFound();
            }

            return View(draft);
        }

        // POST: Drafts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var draft = await _context.Drafts.FindAsync(id);
            _context.Drafts.Remove(draft);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DraftExists(int id)
        {
            return _context.Drafts.Any(e => e.ID == id);
        }

        private SelectList AttributeSelectList(int? selectedId)
        {
            return new SelectList(_context.Attributes.OrderBy(d => d.AttributeName), "ID", "AttributeName", selectedId);
        }
        private SelectList TeamSelectList(int? selectedId)
        {
            return new SelectList(_context.Teams
                .OrderBy(d => d.HockeyTeam), "ID", "HockeyTeam", selectedId);
        }
        private void PopulateDropDownLists(Draft draft = null)
        {
            ViewData["TeamID"] = TeamSelectList(draft?.TeamID);
            ViewData["AttributeID"] = AttributeSelectList(draft?.AttributeID);
        }
    }
}
