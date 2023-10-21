using MobileWeb.Models.DTOs;
using MobileWeb.Models.Entities;

namespace MobileWeb.Areas.Admin.Services.ProductService
{
	public interface IProductService
	{
		Task<List<Product>> GetAllAsync();

		Task<Product?> GetByIdAsync(int id);

		Task<Product?> GetByNameAsync(string productName);

		Task CreateAsync(ProductDTO request);

		Task UpdateAsync(int id, ProductDTO request);

		Task DeleteAsync(int id);

		Task<List<Category>> GetAllCategoriesAsync();

		Task<Specifications?> GetSpecificationsAsync(int id);
	}
}
