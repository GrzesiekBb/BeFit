using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;
using BeFit.Models.DTO;
using System.Security.Claims;


namespace BeFit.Controllers
{
    public class ExConnsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExConnsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExConns
        public async Task<IActionResult> Index()
        {
            var exConns = _context.ExConn
                .Include(e => e.ExType)
                .Include(e => e.SessionInfo);

            return View(await exConns.ToListAsync());
        }


        // GET: ExConns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exConn = await _context.ExConn
                .FirstOrDefaultAsync(m => m.ConnId == id);
            if (exConn == null)
            {
                return NotFound();
            }

            return View(exConn);
        }

        // GET: ExConns/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.ExType, "Id", "Name");
            ViewData["SessionId"] = new SelectList(_context.SessionInfo, "SessionId", "Start");
            return View();
        }


        // POST: ExConns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExConnDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewData["TypeId"] = new SelectList(_context.ExType, "Id", "Name", dto.TypeId);
                ViewData["SessionId"] = new SelectList(_context.SessionInfo, "SessionId", "Start", dto.SessionId);
                return View(dto);
            }

            var exConn = new ExConn
            {
                TypeId = dto.TypeId,
                SessionId = dto.SessionId,
                Sets = dto.Sets,
                RepsPerSet = dto.RepsPerSet,
                Load = dto.Load,
                CreatedById = GetUserId()
            };

            _context.Add(exConn);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }





        // GET: ExConns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exConn = await _context.ExConn.FindAsync(id);
            if (exConn == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.ExType, "Id", "Name", exConn.TypeId);
            ViewData["SessionId"] = new SelectList(_context.SessionInfo, "SessionId", "Start", exConn.SessionId);

            return View(exConn);

        }

        // POST: ExConns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExConn exConn)
        {
            if (id != exConn.ConnId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exConn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExConnExists(exConn.ConnId))
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

            ViewData["TypeId"] = new SelectList(_context.ExType, "Id", "Name", exConn.TypeId);
            ViewData["SessionId"] = new SelectList(_context.SessionInfo, "SessionId", "Start", exConn.SessionId);
            return View(exConn);
        }


        // GET: ExConns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exConn = await _context.ExConn
                .FirstOrDefaultAsync(m => m.ConnId == id);
            if (exConn == null)
            {
                return NotFound();
            }

            return View(exConn);
        }

        // POST: ExConns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exConn = await _context.ExConn.FindAsync(id);
            if (exConn != null)
            {
                _context.ExConn.Remove(exConn);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExConnExists(int id)
        {
            return _context.ExConn.Any(e => e.ConnId == id);
        }
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

    }
}
