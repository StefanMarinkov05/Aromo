using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aromo.Models;
using Microsoft.AspNetCore.Hosting; // Ensure this is included

namespace Aromo.Areas.User.Controllers
{
    [Area("User")]
    public class PerfumesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public PerfumesController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
        }

        private static (int currentId, int[] priorityList) GetSeasonData(DateTime date)
        {
            float value = (float)date.Month + date.Day / 100f;
            int current = (value < 3.21 || value >= 12.22) ? 4 : (value < 6.21) ? 1 : (value < 9.23) ? 2 : 3;

            var remainingIds = new List<int> { 1234, 134, 124, 123, 34, 24, 23, 14, 13, 12, 4, 3, 2, 1 };
            var priority = new List<int>();
            int checkSeason = current;

            for (int i = 0; i < 4; i++)
            {
                var matches = remainingIds.Where(id => id.ToString().Contains(checkSeason.ToString())).ToList();
                matches.Reverse();
                priority.AddRange(matches);
                remainingIds.RemoveAll(id => matches.Contains(id));
                checkSeason = (checkSeason % 4) + 1;
            }
            return (current, priority.ToArray());
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

        public async Task<IActionResult> Index(string search, int? seasonId, string sortBy = "id", string sortDir = "asc")
        {
            var userId = _userManager.GetUserId(User);

            var perfumesQuery = _context.Perfumes
                .Include(p => p.Brand)
                .Include(p => p.Gender)
                .Include(p => p.Season)
                .Include(p => p.TopScents)
                .Include(p => p.HeartScents)
                .Include(p => p.BaseScents)
                .Include(p => p.Catalogues)
                .AsQueryable();

            if (seasonId.HasValue)
            {
                string sId = seasonId.Value.ToString();
                perfumesQuery = perfumesQuery.Where(p => p.SeasonId.ToString().Contains(sId));
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                perfumesQuery = perfumesQuery.Where(p =>
                    p.Id.ToString().Contains(search) ||
                    p.Name.ToLower().Contains(search) ||
                    (p.Original != null && p.Original.ToLower().Contains(search)) ||
                    p.Brand.Name.ToLower().Contains(search) ||
                    p.Gender.Name.ToLower().Contains(search) ||
                    p.Volume.ToString().Contains(search) ||
                    p.TopScents.Any(s => s.Name.ToLower().Contains(search)) ||
                    p.HeartScents.Any(s => s.Name.ToLower().Contains(search)) ||
                    p.BaseScents.Any(s => s.Name.ToLower().Contains(search))
                );
            }

            var seasonData = GetSeasonData(DateTime.Now);
            var priority = seasonData.priorityList;

            List<Perfume> perfumes;
            if (sortBy == "season")
            {
                var data = await perfumesQuery.ToListAsync();
                perfumes = (sortDir == "asc" ? data.OrderBy(p => Array.IndexOf(priority, p.SeasonId)) : data.OrderByDescending(p => Array.IndexOf(priority, p.SeasonId)))
                           .ThenBy(p => p.Name).ToList();
            }
            else
            {
                perfumesQuery = (sortBy, sortDir) switch
                {
                    ("name", "asc") => perfumesQuery.OrderBy(p => p.Name),
                    ("name", "desc") => perfumesQuery.OrderByDescending(p => p.Name),
                    ("brand", "asc") => perfumesQuery.OrderBy(p => p.Brand.Name),
                    ("brand", "desc") => perfumesQuery.OrderByDescending(p => p.Brand.Name),
                    ("price", "asc") => perfumesQuery.OrderBy(p => p.Price),
                    ("price", "desc") => perfumesQuery.OrderByDescending(p => p.Price),
                    _ => perfumesQuery.OrderBy(p => p.Id)
                };
                perfumes = await perfumesQuery.ToListAsync();
            }

            // Create Dictionary for Image URLs
            ViewBag.ImageUrls = perfumes.ToDictionary(p => p.Id, p => GetImageUrl(p.Id));

            ViewBag.SelectedSeason = seasonId;
            ViewBag.CurrentSeasonId = seasonData.currentId;
            ViewBag.Catalogues = userId == null ? new List<Catalogue>() : await _context.Catalogues.Where(c => c.UserId == userId).Include(c => c.Perfumes).ToListAsync();
            ViewBag.SortBy = sortBy;
            ViewBag.SortDir = sortDir;
            ViewBag.Search = search;

            var seasonCounts = new Dictionary<int, int>();
            var allPerfumesForCounts = await _context.Perfumes.Select(p => p.SeasonId).ToListAsync();
            for (int i = 1; i <= 4; i++) { seasonCounts[i] = allPerfumesForCounts.Count(id => id.ToString().Contains(i.ToString())); }
            ViewBag.SeasonCounts = seasonCounts;

            return View(perfumes);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var perfume = await _context.Perfumes
                .Include(p => p.Brand).Include(p => p.Gender).Include(p => p.Season)
                .Include(p => p.TopScents).Include(p => p.HeartScents).Include(p => p.BaseScents).Include(p => p.Catalogues)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (perfume == null) return NotFound();

            var userId = _userManager.GetUserId(User);
            ViewBag.Catalogues = await _context.Catalogues.Where(c => c.UserId == userId).ToListAsync();
            ViewBag.ImageUrl = GetImageUrl(perfume.Id);

            return View(perfume);
        }
    }
}