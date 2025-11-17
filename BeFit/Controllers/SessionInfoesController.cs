using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;

namespace BeFit.Controllers
{
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
            return View(await _context.SessionInfo.ToListAsync());
        }

        // GET: SessionInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionInfo = await _context.SessionInfo
                .FirstOrDefaultAsync(m => m.SessionId == id);
            if (sessionInfo == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> Create([Bind("SessionId,Start,End")] SessionInfo sessionInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sessionInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sessionInfo);
        }

        // GET: SessionInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionInfo = await _context.SessionInfo.FindAsync(id);
            if (sessionInfo == null)
            {
                return NotFound();
            }
            return View(sessionInfo);
        }

        // POST: SessionInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SessionId,Start,End")] SessionInfo sessionInfo)
        {
            if (id != sessionInfo.SessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sessionInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionInfoExists(sessionInfo.SessionId))
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
            return View(sessionInfo);
        }

        // GET: SessionInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionInfo = await _context.SessionInfo
                .FirstOrDefaultAsync(m => m.SessionId == id);
            if (sessionInfo == null)
            {
                return NotFound();
            }

            return View(sessionInfo);
        }

        // POST: SessionInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sessionInfo = await _context.SessionInfo.FindAsync(id);
            if (sessionInfo != null)
            {
                _context.SessionInfo.Remove(sessionInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionInfoExists(int id)
        {
            return _context.SessionInfo.Any(e => e.SessionId == id);
        }
    }
}
