using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;

namespace BeFit.Controllers
{
    public class StatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Stats
        public async Task<IActionResult> Index()
        {
            var cutoff = DateTime.Now.AddDays(-28);

            var stats = await _context.ExConn
                .Include(e => e.ExType)
                .Include(e => e.SessionInfo)
                .Where(e => e.SessionInfo != null && e.SessionInfo.Start >= cutoff)
                .GroupBy(e => e.ExType!.Name)
                .Select(g => new ExerciseStatsViewModel
                {
                    ExerciseName = g.Key,
                    ExecutionCount = g.Count(),
                    TotalReps = g.Sum(x => x.Sets * x.RepsPerSet),
                    AverageLoad = g.Any()
                        ? g.Average(x => x.Load)
                        : 0,
                    MaxLoad = g.Any()
                        ? g.Max(x => x.Load)
                        : 0
                })
                .ToListAsync();

            return View(stats);
        }
    }
}
