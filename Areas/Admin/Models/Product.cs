using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileWeb.Areas.Admin.Models
{
  public class Product
  {
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImgUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
    public string? Color { get; set; }
    public string? Specifications { get; set; }
  }
}
