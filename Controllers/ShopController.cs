using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using MobileWeb.Data;
using MobileWeb.Models.Entities;
using Newtonsoft.Json;
using System.Globalization;

namespace MobileWeb.Controllers
{
    public class ShopController : Controller
    {
        private readonly WebDbContext _context;
        //public const string CARTKEY = "cart";

        public ShopController(WebDbContext context)
        {
            _context = context;
        }

        public WebDbContext GetContext()
        {
            return _context;
        }

        public IActionResult Index()
        {
            var listProducts = _context.Products?.Include(p => p.Category).ToList();
            return View(listProducts);
        }

        [HttpGet]
        public IActionResult Index([FromQuery] string keyword)
        {
            var listProducts = _context.Products?.Include(p => p.Category);

            if (keyword != null)
            {
                var listSearch = _context.Products?.Include(p => p.Category)
                                  .Where(p => p.Name!.Contains(keyword))
                                  .ToList();

                return View(listSearch);
            }
            
            return View(listProducts);
        }

        public async Task<IActionResult> ProductDetails(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
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

        //giá từ thấp -> cao
        public IActionResult SortProductsByPrice(string? price)
        {
            var listProduct = new List<Product>();

            if (price == "min-to-max")
            {
                listProduct = _context?.Products?.OrderBy(p => p.Price).ToList();
            }
            else if (price == "max-to-min")
            {
                listProduct = _context?.Products?.OrderByDescending(p => p.Price).ToList();
            }
            else
            {
                listProduct = _context?.Products?.ToList();
            }

            return View("Index", listProduct);
        }
    }
}
