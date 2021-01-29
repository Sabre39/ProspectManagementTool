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
    public class AttributesController : Controller
    {
        private readonly ProspectManagementContext _context;

        public AttributesController(ProspectManagementContext context)
        {
            _context = context;
        }

        // GET: Attributes
        public async Task<IActionResult> Index()
        {
            var attributes = from a in _context.Attributes.OrderBy(a => a.AttributeName)
                        select a;
            return View(await attributes.ToListAsync());
        }

        // GET: Attributes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attribute = await _context.Attributes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (attribute == null)
            {
                return NotFound();
            }

            return View(attribute);
        }

        // GET: Attributes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attributes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AttributeName")] Models.Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attribute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(attribute);
        }

        // GET: Attributes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attribute = await _context.Attributes.FindAsync(id);
            if (attribute == null)
            {
                return NotFound();
            }
            return View(attribute);
        }

        // POST: Attributes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AttributeName")] Models.Attribute attribute)
        {
            if (id != attribute.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attribute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttributeExists(attribute.ID))
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
            return View(attribute);
        }

        // GET: Attributes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attribute = await _context.Attributes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (attribute == null)
            {
                return NotFound();
            }

            return View(attribute);
        }

        // POST: Attributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attribute = await _context.Attributes.FindAsync(id);
            try
            {
                _context.Attributes.Remove(attribute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dex)
            {
                if (dex.InnerException.Message.Contains("FK_Prospects_Attribute_AttributeID"))
                {
                    ModelState.AddModelError("", "Cannot delete an attribute that is assigned to players.");
                }
                else
                {
                    ModelState.AddModelError("", "Delete failed. Please try again.");
                }
            }
            return View(attribute);
        }

                private bool AttributeExists(int id)
        {
            return _context.Attributes.Any(e => e.ID == id);
        }
    }
}
