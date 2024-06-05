using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Amora.Data;
using Amora.Models;

namespace Amora.Controllers
{
    public class RejectedsController : Controller
    {
        private readonly AmoraContext _context;

        public RejectedsController(AmoraContext context)
        {
            _context = context;
        }

        // GET: Rejecteds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rejected.ToListAsync());
        }

        // GET: Rejecteds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rejected = await _context.Rejected
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rejected == null)
            {
                return NotFound();
            }

            return View(rejected);
        }

        // GET: Rejecteds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rejecteds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,User_ID,RejectedUser_ID")] Rejected rejected)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rejected);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rejected);
        }

        // GET: Rejecteds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rejected = await _context.Rejected.FindAsync(id);
            if (rejected == null)
            {
                return NotFound();
            }
            return View(rejected);
        }

        // POST: Rejecteds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,User_ID,RejectedUser_ID")] Rejected rejected)
        {
            if (id != rejected.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rejected);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RejectedExists(rejected.Id))
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
            return View(rejected);
        }

        // GET: Rejecteds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rejected = await _context.Rejected
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rejected == null)
            {
                return NotFound();
            }

            return View(rejected);
        }

        // POST: Rejecteds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rejected = await _context.Rejected.FindAsync(id);
            if (rejected != null)
            {
                _context.Rejected.Remove(rejected);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RejectedExists(int id)
        {
            return _context.Rejected.Any(e => e.Id == id);
        }
    }
}
