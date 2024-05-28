using System.Diagnostics;
using Amora.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amora.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

		public IActionResult Profile()
		{
			return View();
		}
        public IActionResult EditHobby()
        {
            return View();
        }

        public IActionResult ChoosePhoto()
        {
            return View();
        }
        public IActionResult ChooseGender()
        {
            return View();
        }

        public IActionResult PreviousMatch()
        {
            return View();
        }
        public IActionResult NoMatches()
        {
            return View("~/Views/Home/ChooseGender.cshtml");
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
