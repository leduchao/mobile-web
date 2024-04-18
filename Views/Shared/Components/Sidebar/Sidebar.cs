using Microsoft.AspNetCore.Mvc;
using MobileWeb.Data;

namespace MobileWeb.Views.Shared.Components.Sidebar;

public class Sidebar(WebDbContext context) : ViewComponent
{
    private readonly WebDbContext _context = context;

	public IViewComponentResult Invoke()
    {
        var categories = _context.Categories.ToList();
        return View(categories);
    }
}
