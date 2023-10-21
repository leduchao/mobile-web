using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileWeb.Models.Entities;
using MobileWeb.Data;
using MobileWeb.Models.DTOs;
using MobileWeb.Areas.Admin.Services.CategoryService;

namespace MobileWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("admin/quan-ly-danh-muc")]
	public class CategoriesController : Controller
	{
		private readonly ICategoryService _service;

		public CategoriesController(ICategoryService service)
		{
			_service = service;
		}

		// danh sach cac danh muc
		[Route("danh-sach-danh-muc")]
		public async Task<IActionResult> Index()
		{
			return View(await _service.GetAllAsync());
		}

		[Route("tao-danh-muc-moi")]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[Route("tao-danh-muc-moi")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CategoryDTO categoryDTO)
		{
			if (ModelState.IsValid)
			{
				var category = new Category
				{
					Name = categoryDTO.Name
				};

				await _service.CreateAsync(category);
				return RedirectToAction(nameof(Index));
			}

			return View(categoryDTO);
		}

		[Route("chinh-sua-danh-muc")]
		public async Task<IActionResult> Edit(int id)
		{
			var category = await _service.GetByIdAsync(id);

			if (category == null)
				return NotFound();

			ViewData["CategoryId"] = category.Id;

			return View(new CategoryDTO { Name = category.Name });
		}

		[HttpPost]
        [Route("chinh-sua-danh-muc")]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, CategoryDTO categoryDTO)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var existCategory = await _service.GetByIdAsync(id);

					if (existCategory != null)
					{
						existCategory.Name = categoryDTO.Name;

						await _service.UpdateAsync(existCategory);
					}
					else
						return NotFound();

				}
				catch (DbUpdateConcurrencyException)
				{
					throw;
				}

				return RedirectToAction(nameof(Index));
			}

			return View(categoryDTO);
		}

		[HttpGet]
		[Route("xoa-danh-muc")]
		public async Task<IActionResult> Delete(int id)
		{
			var category = await _service.GetByIdAsync(id);

			if (category == null)
				return NotFound();

			await _service.DeleteAsync(category);
			return RedirectToAction(nameof(Index));
        }
	}
}
