using MobileWeb.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileWeb.Models.DTOs
{
    public class ProductDTO
    {
        [Required(ErrorMessage = "Phải điền tên sản phẩm!")]
        [StringLength(20, ErrorMessage = "{0} dài từ {2} đến {1} ký tự!", MinimumLength = 3)]
        [Display(Name = "Tên sản phẩm")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Hãy nhập một đoạn mô tả ngắn!")]
        [Display(Name = "Mô tả sản phẩm")]
        public string? Description { get; set; }

        //[Required(ErrorMessage = "Không được bỏ trống trường này!")]
        public string? ImgUrl { get; set; }

        [Required(ErrorMessage = "Phải nhập giá tiền!")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} phải lớn hơn hoặc bằng 0!")]
        [Display(Name = "Giá tiền")]
        public double Price { get; set; } = 0;

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0!")]
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; } = 1;

        [Required(ErrorMessage = "Hãy chọn danh mục!")]
        [ForeignKey("Category")]
        [Display(Name = "Chọn danh mục sản phẩm")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        [Required(ErrorMessage = "Nhập ít nhất 1 màu!")]
        [Display(Name = "Màu sắc")]
        public string? Color { get; set; }

        [Required(ErrorMessage = "Hãy nhập thông số kỹ thuật!")]
        [Display(Name = "Thông số kỹ thuật")]
        public int SpecificationsId { get; set; }

        public Specifications? Specifications { get; set; } // thong so chi tiet
    }
}
