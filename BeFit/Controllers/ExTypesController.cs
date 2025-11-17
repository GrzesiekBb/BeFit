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
    public class ExTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExType.ToListAsync());
        }

        // GET: ExTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exType = await _context.ExType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exType == null)
            {
                return NotFound();
            }

            return View(exType);
        }

        // GET: ExTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ExType exType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exType);
        }

        // GET: ExTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exType = await _context.ExType.FindAsync(id);
            if (exType == null)
            {
                return NotFound();
            }
            return View(exType);
        }

        // POST: ExTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ExType exType)
        {
            if (id != exType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExTypeExists(exType.Id))
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
            return View(exType);
        }

        // GET: ExTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exType = await _context.ExType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exType == null)
            {
                return NotFound();
            }

            return View(exType);
        }

        // POST: ExTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exType = await _context.ExType.FindAsync(id);
            if (exType != null)
            {
                _context.ExType.Remove(exType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExTypeExists(int id)
        {
            return _context.ExType.Any(e => e.Id == id);
        }
    }
}
