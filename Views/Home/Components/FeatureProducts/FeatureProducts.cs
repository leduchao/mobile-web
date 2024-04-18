using Microsoft.AspNetCore.Mvc;
using MobileWeb.Areas.Admin.Services.ProductService;
using NuGet.Protocol;

namespace MobileWeb.Views.Home.Components.FeatureProducts
{
    public class FeatureProducts(IProductService service) : ViewComponent
    {
        private readonly IProductService _service = service;

        public IViewComponentResult Invoke()
        {
            // lay 3 san pham co gia cao nhat
            var slideList = _service
                .GetAllAsync()
                //.Where(p => p.Price > 10000000)
                //.OrderBy(p => p.Price)
                .Take(3);

            return View(slideList);
        }
    }
}
