using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileWeb.Data;
using MobileWeb.Models;

namespace MobileWeb.Views.Shared.Components.FilterProducts
{  
    //[ViewComponent]
    public class FilterProducts : ViewComponent
    {
        private readonly MobileWebContext _context;

        public FilterProducts(MobileWebContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var listProducts = _context.Product!.Include(p => p.Category).ToList();
            return View(listProducts);
        }
    }
}
