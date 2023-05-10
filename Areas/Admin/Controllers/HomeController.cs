using Microsoft.AspNetCore.Mvc;

namespace MobileWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	//[Route("admin/[controller]/[action]/[id?]")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult ShowProduct()
		{
			return View();
		}
	}
}
