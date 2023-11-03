﻿
using System.ComponentModel.DataAnnotations;

namespace MobileWeb.Areas.Identity.Models.Account;
public class LoginViewModel
{
    [Required(ErrorMessage = "Phải nhập email")]
    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
    public string? Email { get; set; }


    [Required(ErrorMessage = "Phải nhập mật khẩu")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string? Password { get; set; }

    [Display(Name = "Nhớ thông tin đăng nhập?")]
    public bool RememberMe { get; set; }
}
