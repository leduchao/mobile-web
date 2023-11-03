using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MobileWeb.Areas.Identity.Services;
using MobileWeb.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MobileWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountService _service;

        public HomeController(ILogger<HomeController> logger, IAccountService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
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

        //public async Task<IActionResult> SendMail()
        //{
        //    string to = "doantrihung4444@gmail.com";
        //    int code = 123456;

        //    await _service.SendEmailAsync(to, code.ToString());

        //    _logger.LogInformation("Mail has been sended!");

        //    return NoContent();
        //}
    }
}