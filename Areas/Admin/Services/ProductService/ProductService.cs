using Azure.Core;
using Microsoft.EntityFrameworkCore;
using MobileWeb.Data;
using MobileWeb.Models.DTOs;
using MobileWeb.Models.Entities;
using System.Drawing;
using System.Runtime.Intrinsics.Arm;

namespace MobileWeb.Areas.Admin.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly WebDbContext _context;

        public ProductService(WebDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ProductDTO request)
        {
            var specifications = new Specifications
            {
                RAM = request.RAM,
                ROM = request.ROM,
                NumberOfFrontCamera = 1,
                NumberOfBehindCamera = request.NumberOfBehindCamera,
                Model = request.Model,
                OperatingSystem = request.OperatingSystemVersion
            };

            var newProduct = new Product
            {
                Name = request.Name,
                Description = request.Description,
                ImgUrl = request.ImgUrl,
                Price = request.Price,
                Quantity = request.Quantity,
                Color = request.Color,
                CategoryId = request.CategoryId,
                Specifications = specifications
            };

            var cate = await _context.Categories.FirstOrDefaultAsync(c => c.Id == newProduct.CategoryId);
            specifications.Brand = cate?.Name;

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> GetByNameAsync(string productName)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Name == productName);
        }

        public async Task<Specifications?> GetSpecificationsAsync(int id)
        {
            return await _context.Specifications.FirstOrDefaultAsync(s => s.ProductId == id);
        }

        public async Task UpdateAsync(int id, ProductDTO request)
        {
            var existProduct = await GetByIdAsync(id);

            if (existProduct != null)
            {
                var specifications = await _context.Specifications
                    .FirstOrDefaultAsync(s => s.ProductId == id);

                if (specifications != null)
                {
                    specifications.RAM = request.RAM;
                    specifications.ROM = request.ROM;
                    specifications.NumberOfFrontCamera = 1;
                    specifications.NumberOfBehindCamera = request.NumberOfBehindCamera;
                    specifications.OperatingSystem = request.OperatingSystemVersion;
                    specifications.Model = request.Model;

                    existProduct.Name = request.Name;
                    existProduct.Description = request.Description;
                    existProduct.ImgUrl = request.ImgUrl;
                    existProduct.Price = request.Price;
                    existProduct.Quantity = request.Quantity;
                    existProduct.Color = request.Color;
                    existProduct.CategoryId = request.CategoryId;

                    var cate = _context.Categories.FirstOrDefault(c => c.Id == existProduct.CategoryId);
                    specifications.Brand = cate?.Name;

                    existProduct.Specifications = specifications;

                    _context.Products.Update(existProduct);
                    _context.Specifications.Update(specifications);
                    await _context.SaveChangesAsync();
				}
            }
        }
    }
}
