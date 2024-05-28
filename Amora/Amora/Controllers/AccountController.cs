using Microsoft.AspNetCore.Mvc;
using Amora.Data;
using Amora.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Amora.Migrations;

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
        public async Task<IActionResult> Register(RegisterViewModel model, IFormFile photo)
        {
            var hashedNumberPhone = HashString(model.PhoneNumber);
            var hashedEmail = HashString(model.Email);
            var existingUser = _context.RegisterViewModel.FirstOrDefault(u => u.Email == HashString(model.Email));
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "User with this email already exists.");
                return View("~/Views/Home/Register.cshtml", model);
            }

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Age.Year < 1959 || model.Age.Year > 2024)
                {
                    ModelState.AddModelError("Age", "Invalid birth year, minimal is 1959 and maximum is the year we are in");
                    return View("~/Views/Home/Register.cshtml", model);
                }
                try
                {
                    if (photo != null)
                    {
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(photo.FileName);

                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");

                        Directory.CreateDirectory(uploadsFolder);

                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await photo.CopyToAsync(fileStream);
                        }

                        model.Photo = uniqueFileName;
                    }

                    var user = new RegisterViewModel
                    {
                        Email = hashedEmail,
                        Name = model.Name,
                        Surname = model.Surname,
                        Gender = model.Gender,
                        Age = model.Age,
                        PhoneNumber = hashedNumberPhone,
                        Hobby = model.Hobby,
                        Password = HashPassword(model.Password),
                        ConfirmPassword = HashPassword(model.ConfirmPassword),
                        Photo = model.Photo
                    };

                    _context.RegisterViewModel.Add(user);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while registering the user. " + ex.Message);
                    return View("~/Views/Home/Register.cshtml", model);
                }
            }

            return View("~/Views/Home/Register.cshtml", model);
        }






        private int CalculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }

        public IActionResult Login()
        {
            return View("~/Views/Home/login.cshtml");
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                var hashedEmail = HashString(model.Email);
                var user = _context.RegisterViewModel.FirstOrDefault(u => u.Email == hashedEmail && u.Password == HashPassword(model.Password));

                if (user != null)
                {
                    int age = CalculateAge(user.Age);
                    HttpContext.Session.SetInt32("LoggedInUserId", user.Id);
                    HttpContext.Session.SetString("Name", user.Name);
                    HttpContext.Session.SetString("Surname", user.Surname);
                    HttpContext.Session.SetString("Email", user.Email);
                    HttpContext.Session.SetString("Gender", user.Gender);
                    HttpContext.Session.SetInt32("Age", age);
                    HttpContext.Session.SetString("PhoneNumber", user.PhoneNumber);
                    HttpContext.Session.SetString("Hobby", user.Hobby);
                    HttpContext.Session.SetString("Photo", user.Photo);


                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "Invalid email or password.");
                    return View("~/Views/Home/Login.cshtml", model);
                }
            }

            return View("~/Views/Home/Login.cshtml", model);
        }



        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private string HashString(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public string UnhashPhoneNumber(string hashedPhoneNumber)
        {
            try
            {
                byte[] decodedBytes = Convert.FromBase64String(hashedPhoneNumber);
                string phoneNumber = Encoding.UTF8.GetString(decodedBytes);
                return phoneNumber;
            }
            catch (FormatException)
            {
     
                return "Invalid phone number format";
            }
        }





        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }




        public IActionResult Profile()
        {
            var userName = HttpContext.Session.GetString("Name");
            var userGender = HttpContext.Session.GetString("Gender");
            var userBirthDateStr = HttpContext.Session.GetString("BirthDate");
            var userHobby = HttpContext.Session.GetString("Hobby");
            var userPhoto = HttpContext.Session.GetString("Photo");


            DateTime userBirthDate;
            if (!string.IsNullOrEmpty(userBirthDateStr) && DateTime.TryParse(userBirthDateStr, out userBirthDate))
            {
                var userAge = CalculateAge(userBirthDate);

                var userModel = new UserProfileViewModel
                {
                    Name = userName,
                    Gender = userGender,
                    Age = userAge,
                    Hobby = userHobby,
                    Photo = userPhoto
                  
                };

                return View(userModel);
            }
            else
            {
                ViewData["ErrorMessage"] = "Invalid or missing birth date.";
                return View(new UserProfileViewModel());
            }
        }



        
     
        public IActionResult EditHobby()
        {
            var userHobby = HttpContext.Session.GetString("Hobby");
            return View("~/Views/Home/EditHobby.cshtml", new EditHobbyViewModel { Hobby = userHobby });
        }

        [HttpPost]
        public IActionResult EditHobby(EditHobbyViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userEmail = HttpContext.Session.GetString("Email");
                    var user = _context.RegisterViewModel.FirstOrDefault(u => u.Email == userEmail);

                    if (user != null)
                    {
                        user.Hobby = model.Hobby;
                        _context.SaveChanges();

                        HttpContext.Session.SetString("Hobby", model.Hobby);

                        return RedirectToAction("Profile", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "User not found.");
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the hobby. " + ex.Message);
                    return View(model);
                }
            }
            return View(model);
        }



        public IActionResult Match(string gender)
        {
            if (string.IsNullOrEmpty(gender))
            {
                return RedirectToAction("ChooseGender");
            }
            else
            {
                var loggedInUserEmail = HttpContext.Session.GetString("Email");

                var loggedInUser = _context.RegisterViewModel.FirstOrDefault(u => u.Email == loggedInUserEmail);

                var allMatches = _context.Match.Where(m => m.UserId == loggedInUser.Id).ToList();

                var matchedUserIds = allMatches.Select(m => m.MatchedUserId).ToList();

                var users = _context.RegisterViewModel.Where(u => u.Gender == gender && u.Email != loggedInUserEmail && !matchedUserIds.Contains(u.Id)).ToList();

                if (users.Count > 0)
                {
                    int lastIndex = HttpContext.Session.GetInt32("LastDisplayedUserIndex") ?? -1;

                    lastIndex = (lastIndex + 1) % users.Count;

                    HttpContext.Session.SetInt32("LastDisplayedUserIndex", lastIndex);

                    var nextUser = users[lastIndex];

                    return View("~/Views/Home/MatchResults.cshtml", nextUser);
                }
                else
                {
                    return View("~/Views/Home/NoMatches.cshtml");
                }
            }
        }











        public IActionResult MatchDecision(string gender, int id, string decision)
        {
           
            var loggedInUser = _context.RegisterViewModel.FirstOrDefault(u => u.Email == HttpContext.Session.GetString("Email"));

            if (loggedInUser != null)
            {
                
                var match = new Match
                {
                    UserId = loggedInUser.Id, 
                    MatchedUserId = id
                };

                
                if (decision == "Yes")
                {
                    
                }
                else if (decision == "No")
                {
                   
                }

                _context.Match.Add(match);
                _context.SaveChanges();
            }
            else
            {
                
                return RedirectToAction("Login");
            }

            
            var usersJson = HttpContext.Session.GetString("MatchUsers");
            if (usersJson != null)
            {
                var users = JsonConvert.DeserializeObject<List<RegisterViewModel>>(usersJson);
                var userToRemove = users.FirstOrDefault(u => u.Id == id);
                if (userToRemove != null)
                {
                    users.Remove(userToRemove);
                    HttpContext.Session.SetString("MatchUsers", JsonConvert.SerializeObject(users));
                }
            }

            return RedirectToAction("Match", new { gender = gender });
        }


        public IActionResult PreviousMatches()
        {
            var loggedInUserId = HttpContext.Session.GetInt32("LoggedInUserId");

            if (loggedInUserId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var matches = _context.Match
                                  .Include(m => m.MatchedUser)
                                  .Where(m => m.UserId == loggedInUserId)
                                  .ToList();

            var matchedUsers = matches.Select(m => m.MatchedUser).ToList();


            return View("~/Views/Home/PreviousMatches.cshtml", matchedUsers);
        }







        public IActionResult NoMatches()
        {
            return View("~/Views/Home/NoMatches.cshtml");
        }



        public IActionResult ChooseGender()
        {
            return View("~/Views/Home/ChooseGender.cshtml");
        }


       
    }

}

