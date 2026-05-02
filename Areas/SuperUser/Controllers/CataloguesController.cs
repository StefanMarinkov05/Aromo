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
    public class CataloguesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CataloguesController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        private string GetImageUrl(int perfumeId)
        {
            string baseDir = "images";
            string fileName = $"perfume{perfumeId}";

            if (System.IO.File.Exists(Path.Combine(_env.WebRootPath, baseDir, $"{fileName}.jpg")))
                return $"/{baseDir}/{fileName}.jpg";

            if (System.IO.File.Exists(Path.Combine(_env.WebRootPath, baseDir, $"{fileName}.png")))
                return $"/{baseDir}/{fileName}.png";

            return $"/{baseDir}/default.png";
        }

        // GET: SuperUser/Catalogues
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Catalogues
                .Include(c => c.ApplicationUser)
                .Include(p => p.Perfumes)
                .OrderBy(cuid => cuid.UserId)
                    .ThenBy(cn => cn.Name);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SuperUser/Catalogues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogue = await _context.Catalogues
                .Include(c => c.ApplicationUser)
                .Include(p => p.Perfumes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogue == null)
            {
                return NotFound();
            }
            ViewBag.ImageUrls = catalogue.Perfumes.ToDictionary(
                p => p.Id,
                p => GetImageUrl(p.Id)
            );

            return View(catalogue);
        }

        // GET: SuperUser/Catalogues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogue = await _context.Catalogues.FindAsync(id);
            if (catalogue == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", catalogue.UserId);
            return View(catalogue);
        }

        // POST: SuperUser/Catalogues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Name")] Catalogue catalogue)
        {
            if (id != catalogue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalogue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogueExists(catalogue.Id))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", catalogue.UserId);
            return View(catalogue);
        }

        // GET: SuperUser/Catalogues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogue = await _context.Catalogues
                .Include(c => c.ApplicationUser)
                .Include(p => p.Perfumes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogue == null)
            {
                return NotFound();
            }

            return View(catalogue);
        }

        // POST: SuperUser/Catalogues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalogue = await _context.Catalogues.FindAsync(id);
            if (catalogue != null)
            {
                _context.Catalogues.Remove(catalogue);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogueExists(int id)
        {
            return _context.Catalogues.Any(e => e.Id == id);
        }
    }
}
