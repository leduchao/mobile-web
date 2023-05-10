using System.ComponentModel.DataAnnotations;

namespace MobileWeb.Areas.Admin.Models
{
  public class Category
  {
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
  }
}
