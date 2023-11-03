using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using MobileWeb.Areas.Admin.Services.ProductService;
using MobileWeb.Data;
using MobileWeb.Models.Entities;
using Newtonsoft.Json;
using System.Globalization;

namespace MobileWeb.Controllers
{
    public class ShopController : Controller
    {
        private readonly WebDbContext _context;
        private readonly IProductService _service;

        //public const string CARTKEY = "cart";

        public ShopController(WebDbContext context, IProductService service)
        {
            _context = context;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var listProducts = await _service.GetAllAsync();
            return View(listProducts);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword)
        {
            var listProducts = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(keyword))
            {
                var listSearch = listProducts
                    .Where(p => 
                        p.Name!.ToLower().Contains(keyword.ToLower()) || 
                        p.Category!.Name!.ToLower().Contains(keyword.ToLower()))
                    .ToList();

                return View(listSearch);
            }

            return View(listProducts);
        }

        public async Task<IActionResult> ProductDetails(int id)
        {

            var product = await _service.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Specifications = await _service.GetSpecificationsAsync(product.Id);
            return View(product);
        }

        //[Authorize(Roles = "user")]
        //public IActionResult AddToCart(int id)//int id, string name, decimal price, string color, string quanity)
        //{
        //  var product = _context.Product?.Where(p => p.Id == id).FirstOrDefault();
        //  if (product == null)
        //  {
        //    return NotFound("Không tìm thấy sản phẩm");
        //  }
        //  //dua vao gio hang
        //  var cart = GetCartItems();
        //  var cartitem = cart.Find(p => p.Product?.Id == id);
        //  if (cartitem != null)
        //  {
        //    // Đã tồn tại, tăng thêm 1
        //    cartitem.Quantity++;
        //  }
        //  else
        //  {
        //    //  Thêm mới
        //    cart.Add(new CartItems() { Quantity = 1, Product = product });
        //  }
        //  // Lưu cart vào Session
        //  SaveCartSession(cart);
        //  // Chuyển đến trang hiện thị Cart
        //  return RedirectToAction("Cart", "Users");
        //}

        //[Authorize(Roles = "user")]
        //public IActionResult RemoveCart(int productId)
        //{

        //  // Xử lý xóa một mục của Cart ...
        //  return RedirectToAction(nameof(Cart));
        //}

        ////dat hang
        //public IActionResult CheckOut()
        //{
        //  // Xử lý khi đặt hàng
        //  return View();
        //}

        //[Authorize(Roles = "user")]
        //public IActionResult ShowCart()
        //{
        //  var listProducts = _context.Cart?.ToList();
        //  return View("Views/Users/Cart.cshtml", GetCartItems());
        //}

        //// Lấy cart từ Session (danh sách CartItem)
        //List<CartItems> GetCartItems()
        //{
        //  var session = HttpContext.Session;
        //  string? jsoncart = session.GetString(CARTKEY);
        //  if (jsoncart != null)
        //  {
        //    return JsonConvert.DeserializeObject<List<CartItems>>(jsoncart)!;
        //  }
        //  return new List<CartItems>();
        //}

        //// Xóa cart khỏi session
        //void ClearCart()
        //{
        //  var session = HttpContext.Session;
        //  session.Remove(CARTKEY);
        //}

        //// Lưu Cart (Danh sách CartItem) vào session
        //void SaveCartSession(List<CartItems> ls)
        //{
        //  var session = HttpContext.Session;
        //  string jsoncart = JsonConvert.SerializeObject(ls);
        //  session.SetString(CARTKEY, jsoncart);
        //}

        [HttpPost]
        [Authorize]
        public IActionResult Buy()
        {
            return View();
        }

        //sap xep theo gia
        public async Task<IActionResult> SortProductsByPrice(string price)
        {
            var listProduct = await _service.GetAllAsync();
            IEnumerable<Product> listSorted;

            if (price == "min-to-max")
                listSorted = listProduct.OrderBy(p => p.Price);
            else if (price == "max-to-min")
                listSorted = listProduct.OrderByDescending(p => p.Price);
            else 
                return RedirectToAction(nameof(Index));

            return View(nameof(Index), listSorted);
        }
    }
}
