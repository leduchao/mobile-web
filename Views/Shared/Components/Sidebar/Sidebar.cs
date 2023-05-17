using Microsoft.AspNetCore.Mvc;
using MobileWeb.Data;

namespace MobileWeb.Views.Shared.Components.Sidebar
{
    [ViewComponent]
    public class Sidebar : ViewComponent
    {
        private readonly MobileWebContext _context;

        public Sidebar(MobileWebContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _context.Category.ToList();
            return View(categories);
        }
    }
}
