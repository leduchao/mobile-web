using Microsoft.AspNetCore.Mvc;
using MobileWeb.Models;
using MobileWeb.Services.EmailService;
using System.Diagnostics;

namespace MobileWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _service;

        public HomeController(ILogger<HomeController> logger, IEmailService service)
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
        //    string subject = "TEST";
        //    string body = "<h1>Xin chào</h1>";

        //    await _service.SendMailAsync(to, subject, body);

        //    _logger.LogInformation("Email sended!");

        //    return NoContent();
        //}
    }
}