//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using MobileWeb.Areas.Admin.Models;
//using MobileWeb.Data;

//namespace MobileWeb.Areas.Admin.Controllers
//{
//  [Area("Admin")]
//  public class ProductsController : Controller
//  {
//    private readonly MobileWebContext _context;

//    public ProductsController(MobileWebContext context)
//    {
//      _context = context;
//    }

//    // GET: Admin/Products
//    public async Task<IActionResult> Index()
//    {
//      var mobileWebContext = _context.Product_1?.Include(p => p.Category);
//      return View(await mobileWebContext!.ToListAsync());
//    }

//    // GET: Admin/Products/Details/5
//    public async Task<IActionResult> Details(int? id)
//    {
//      if (id == null || _context.Product_1 == null)
//      {
//        return NotFound();
//      }

//      var product = await _context.Product_1
//          .Include(p => p.Category)
//          .FirstOrDefaultAsync(m => m.Id == id);
//      if (product == null)
//      {
//        return NotFound();
//      }

//      return View(product);
//    }

//    // GET: Admin/Products/Create
//    public IActionResult Create()
//    {
//      ViewData["CategoryId"] = new SelectList(_context.Category_1, "Id", "Id");
//      return View();
//    }

//    // POST: Admin/Products/Create
//    // To protect from overposting attacks, enable the specific properties you want to bind to.
//    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Create([Bind("Id,Name,Description,ImgUrl,Price,Quantity,CategoryId,Color,Specifications")] Product product)
//    {
//      if (ModelState.IsValid)
//      {
//        _context.Add(product);
//        await _context.SaveChangesAsync();
//        return RedirectToAction(nameof(Index));
//      }
//      ViewData["CategoryId"] = new SelectList(_context.Category_1, "Id", "Id", product.CategoryId);
//      return View(product);
//    }

//    // GET: Admin/Products/Edit/5
//    public async Task<IActionResult> Edit(int? id)
//    {
//      if (id == null || _context.Product_1 == null)
//      {
//        return NotFound();
//      }

//      var product = await _context.Product_1.FindAsync(id);
//      if (product == null)
//      {
//        return NotFound();
//      }
//      ViewData["CategoryId"] = new SelectList(_context.Category_1, "Id", "Id", product.CategoryId);
//      return View(product);
//    }

//    // POST: Admin/Products/Edit/5
//    // To protect from overposting attacks, enable the specific properties you want to bind to.
//    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImgUrl,Price,Quantity,CategoryId,Color,Specifications")] Product product)
//    {
//      if (id != product.Id)
//      {
//        return NotFound();
//      }

//      if (ModelState.IsValid)
//      {
//        try
//        {
//          _context.Update(product);
//          await _context.SaveChangesAsync();
//        }
//        catch (DbUpdateConcurrencyException)
//        {
//          if (!ProductExists(product.Id))
//          {
//            return NotFound();
//          }
//          else
//          {
//            throw;
//          }
//        }
//        return RedirectToAction(nameof(Index));
//      }
//      ViewData["CategoryId"] = new SelectList(_context.Category_1, "Id", "Id", product.CategoryId);
//      return View(product);
//    }

//    // GET: Admin/Products/Delete/5
//    public async Task<IActionResult> Delete(int? id)
//    {
//      if (id == null || _context.Product_1 == null)
//      {
//        return NotFound();
//      }

//      var product = await _context.Product_1
//          .Include(p => p.Category)
//          .FirstOrDefaultAsync(m => m.Id == id);
//      if (product == null)
//      {
//        return NotFound();
//      }

//      return View(product);
//    }

//    // POST: Admin/Products/Delete/5
//    [HttpPost, ActionName("Delete")]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> DeleteConfirmed(int id)
//    {
//      if (_context.Product_1 == null)
//      {
//        return Problem("Entity set 'MobileWebContext.Product_1'  is null.");
//      }
//      var product = await _context.Product_1.FindAsync(id);
//      if (product != null)
//      {
//        _context.Product_1.Remove(product);
//      }

//      await _context.SaveChangesAsync();
//      return RedirectToAction(nameof(Index));
//    }

//    private bool ProductExists(int id)
//    {
//      return (_context.Product_1?.Any(e => e.Id == id)).GetValueOrDefault();
//    }
//  }
//}
