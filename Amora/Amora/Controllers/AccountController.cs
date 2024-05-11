using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Amora.Data;
using System.Security.Cryptography;
using Amora.Models;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace Amora.Controllers
{
    public class AccountController : Controller
    {
        private readonly AmoraContext _context;

        public AccountController(AmoraContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View("~/Views/Home/Register.cshtml");
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //string hashedPassword = HashPassword(model.Password);

                try
                {
                    var user = new RegisterViewModel
                    {
                        Email = model.Email,
                        Name = model.Name,
                        Surname = model.Surname,
                        Gender = model.Gender,
                        Age = model.Age,
                        PhoneNumber = model.PhoneNumber,
                        Hobby = model.Hobby,
                        Password = model.Password,
                        ConfirmPassword = model.ConfirmPassword
                    };

                    _context.RegisterViewModel.Add(user);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    // Obsługa błędów zapisu do bazy danych
                    ModelState.AddModelError("", "Wystąpił błąd podczas rejestracji użytkownika. " + ex.Message); // Dodanie komunikatu błędu do ModelState
                    // Logowanie lub inne działania w przypadku błędu
                    return View(model);
                }
            }

            return View(model);
        }
    }
}
