using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using MobileWeb.Data;
using MobileWeb.Models;
using Newtonsoft.Json;
using System.Globalization;

namespace MobileWeb.Controllers
{
  public class ShopController : Controller
  {
    private readonly MobileWebContext _context;
    //public const string CARTKEY = "cart";

    public ShopController(MobileWebContext context)
    {
      _context = context;
    }

    public MobileWebContext GetContext()
    {
      return _context;
    }

    public IActionResult Index()
    {
      var listProducts = _context.Product?.Include(p => p.Category).ToList();
      return View(listProducts);
    }

    [HttpGet]
    public IActionResult Index(string keyword)
    {
      var listProducts = _context.Product?.Include(p => p.Category);
      if (keyword != null)
      {
        var listSearch = _context.Product?.Include(p => p.Category)
                          .Where(p => p.Name!.Contains(keyword))// || p.Description.Contains(keyword))
                          .ToList();

        //ViewData["products"] = listSearch;

        return View(listSearch);
      }
      //ViewBag.Products = listSearch;
      return View(listProducts);
    }

    public async Task<IActionResult> ProductDetails(int? id)
    {
      if (id == null || _context.Product == null)
      {
        return NotFound();
      }
      var product = await _context.Product
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
    public IActionResult SortProductsByPrice(string? condition)
    {
      var listProduct = new List<Product>();

      if (condition == "min-to-max")
      {
        listProduct = _context?.Product?.OrderBy(p => p.Price).ToList();
      }
      else if (condition == "max-to-min")
      {
        listProduct = _context?.Product?.OrderByDescending(p => p.Price).ToList();
      }
      else
      {
        listProduct = _context?.Product?.ToList();
      }

      ViewData["ListProducts"] = listProduct;
      return View();
    }

    //giá từ cao -> thấp
    public IActionResult FromMaxToMinPrice()
    {
      return View();
    }
  }
}
