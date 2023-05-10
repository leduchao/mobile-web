using System.ComponentModel.DataAnnotations;

namespace MobileWeb.Models
{
  public class User
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Không được bỏ trống trường này!")]

    public string? Name { get; set; }

    [Required(ErrorMessage = "Không được bỏ trống trường này!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Không được bỏ trống trường này!")]
    public string? Password { get; set; }

    //[Required(ErrorMessage = "Không được bỏ trống trường này!")]
    public string? Address { get; set; }

    //[Required(ErrorMessage = "Không được  bỏ trống trường này!")]
    public string? AvatarUrl { get; set; }

    //[Required(ErrorMessage = "Không được bỏ trống trường này!")]
    public string? Role { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public int? Birthday { get; set; }
  }
}
