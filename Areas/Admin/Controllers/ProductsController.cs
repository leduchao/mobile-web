using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobileWeb.Models.Entities;
using MobileWeb.Data;

namespace MobileWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("admin/products")]
	public class ProductsController : Controller
	{
		private readonly WebDbContext _context;

		public ProductsController(WebDbContext context)
		{
			_context = context;
		}

		[Route("danh-sach-san-pham")]
		public async Task<IActionResult> Index()
		{
			var listProducts = _context.Products?.Include(p => p.Category);
			return View(await listProducts!.ToListAsync());
		}

		[Route("chi-tiet-san-pham")]
		public async Task<IActionResult> Details(int? id)
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

		// GET: Admin/Products/Create
		public IActionResult Create()
		{
			ViewData["CategoryId"] = new SelectList(_context.Products, "Id", "Id");
			return View();
		}

		// POST: Admin/Products/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(
			[Bind("Name,Description,ImgUrl,Price," +
			"Quantity,CategoryId,Color,Specifications")] Product product)
		{
			if (ModelState.IsValid)
			{
				_context.Add(product);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
			return View(product);
		}

		// GET: Admin/Products/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Products == null)
			{
				return NotFound();
			}

			var product = await _context.Products.FindAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
			return View(product);
		}

		// POST: Admin/Products/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImgUrl,Price,Quantity,CategoryId,Color,Specifications")] Product product)
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
			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
			return View(product);
		}

		// GET: Admin/Products/Delete/5
		public async Task<IActionResult> Delete(int? id)
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

		// POST: Admin/Products/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Products == null)
			{
				return Problem("Entity set 'MobileWebContext.Product_1'  is null.");
			}
			var product = await _context.Products.FindAsync(id);
			if (product != null)
			{
				_context.Products.Remove(product);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ProductExists(int id)
		{
			return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
