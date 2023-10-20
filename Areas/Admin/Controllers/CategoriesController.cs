using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileWeb.Models.Entities;
using MobileWeb.Data;
using MobileWeb.Models.DTOs;

namespace MobileWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("admin/quan-ly-danh-muc")]
	public class CategoriesController : Controller
	{
		private readonly WebDbContext _context;

		public CategoriesController(WebDbContext context)
		{
			_context = context;
		}

		// danh sach cac danh muc
		[Route("danh-sach-danh-muc")]
		public async Task<IActionResult> Index()
		{
			return _context.Categories != null ?
						View(await _context.Categories.ToListAsync()) :
						Problem("Entity set 'MobileWebContext.Category_1'  is null.");
		}

		[HttpGet]
		[Route("chi-tiet-danh-muc")]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Categories == null)
				return NotFound();

			var category = await _context.Categories
				.FirstOrDefaultAsync(m => m.Id == id);

			if (category == null)
				return NotFound();

			return View(category);
		}

		[Route("tao-danh-muc-moi")]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[Route("tao-danh-muc-moi")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name")] CategoryDTO categoryDTO)
		{
			if (ModelState.IsValid)
			{
				var category = new Category
				{
					Name = categoryDTO.Name
				};

				_context.Add(category);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(categoryDTO);
		}

		[Route("chinh-sua-danh-muc")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Categories == null)
				return NotFound();

			var category = await _context.Categories.FindAsync(id);

			if (category == null)
				return NotFound();

			ViewData["CategoryId"] = category.Id;

			return View(new CategoryDTO { Name = category.Name });
		}

		[HttpPost]
        [Route("chinh-sua-danh-muc")]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Name")] CategoryDTO categoryDTO)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var existCategory = await _context.Categories.FindAsync(id);

					if (existCategory != null)
					{
						existCategory.Name = categoryDTO.Name;

						_context.Categories.Update(existCategory);
						await _context.SaveChangesAsync();
					}
					else
					{
						return NotFound();
					}

				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CategoryExists(id))
						return NotFound();
					else
						throw;
				}

				return RedirectToAction(nameof(Index));
			}

			return View(categoryDTO);
		}

		[Route("xoa-danh-muc")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Categories == null)
				return NotFound();

			var category = await _context.Categories
				.FirstOrDefaultAsync(m => m.Id == id);

			if (category == null)
				return NotFound();

			ViewData["CategoryId"] = category.Id;

			return View(new CategoryDTO { Name = category.Name });
		}

        [Route("xoa-danh-muc")]
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Categories == null)
				return Problem("Entity set 'MobileWebContext.Categories' is null.");

			var category = await _context.Categories.FindAsync(id);

			if (category != null)
				_context.Categories.Remove(category);

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool CategoryExists(int id)
		{
			return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
