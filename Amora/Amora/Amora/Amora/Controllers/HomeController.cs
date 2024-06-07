using System.Diagnostics;
using System.Globalization;
using Amora.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            _logger.LogInformation("Wywo³ano akcjê Index");
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult Login()
        {
            _logger.LogInformation("Wywo³ano akcjê Login");
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

        public IActionResult Notifications()
        {
            return View();
        }

        public IActionResult ChangeLanguage(string lang)
        {
            if (!string.IsNullOrEmpty(lang))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                lang = "en";
            }
            Response.Cookies.Append("Language", lang);
            return Redirect(Request.GetTypedHeaders().Referer.ToString());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
