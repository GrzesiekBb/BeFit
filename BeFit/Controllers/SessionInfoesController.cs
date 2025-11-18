using BeFit.Data;
using BeFit.Models;
using BeFit.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;



namespace BeFit.Controllers
{
    [Authorize]
    public class SessionInfoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SessionInfoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SessionInfoes
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();

            var sessions = _context.SessionInfo
                .Where(s => s.CreatedById == userId);

            return View(await sessions.ToListAsync());
        }


        // GET: SessionInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = GetUserId();

            var sessionInfo = await _context.SessionInfo
                .FirstOrDefaultAsync(m => m.SessionId == id && m.CreatedById == userId);

            if (sessionInfo == null)
                return NotFound();

            return View(sessionInfo);
        }


        // GET: SessionInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SessionInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SessionInfoDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var session = new SessionInfo
            {
                Start = dto.Start,
                End = dto.End,
                CreatedById = GetUserId()
            };

            _context.Add(session);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: SessionInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = GetUserId();

            var sessionInfo = await _context.SessionInfo
                .FirstOrDefaultAsync(s => s.SessionId == id && s.CreatedById == userId);

            if (sessionInfo == null)
                return NotFound();

            return View(sessionInfo);
        }


        // POST: SessionInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SessionInfo sessionInfo)
        {
            if (id != sessionInfo.SessionId)
                return NotFound();

            var userId = GetUserId();

            // Czy ta sesja naprawdę należy do użytkownika?
            var exists = await _context.SessionInfo
                .AnyAsync(s => s.SessionId == id && s.CreatedById == userId);

            if (!exists)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Upewniamy się, że nie nadpiszemy CreatedById
                    sessionInfo.CreatedById = userId;

                    _context.Update(sessionInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.SessionInfo.Any(e => e.SessionId == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(sessionInfo);
        }


        // GET: SessionInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = GetUserId();

            var sessionInfo = await _context.SessionInfo
                .FirstOrDefaultAsync(m => m.SessionId == id && m.CreatedById == userId);

            if (sessionInfo == null)
                return NotFound();

            return View(sessionInfo);
        }


        // POST: SessionInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = GetUserId();

            var sessionInfo = await _context.SessionInfo
                .FirstOrDefaultAsync(s => s.SessionId == id && s.CreatedById == userId);

            if (sessionInfo != null)
            {
                _context.SessionInfo.Remove(sessionInfo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool SessionInfoExists(int id)
        {
            return _context.SessionInfo.Any(e => e.SessionId == id);
        }
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

    }
}
