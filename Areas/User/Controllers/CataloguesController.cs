using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Aromo;
using Aromo.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aromo.Areas.User.Controllers
{
    [Authorize]
    [Area("User")]
    public class CataloguesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;
        public CataloguesController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment env) 
        {
            _context = context;
            _userManager = userManager;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var catalogues = await _context.Catalogues
                .Where(c => c.UserId == userId)
                .Include(c => c.Perfumes)
                    .ThenInclude(p => p.Brand)
                .OrderBy(cuid => cuid.UserId)
                    .ThenBy(cn => cn.Name)
                .ToListAsync();

            return View(catalogues);
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

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var catalogue = await GetOwnedCatalogue(id.Value);
            if (catalogue == null)
                return NotFound();

            ViewBag.ImageUrls = catalogue.Perfumes.ToDictionary(
                p => p.Id,
                p => GetImageUrl(p.Id)
            );

            return View(catalogue);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Catalogue catalogue)
        {
            catalogue.UserId = _userManager.GetUserId(User);

            catalogue.Id = 0;

            ModelState.Remove(nameof(catalogue.UserId));
            ModelState.Remove(nameof(catalogue.ApplicationUser));
            ModelState.Remove(nameof(catalogue.Id));

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return View(catalogue);
            }

            try
            {
                _context.Catalogues.Add(catalogue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Errors = new List<string> { ex.Message, ex.InnerException?.Message };
                return View(catalogue);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var catalogue = await GetOwnedCatalogue(id.Value);
            if (catalogue == null)
                return NotFound();

            return View(catalogue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Catalogue input)
        {
            if (id != input.Id)
                return NotFound();

            var catalogue = await GetOwnedCatalogue(id);
            if (catalogue == null)
                return NotFound();

            catalogue.UserId = _userManager.GetUserId(User);

            ModelState.Remove(nameof(catalogue.UserId));
            ModelState.Remove(nameof(catalogue.ApplicationUser));


            if (!ModelState.IsValid)
                return View(input);

            catalogue.Name = input.Name;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var catalogue = await GetOwnedCatalogue(id.Value);
            if (catalogue == null)
                return NotFound();

            return View(catalogue);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalogue = await GetOwnedCatalogue(id);
            if (catalogue == null)
                return NotFound();

            _context.Catalogues.Remove(catalogue);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }  
        private async Task<Catalogue?> GetOwnedCatalogue(int id)
        {
            var userId = _userManager.GetUserId(User);

            return await _context.Catalogues
                .Include(c => c.Perfumes)
                    .ThenInclude(p => p.Brand)
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPerfume(int catalogueId, int perfumeId)
        {
            var userId = _userManager.GetUserId(User);
            var catalogue = await _context.Catalogues
                .Include(c => c.Perfumes)
                .FirstOrDefaultAsync(c => c.Id == catalogueId && c.UserId == userId);

            if (catalogue == null)
                return NotFound();

            var perfume = await _context.Perfumes.FindAsync(perfumeId);
            if (perfume == null)
                return NotFound();

            if (!catalogue.Perfumes.Any(p => p.Id == perfumeId))
            {
                catalogue.Perfumes.Add(perfume);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Perfumes", new { area = "User" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePerfume(int catalogueId, int perfumeId, bool? perfumeIndex)
        {
            var userId = _userManager.GetUserId(User);
            var catalogue = await _context.Catalogues
                .Include(c => c.Perfumes)
                .FirstOrDefaultAsync(c => c.Id == catalogueId && c.UserId == userId);

            if (catalogue == null)
                return NotFound();

            var perfume = catalogue.Perfumes.FirstOrDefault(p => p.Id == perfumeId);
            if (perfume != null)
            {
                catalogue.Perfumes.Remove(perfume);
                await _context.SaveChangesAsync();
            }
            if (perfumeIndex != null)
            {
                return RedirectToAction("Index", "Perfumes", new { area = "User" });
            }
            return RedirectToAction("Index", "Catalogues", new { area = "User" });
        }

    }
}
