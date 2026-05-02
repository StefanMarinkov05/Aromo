using Aromo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aromo.Areas.SuperUser.Controllers
{
    [Authorize(Roles = AddDefaultUsers.ADMIN_ROLE)]
    [Area("SuperUser")]
    public class PerfumeFeaturesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PerfumeFeaturesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: PerfumeFeaturesController
        public async Task<IActionResult> Index()
        {
            var model = new PerfumeFeaturesViewModel
            {
                Brands = await _context.Brands
                    .OrderBy(b => b.Name)
                    .ToListAsync(),

                Genders = await _context.Genders
                    .OrderBy(g => g.Name)
                    .ToListAsync(),

                Seasons = await _context.Seasons
                    .OrderBy(s => s.Id.ToString())
                    .ToListAsync(),
                Scents = await _context.Scents
                    .OrderBy(s => s.Name)
                    .ToListAsync()
            };

            return View(model);
        }
    }
}
