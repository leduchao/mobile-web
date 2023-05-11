//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobileWeb.Data;
using MobileWeb.Models;
using MobileWeb.Services;

namespace MobileWeb.Controllers
{
  public class ProductsController : Controller
  {
    private readonly MobileWebContext _context;
    private readonly CartService _cartService;

    public ProductsController(MobileWebContext context, CartService cartService)
    {
      _context = context;
      _cartService = cartService;
    }

    // GET: Products
    public async Task<IActionResult> Index()
    {
      var mobileWebContext = _context?.Product?.Include(p => p.Category);
      return View(await mobileWebContext!.ToListAsync());
    }

    // GET: Products/Details/5
    public async Task<IActionResult> Details(int? id)
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

    // GET: Products/Create
    public IActionResult Create()
    {
      ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
      return View();
    }

    // POST: Products/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description,CategoryId,Id,Price,ImgUrl,Quantity,Color,Specifications")] Product product)
    {
      if (ModelState.IsValid)
      {
        _context.Add(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
      return View(product);
    }

    // GET: Products/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null || _context.Product == null)
      {
        return NotFound();
      }

      var product = await _context.Product.FindAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
      return View(product);
    }

    // POST: Products/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Name,Description,CategoryId,Id,Price,ImgUrl,Quantity,Color,Specifications")] Product product)
    {
      if (id != product.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(product);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ProductExists(product.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
      return View(product);
    }

    // GET: Products/Delete/5
    public async Task<IActionResult> Delete(int? id)
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

    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      if (_context.Product == null)
      {
        return Problem("Entity set 'MobileWebContext.Product'  is null.");
      }
      var product = await _context.Product.FindAsync(id);
      if (product != null)
      {
        _context.Product.Remove(product);
      }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int id)
    {
      return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    [Route("/cart", Name = "cart")]
    public IActionResult Cart()
    {
      return View(_cartService.GetCartItems());
    }

    [Authorize(Roles = "user")]
    //[Route("/cart/{product-id:int}",  Name = "AddCart")]
    public IActionResult AddToCart(int productId)
    {
      var product = _context.Product?.Where(p => p.Id == productId)
                                    .FirstOrDefault();
      if(product == null)
      {
        return NotFound();
      }
      var cart = _cartService.GetCartItems();
      var cartitem = cart?.Find(p => p.Product?.Id == productId);
      if (cartitem != null)
      {
        // Đã tồn tại, tăng thêm 1
        cartitem.Quantity++;
      }
      else
      {
        //  Thêm mới
        cart?.Add(new CartItems() { Quantity = 1, Product = product });
      }

      // Lưu cart vào Session
      _cartService.SaveCartSession(cart!);
      return RedirectToAction(nameof(Cart));
    }

    //cập nhật ljai thông tin của sản phẩm trong giỏ
    [HttpPost]
    [Route("/updatecart", Name = "updatecart")]
    public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
    {
      // Cập nhật Cart thay đổi số lượng quantity ...
      var cart = _cartService.GetCartItems();
      var cartitem = cart?.Find(p => p.Product?.Id == productid);
      if (cartitem != null)
      {
        // Đã tồn tại, tăng thêm 1
        cartitem.Quantity = quantity;
      }
      _cartService.SaveCartSession(cart!);
      // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
      return Ok();
    }

    //xóa 1 sản phẩm ra khỏi giỏ
    [Route("/removecart/{productid:int}", Name = "removecart")]
    public IActionResult RemoveCart([FromRoute] int productid)
    {
      var cart = _cartService.GetCartItems();
      var cartitem = cart?.Find(p => p.Product?.Id == productid);
      if (cartitem != null)
      {
        // Đã tồn tại, giảm đi 1
        cart?.Remove(cartitem);
      }

      _cartService.SaveCartSession(cart!);
      return RedirectToAction(nameof(Cart));
    }
  }
}
