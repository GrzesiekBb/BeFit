using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace BeFit.Controllers
{
    public class StatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatsController(ApplicationDbContext context)
        {
            _context = context;
        }
        private string GetUserId()
            => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        // GET: /Stats
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var fourWeeksAgo = DateTime.Now.AddDays(-28);

            var recentEntries = await _context.ExConn
                .Where(e => e.CreatedById == userId)
                .Where(e => e.SessionInfo.Start >= fourWeeksAgo)
                .Include(e => e.ExType)
                .Include(e => e.SessionInfo)
                .ToListAsync();

            var stats = recentEntries
                .GroupBy(e => e.ExType.Name)
                .Select(g => new ExerciseStatsViewModel
                {
                    ExerciseName = g.Key,
                    ExecutionCount = g.Count(),
                    TotalReps = g.Sum(e => e.Sets * e.RepsPerSet),
                    AverageLoad = g.Any() ? g.Average(e => e.Load) : 0,
                    MaxLoad = g.Any() ? g.Max(e => e.Load) : 0
                })
                .ToList();

            return View(stats);
        }

    }
}
