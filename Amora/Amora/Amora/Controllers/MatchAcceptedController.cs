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
    public class MatchAcceptedController : Controller
    {
        private readonly AmoraContext _context;

        public MatchAcceptedController(AmoraContext context)
        {
            _context = context;
        }

        // GET: MatchAccepted
        public async Task<IActionResult> Index()
        {
            return View(await _context.MatchAccepted.ToListAsync());
        }

        // GET: MatchAccepted/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchAccepted = await _context.MatchAccepted
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchAccepted == null)
            {
                return NotFound();
            }

            return View(matchAccepted);
        }

        // GET: MatchAccepted/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MatchAccepted/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,User_Id,MatchedUser_Id")] MatchAccepted matchAccepted)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matchAccepted);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(matchAccepted);
        }

        // GET: MatchAccepted/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchAccepted = await _context.MatchAccepted.FindAsync(id);
            if (matchAccepted == null)
            {
                return NotFound();
            }
            return View(matchAccepted);
        }

        // POST: MatchAccepted/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,User_Id,MatchedUser_Id")] MatchAccepted matchAccepted)
        {
            if (id != matchAccepted.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matchAccepted);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchAcceptedExists(matchAccepted.Id))
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
            return View(matchAccepted);
        }

        // GET: MatchAccepted/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchAccepted = await _context.MatchAccepted
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchAccepted == null)
            {
                return NotFound();
            }

            return View(matchAccepted);
        }

        // POST: MatchAccepted/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matchAccepted = await _context.MatchAccepted.FindAsync(id);
            if (matchAccepted != null)
            {
                _context.MatchAccepted.Remove(matchAccepted);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchAcceptedExists(int id)
        {
            return _context.MatchAccepted.Any(e => e.Id == id);
        }
    }
}
