using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileWeb.Models
{
  public class Product
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Không được bỏ trống trường này!")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Không được bỏ trống trường này!")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Không được bỏ trống trường này!")]
    public string? ImgUrl { get; set; }

    [Required(ErrorMessage = "Không được bỏ trống trường này!")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Không được bỏ trống trường này!")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Không được bỏ trống trường này!")]
    [ForeignKey("Category")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Không được bỏ trống trường này!")]
    public virtual Category? Category { get; set; }

    [Required(ErrorMessage = "Không được bỏ trống trường này!")]
    public string? Color { get; set; }

    [Required(ErrorMessage = "Không được bỏ trống trường này!")]
    public string? Specifications { get; set; }
  }
}
