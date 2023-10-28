using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobileWeb.Areas.Admin.Services.ProductService;
using MobileWeb.Models.DTOs;

namespace MobileWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/quan-ly-san-pham")]
    public class ProductsController : Controller
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [Route("danh-sach-san-pham")]
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        [HttpGet]
        [Route("chi-tiet-san-pham")]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _service.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            var specifications = await _service.GetSpecificationsAsync(product.Id);

            if (specifications == null)
                return NotFound();

            //FileAccess.

            var productDTO = new ProductDTO
            {
                Name = product.Name,
                Description = product.Description,
                //ImgUrl = ,
                
                Price = product.Price,
                Quantity = product.Quantity,
                Category = product.Category,
                Color = product.Color,
                RAM = specifications!.RAM,
                ROM = specifications.ROM,
                NumberOfBehindCamera = specifications.NumberOfBehindCamera,
                Model = specifications.Model,
                OperatingSystemVersion = specifications.OperatingSystem
            };

            ViewBag.ProductId = product.Id;

            return View(productDTO);
        }

        // GET: Admin/Products/Create
        [Route("tao-moi-san-pham")]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(
                await _service.GetAllCategoriesAsync(), "Id", "Name");

            return View();
        }

        // POST: Admin/Products/Create
        [HttpPost]
        [Route("tao-moi-san-pham")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO request)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(request);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(
                await _service.GetAllCategoriesAsync(), "Id", "Name");

            return View(request);
        }

        // GET: Admin/Products/Edit/5
        [Route("chinh-sua-san-pham")]
        public async Task<IActionResult> Edit(int id)
        {

            var product = await _service.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var specifications = await _service.GetSpecificationsAsync(product.Id);

            if (specifications == null)
            {
                return NotFound();
            }

            var productDTO = new ProductDTO
            {
                Name = product.Name,
                Description = product.Description,
                //ImgUrl = product.ImgUrl,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
                Color = product.Color,
                RAM = specifications!.RAM,
                ROM = specifications.ROM,
                NumberOfBehindCamera = specifications.NumberOfBehindCamera,
                Model = specifications.Model,
                OperatingSystemVersion = specifications.OperatingSystem
            };

            ViewBag.ProductId = product.Id;
            ViewBag.CategoryId = new SelectList(
                await _service.GetAllCategoriesAsync(), "Id", "Name");

            return View(productDTO);
        }

        // POST: Admin/Products/Edit/5
        [HttpPost]
        [Route("chinh-sua-san-pham")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductDTO request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(id, request);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(
                await _service.GetAllCategoriesAsync(), "Id", "Name");

            return View(request);
        }

        // GET: Admin/Products/Delete/5
        [HttpGet]
        [Route("xoa-san-pham")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
