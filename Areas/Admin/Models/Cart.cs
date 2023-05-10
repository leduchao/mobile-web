using System.ComponentModel.DataAnnotations;

namespace MobileWeb.Areas.Admin.Models
{
	public class Cart
	{
		[Key]
		public int ProductId { get; set; }
		public int CategoryId { get; set; }
		public int NumberOfProducts { get; set; }
	}
}
