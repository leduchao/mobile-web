using Microsoft.AspNetCore.Mvc;
using MobileWeb.Areas.Admin.Services.ProductService;
using MobileWeb.Models;
using MobileWeb.Services.EmailService;
using System.Diagnostics;

namespace MobileWeb.Controllers
{
    public class HomeController(
        ILogger<HomeController> logger) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

		public IActionResult Index()
        {
            _logger.LogInformation("Application init!");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}