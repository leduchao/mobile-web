using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobileWeb.Data;
using MobileWeb.Models;

namespace MobileWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly MobileWebContext _context;

        public AdminController(MobileWebContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        ////GET: Products(trang home admin, hien thi danh sach san pham)
        //public async Task<IActionResult> ShowListProducts()
        //{
        //  var mobileWebContext = _context?.Product?.Include(p => p.Category);
        //  return View("Views/Products/Index.cshtml", await mobileWebContext!.ToListAsync());
        //}

        //// GET: Products/ShowListProducts/5 (xem 1 san pham)
        //public async Task<IActionResult> ShowProductDetail(int? id)
        //{
        //  if (id == null || _context.Product == null)
        //  {
        //    return NotFound();
        //  }

        //  var product = await _context.Product
        //      .Include(p => p.Category)
        //      .FirstOrDefaultAsync(m => m.Id == id);

        //  if (product == null)
        //  {
        //    return NotFound();
        //  }

        //  return View("Views/Products/Detail", product);
        //}

        //// GET: Products/AddProduct (nut them san pham)
        //public IActionResult AddProduct()
        //{
        //  ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
        //  return View("/Products/Create");
        //}

        //// POST: Products/AddProducts (them sam pham vao database)
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddProduct([Bind("Name,Description,CategoryId,Id,Price,ImgUrl,Quantity")] Product product)
        //{
        //  if (ModelState.IsValid)
        //  {
        //    _context.Add(product);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //  }
        //  ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
        //  return View("/Products/Create", product);
        //}

        //// GET: Products/EditProduct/5 (nut chinh sua san pham)
        //public async Task<IActionResult> EditProduct(int? id)
        //{
        //  if (id == null || _context.Product == null)
        //  {
        //    return NotFound();
        //  }

        //  var product = await _context.Product.FindAsync(id);
        //  if (product == null)
        //  {
        //    return NotFound();
        //  }
        //  ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", product.CategoryId);
        //  return View("/Products/Edit", product);
        //}

        //// POST: Products/EditProduct/5 (cap nhat san pham vao database)
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditProduct(int id, [Bind("Name,Description,CategoryId,Id,Price,ImgUrl,Quantity,Color,Specifications")] Product product)
        //{
        //  if (id != product.Id)
        //  {
        //    return NotFound();
        //  }

        //  if (ModelState.IsValid)
        //  {
        //    try
        //    {
        //      _context.Update(product);
        //      await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //      if (!ProductExists(product.Id))
        //      {
        //        return NotFound();
        //      }
        //      else
        //      {
        //        throw;
        //      }
        //    }
        //    return RedirectToAction(nameof(Index));
        //  }
        //  ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", product.CategoryId);
        //  return View("/Products/Edit", product);
        //}

        //// GET: Products/DeleteProduct/5 (nut xoa san pham)
        //public async Task<IActionResult> DeleteProduct(int? id)
        //{
        //  if (id == null || _context.Product == null)
        //  {
        //    return NotFound();
        //  }

        //  var product = await _context.Product
        //      .Include(p => p.Category)
        //      .FirstOrDefaultAsync(m => m.Id == id);
        //  if (product == null)
        //  {
        //    return NotFound();
        //  }

        //  return View("/Product/Delete", product);
        //}

        //// POST: Products/DeleteProduct/5 (xoa san pham khoi database)
        //[HttpPost, ActionName("DeleteProduct")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteProductConfirmed(int id)
        //{
        //  if (_context.Product == null)
        //  {
        //    return Problem("Entity set 'MobileWebContext.Product'  is null.");
        //  }
        //  var product = await _context.Product.FindAsync(id);
        //  if (product != null)
        //  {
        //    _context.Product.Remove(product);
        //  }

        //  await _context.SaveChangesAsync();
        //  return RedirectToAction(nameof(Index));
        //}

        //// kiem tra san pham co ton tai khong
        //private bool ProductExists(int id)
        //{
        //  return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
