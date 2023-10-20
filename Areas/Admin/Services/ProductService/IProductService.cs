using MobileWeb.Models.DTOs;
using MobileWeb.Models.Entities;

namespace MobileWeb.Areas.Admin.Services.ProductService
{
	public interface IProductService
	{
		Task<List<Product>?> GetAll();

		Task<Product?> GetById(int id);

		Task<Product?> GetByName(string productName);

		Task Create(Product product);

		Task<bool> Update(int id, ProductDTO productDTO);

		Task Delete(int id);
	}
}
