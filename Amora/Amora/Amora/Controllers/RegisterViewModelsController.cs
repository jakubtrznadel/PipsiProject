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
    public class RegisterViewModelsController : Controller
    {
        private readonly AmoraContext _context;

        public RegisterViewModelsController(AmoraContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.RegisterViewModel.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerViewModel = await _context.RegisterViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registerViewModel == null)
            {
                return NotFound();
            }

            return View(registerViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Name,Surname,Gender,Age,PhoneNumber,Hobby,Password,ConfirmPassword")] RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registerViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registerViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerViewModel = await _context.RegisterViewModel.FindAsync(id);
            if (registerViewModel == null)
            {
                return NotFound();
            }
            return View(registerViewModel);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Name,Surname,Gender,Age,PhoneNumber,Hobby,Password,ConfirmPassword")] RegisterViewModel registerViewModel)
        {
            if (id != registerViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registerViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegisterViewModelExists(registerViewModel.Id))
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
            return View(registerViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerViewModel = await _context.RegisterViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registerViewModel == null)
            {
                return NotFound();
            }

            return View(registerViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registerViewModel = await _context.RegisterViewModel.FindAsync(id);
            if (registerViewModel != null)
            {
                _context.RegisterViewModel.Remove(registerViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegisterViewModelExists(int id)
        {
            return _context.RegisterViewModel.Any(e => e.Id == id);
        }
    }
}
