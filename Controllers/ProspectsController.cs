 using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProspectManagementTool.Data;
using ProspectManagementTool.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProspectManagementTool.Controllers
{
    [Authorize]
    public class ProspectsController : Controller
    {
        private readonly ProspectManagementContext _context;

        public ProspectsController(ProspectManagementContext context)
        {
            _context = context;
        }

        // GET: Prospects
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? TeamID, int? AttributeID, string actionButton, string sortDirection = "asc", string sortField="Team")
        {
            ViewData["TeamID"] = new SelectList(_context.Teams.OrderBy(a => a.HockeyTeam), "ID", "HockeyTeam");
            PopulateDropDownLists(); 
            ViewData["Filtering"] = "";

            var prospects = from p in _context.Prospects
                            .Include(p => p.Team).Include(p => p.Attribute)
                            select p;
            if (TeamID.HasValue)
            {
                prospects = prospects.Where(p => p.TeamID == TeamID);
                ViewData["Filtering"] = " show";
            }
            if (AttributeID.HasValue)
            {
                prospects = prospects.Where(p => p.AttributeID == AttributeID);
                ViewData["Filtering"] = " show";
            }
            if (!String.IsNullOrEmpty(actionButton))
            {
                if (actionButton != "Filter")
                {
                    if (actionButton == sortField)
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton;
                }
            }
            if(sortField == "Team")
            {
                if (sortDirection == "asc")
                {
                    prospects = prospects.OrderBy(p => p.Team.HockeyTeam).ThenBy(p => p.ProspectName);
                }
                else
                {
                    prospects = prospects.OrderByDescending(p => p.Team.HockeyTeam).ThenBy(p => p.ProspectName);
                }
            }
            else if (sortField == "Name")
            {
               if (sortDirection == "asc")
                {
                    prospects = prospects.OrderBy(p => p.ProspectName)
                        .ThenBy(p => p.Team.HockeyTeam);
                }
               else
                {
                    prospects = prospects.OrderByDescending(p => p.ProspectName)
                        .ThenBy(p => p.Team.HockeyTeam);
                }
            }
            else if (sortField == "Position")
            {
                if (sortDirection == "asc")
                {
                    prospects = prospects.OrderBy(p => p.ProspectPosition)
                        .ThenBy(p => p.Team.HockeyTeam);
                }
                else
                {
                    prospects = prospects.OrderByDescending(p => p.ProspectPosition)
                        .ThenBy(p => p.Team.HockeyTeam);
                }
            }
            else if (sortField == "Rating")
            {
                if (sortDirection == "asc")
                {
                    prospects = prospects.OrderByDescending(p => p.ProspectOV)
                        .ThenBy(p => p.Team.HockeyTeam);
                }
                else
                {
                    prospects = prospects.OrderBy(p => p.ProspectOV)
                        .ThenBy(p => p.Team.HockeyTeam);
                }
            }
            else if (sortField == "Player Type")
            {
                if (sortDirection == "asc")
                {
                    prospects = prospects.OrderBy(p => p.Attribute.AttributeName)
                        .ThenBy(p => p.Team.HockeyTeam);
                }
                else
                {
                    prospects = prospects.OrderByDescending(p => p.Attribute.AttributeName)
                        .ThenBy(p => p.Team.HockeyTeam);
                }
            }
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            return View(await prospects.ToListAsync());
        }
        [AllowAnonymous]
        // GET: Prospects/Details/5
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
            var prospect = new Prospect();
            PopulateDropDownLists();
            return View();
        }

        // POST: Prospects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProspectName,ProspectPosition,ProspectOV,ProspectPotential,ProspectAge,ProspectInitialRating,ProspectRerollRating,AttributeID,ProspectDraftedBy,TeamID")] Prospect prospect)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(prospect);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "CityName", prospect.TeamID);
                PopulateDropDownLists();
                return View(prospect);
            }
            catch (DbUpdateException dex)
            {
                if (dex.InnerException.Message.Contains("UNIQUE"))
                {
                    ModelState.AddModelError("", "Unable to save. Player already exists with same name, age, and position. Are you entering the same player twice?");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save. Check for duplicate entry and try again.");
                }
            }
            PopulateDropDownLists();
            return View(prospect);
        }

        // GET: Prospects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prospect = await _context.Prospects
                .Include(p => p.Team).Include(p => p.Attribute)
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.ID == id);

            if (prospect == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(prospect);
            return View(prospect);
        }

        // POST: Prospects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProspectName,ProspectPosition,ProspectOV,ProspectPotential,ProspectAge,ProspectInitialRating,ProspectRerollRating,AttributeID,TeamID")] Prospect prospect)
        {
            var prospectToUpdate = await _context.Prospects
                 .Include(p => p.Team).Include(p => p.Attribute)
                 .SingleOrDefaultAsync(p => p.ID == id);

            if (prospectToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Prospect>(prospectToUpdate, "", p => p.ProspectName, p => p.ProspectPosition, p => p.ProspectOV,
                p => p.ProspectPotential, p => p.ProspectAge, p => p.ProspectInitialRating, p => p.AttributeID, p => p.ProspectRerollRating, p => p.TeamID))
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
            PopulateDropDownLists(prospectToUpdate);
            return View(prospectToUpdate);

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
        private SelectList TeamSelectList(int? selectedId)
        {
            return new SelectList(_context.Teams
                .OrderBy(d => d.HockeyTeam), "ID", "HockeyTeam", selectedId);
        }

        private SelectList AttributeSelectList(int? selectedId)
        {
            return new SelectList(_context.Attributes.OrderBy(a => a.AttributeName), "ID", "AttributeName", selectedId);
        }

        [HttpGet]
        public JsonResult GetTeams(int? id)
        {
            return Json(TeamSelectList(id));
        }

        private void PopulateDropDownLists(Prospect prospect = null)
        {
            ViewData["TeamID"] = TeamSelectList(prospect?.TeamID);
            ViewData["AttributeID"] = AttributeSelectList(prospect?.AttributeID);
        }

        private bool ProspectExists(int id)
        {
            return _context.Prospects.Any(e => e.ID == id);
        }
    }
}
