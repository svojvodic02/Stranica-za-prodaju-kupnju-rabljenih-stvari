using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vjezba.Web.Models;

namespace Vjezba.Web.Controllers
{
    public class HomeController(
        ILogger<HomeController> _logger) 
        : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy(string lang)
        {
            ViewData["Message"] = "Your application description page. Language = " + lang;
            return View();
        }

		[Route("cesto-postavljana-pitanja/{selected:int:min(1):max(99)?}")]
		public IActionResult FAQ(int? selected = null)
        {
            ViewData["selected"] = selected;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}