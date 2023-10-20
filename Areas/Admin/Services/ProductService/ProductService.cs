using Microsoft.EntityFrameworkCore;
using MobileWeb.Data;
using MobileWeb.Models.DTOs;
using MobileWeb.Models.Entities;

namespace MobileWeb.Areas.Admin.Services.ProductService
{
	public class ProductService : IProductService
	{
		private readonly WebDbContext _context;

		public ProductService(WebDbContext context)
		{
			_context = context;
		}

		public async Task Create(Product product)
		{
			if (_context.Products != null)
			{
				_context.Products.Add(product);
				await _context.SaveChangesAsync();
			}
			else
				return;
		}

		public async Task Delete(int id)
		{
            if (_context.Products != null)
			{ 
                var product = _context.Products.FirstOrDefault(x => x.Id == id);

				if (product != null)
				{
					_context.Products.Remove(product);
					await _context.SaveChangesAsync();
				}
			}
		}

		public async Task<List<Product>?> GetAll()
		{
			if (_context.Products != null)
				return await _context.Products
					.Include(p => p.Category)
					.ToListAsync();

			return null;
		}

		public async Task<Product?> GetById(int id)
		{
			if (_context.Products == null)
				return null;

			return await _context.Products
				.Include(p => p.Category)
				.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<Product?> GetByName(string productName)
		{
            if (_context.Products == null)
                return null;

            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Name == productName);
        }

		public async Task<bool> Update(int id, ProductDTO productDTO)
		{
			if (_context.Products != null)
			{
				var existProduct = await _context.Products
					.Include(p => p.Category)
					.FirstOrDefaultAsync(p => p.Id == id);

				if (existProduct != null)
				{
					existProduct.Name = productDTO.Name;
					existProduct.Description = productDTO.Description;
					existProduct.ImgUrl = productDTO.ImgUrl;
					existProduct.Price = productDTO.Price;
					existProduct.Quantity = productDTO.Quantity;
					existProduct.Category = productDTO.Category;
					existProduct.Color = productDTO.Color;
					existProduct.Specifications = productDTO.Specifications;

					_context.Products.Update(existProduct);
					await _context.SaveChangesAsync();

					return true;
				}
			}
				
			return false;
        }
	}
}
