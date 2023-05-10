using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileWeb.Models
{
	public class Cart
	{
		[Key]
		public int ProductId { get; set; }
		public int CategoryId { get; set; }
		public int Quantity { get; set; }
		public string? Color { get; set; }
		public decimal TotalPrice { get; set; } = 0;
		public decimal Price { get; set; }
		public string? Name { get; set; }
	}
}
