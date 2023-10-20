using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileWeb.Models.Entities
{
	public class Order
	{
		[Key]
		public int ProductId { get; set; }
		public int CategoryId { get; set; }
		public int Quantity { get; set; }
		public double TotalPrice { get; set; }
		public double Price { get; set; }
		public string? Name { get; set; }
	}
}
