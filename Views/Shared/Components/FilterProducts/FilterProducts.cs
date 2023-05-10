using Microsoft.AspNetCore.Mvc;
using MobileWeb.Data;
using MobileWeb.Models;

namespace MobileWeb.Views.Shared.Components.FilterProducts
{
  [ViewComponent]
  public class FilterProducts : ViewComponent
  {
    private readonly MobileWebContext _context;

    public FilterProducts(MobileWebContext context)
    {
      _context = context;
    }

    public IViewComponentResult Invoke(string condition)
    {
      var listProducts = _context.Product?.ToList();
      if (condition == "min-to-max")
      {
        listProducts = _context?.Product?.OrderBy(p => p.Price).ToList();
      }
      else if (condition == "max-to-min")
      {
        listProducts = _context?.Product?.OrderByDescending(p => p.Price).ToList();
      }
      return View(listProducts);
    }
  }
}
