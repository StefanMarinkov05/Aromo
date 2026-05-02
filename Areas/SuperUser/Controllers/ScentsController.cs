using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aromo;
using Aromo.Models;
using Microsoft.AspNetCore.Authorization;

namespace Aromo.Areas.SuperUser.Controllers
{
    [Authorize(Roles = AddDefaultUsers.ADMIN_ROLE)]
    [Area("SuperUser")]
    public class ScentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScentsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: SuperUser/Scents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SuperUser/Scents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Scent scent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "PerfumeFeatures", new { area = "SuperUser" });
            }
            return View(scent);
        }

        // GET: SuperUser/Scents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scent = await _context.Scents.FindAsync(id);
            if (scent == null)
            {
                return NotFound();
            }
            return View(scent);
        }

        // POST: SuperUser/Scents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Scent scent)
        {
            if (id != scent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScentExists(scent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "PerfumeFeatures", new { area = "SuperUser" });
            }
            return View(scent);
        }

        // GET: SuperUser/Scents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scent = await _context.Scents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scent == null)
            {
                return NotFound();
            }

            return View(scent);
        }

        // POST: SuperUser/Scents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scent = await _context.Scents.FindAsync(id);
            if (scent != null)
            {
                _context.Scents.Remove(scent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "PerfumeFeatures", new { area = "SuperUser" });
        }

        private bool ScentExists(int id)
        {
            return _context.Scents.Any(e => e.Id == id);
        }
    }
}
