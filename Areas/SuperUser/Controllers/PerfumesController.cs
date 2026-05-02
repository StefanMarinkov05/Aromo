using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aromo.Models;
using NuGet.Packaging;
using Microsoft.AspNetCore.Authorization;

namespace Aromo.Areas.SuperUser.Controllers
{
    [Authorize(Roles = AddDefaultUsers.ADMIN_ROLE)]
    [Area("SuperUser")]
    public class PerfumesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PerfumesController(ApplicationDbContext context, IWebHostEnvironment env)
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

        // GET: SuperUser/Perfumes
        public async Task<IActionResult> Index()
        {
            var perfumes = _context.Perfumes
                .Include(p => p.Brand)
                .Include(p => p.Gender)
                .Include(p => p.Season)
                .Include(p => p.TopScents)
                .Include(p => p.HeartScents)
                .Include(p => p.BaseScents);

            ViewBag.ImageUrls = perfumes.ToDictionary(p => p.Id, p => GetImageUrl(p.Id));

            return View(await perfumes.ToListAsync());
        }

        // GET: SuperUser/Perfumes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var perfume = await _context.Perfumes
                .Include(p => p.Brand)
                .Include(p => p.Gender)
                .Include(p => p.Season)
                .Include(p => p.TopScents)
                .Include(p => p.HeartScents)
                .Include(p => p.BaseScents)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (perfume == null) return NotFound();
            ViewBag.ImageUrl = GetImageUrl(perfume.Id);

            return View(perfume);
        }

        // GET: SuperUser/Perfumes/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name");
            ViewData["SeasonId"] = new SelectList(_context.Seasons, "Id", "Name");

            ViewData["TopScentsIds"] = new MultiSelectList(_context.Scents, "Id", "Name");
            ViewData["HeartScentsIds"] = new MultiSelectList(_context.Scents, "Id", "Name");
            ViewData["BaseScentsIds"] = new MultiSelectList(_context.Scents, "Id", "Name");

            return View();
        }

        // POST: SuperUser/Perfumes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Perfume perfume,
            int[] TopScentsIds,
            int[] HeartScentsIds,
            int[] BaseScentsIds,
            IFormFile? image)
        {
            // Remove navigation validation
            ModelState.Remove(nameof(perfume.Brand));
            ModelState.Remove(nameof(perfume.Gender));
            ModelState.Remove(nameof(perfume.Season));
            ModelState.Remove(nameof(perfume.TopScents));
            ModelState.Remove(nameof(perfume.HeartScents));
            ModelState.Remove(nameof(perfume.BaseScents));

            if (ModelState.IsValid)
            {
                perfume.TopScents = await _context.Scents
                    .Where(s => TopScentsIds.Contains(s.Id))
                    .ToListAsync();

                perfume.HeartScents = await _context.Scents
                    .Where(s => HeartScentsIds.Contains(s.Id))
                    .ToListAsync();

                perfume.BaseScents = await _context.Scents
                    .Where(s => BaseScentsIds.Contains(s.Id))
                    .ToListAsync();

                _context.Add(perfume);
                await _context.SaveChangesAsync();

                await SavePerfumeImageAsync(perfume.Id, image);

                return RedirectToAction(nameof(Index));
            }

            ReloadDropdowns(perfume, TopScentsIds, HeartScentsIds, BaseScentsIds);
            return View(perfume);
        }

        // GET: SuperUser/Perfumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var perfume = await _context.Perfumes
                .Include(p => p.TopScents)
                .Include(p => p.HeartScents)
                .Include(p => p.BaseScents)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (perfume == null) return NotFound();

            ReloadDropdowns(
                perfume,
                perfume.TopScents.Select(s => s.Id).ToArray(),
                perfume.HeartScents.Select(s => s.Id).ToArray(),
                perfume.BaseScents.Select(s => s.Id).ToArray()
            );
            ViewBag.ImageUrl = GetImageUrl(perfume.Id);

            return View(perfume);
        }

        // POST: SuperUser/Perfumes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Perfume perfume,
            int[] TopScentsIds,
            int[] HeartScentsIds,
            int[] BaseScentsIds,
            IFormFile? image)
        {
            if (id != perfume.Id) return NotFound();

            ModelState.Remove(nameof(perfume.Brand));
            ModelState.Remove(nameof(perfume.Gender));
            ModelState.Remove(nameof(perfume.Season));
            ModelState.Remove(nameof(perfume.TopScents));
            ModelState.Remove(nameof(perfume.HeartScents));
            ModelState.Remove(nameof(perfume.BaseScents));

            if (ModelState.IsValid)
            {
                var perfumeToUpdate = await _context.Perfumes
                    .Include(p => p.TopScents)
                    .Include(p => p.HeartScents)
                    .Include(p => p.BaseScents)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (perfumeToUpdate == null) return NotFound();

                _context.Entry(perfumeToUpdate).CurrentValues.SetValues(perfume);

                perfumeToUpdate.TopScents.Clear();
                perfumeToUpdate.TopScents.AddRange(
                    await _context.Scents.Where(s => TopScentsIds.Contains(s.Id)).ToListAsync()
                );

                perfumeToUpdate.HeartScents.Clear();
                perfumeToUpdate.HeartScents.AddRange(
                    await _context.Scents.Where(s => HeartScentsIds.Contains(s.Id)).ToListAsync()
                );

                perfumeToUpdate.BaseScents.Clear();
                perfumeToUpdate.BaseScents.AddRange(
                    await _context.Scents.Where(s => BaseScentsIds.Contains(s.Id)).ToListAsync()
                );

                await SavePerfumeImageAsync(perfumeToUpdate.Id, image);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ReloadDropdowns(perfume, TopScentsIds, HeartScentsIds, BaseScentsIds);
            return View(perfume);
        }

        // GET: SuperUser/Perfumes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var perfume = await _context.Perfumes
                .Include(p => p.Brand)
                .Include(p => p.Gender)
                .Include(p => p.Season)
                .Include(p => p.TopScents)
                .Include(p => p.HeartScents)
                .Include(p => p.BaseScents)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (perfume == null) return NotFound();
            ViewBag.ImageUrl = GetImageUrl(perfume.Id);

            return View(perfume);
        }

        // POST: SuperUser/Perfumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var perfume = await _context.Perfumes.FindAsync(id);

            if (perfume != null)
            {
                // 🧹 Delete image file
                var imagesPath = Path.Combine(_env.WebRootPath, "images");
                var imagePath = Path.Combine(imagesPath, $"perfume{id}.jpg");

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                _context.Perfumes.Remove(perfume);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private void ReloadDropdowns(
            Perfume perfume,
            int[] topIds,
            int[] heartIds,
            int[] baseIds)
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", perfume.BrandId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name", perfume.GenderId);
            ViewData["SeasonId"] = new SelectList(_context.Seasons, "Id", "Name", perfume.SeasonId);

            ViewData["TopScentsIds"] = new MultiSelectList(_context.Scents, "Id", "Name", topIds);
            ViewData["HeartScentsIds"] = new MultiSelectList(_context.Scents, "Id", "Name", heartIds);
            ViewData["BaseScentsIds"] = new MultiSelectList(_context.Scents, "Id", "Name", baseIds);
        }

        private async Task SavePerfumeImageAsync(int perfumeId, IFormFile? image)
        {
            if (image == null || image.Length == 0) return;

            var imagesPath = Path.Combine(_env.WebRootPath, "images");
            Directory.CreateDirectory(imagesPath);

            var extension = Path.GetExtension(image.FileName);
            var filePath = Path.Combine(imagesPath, $"perfume{perfumeId}{extension}");

            using var stream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(stream);
        }
    }
}
